namespace DatabaseTowel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Transactions;

    /// <summary>
    /// A class that abstracts database access and makes it easier to work with (DRY and testable code).
    /// </summary>
    public class DatabaseTowel : IDatabaseTowel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseTowel" /> class.
        /// </summary>
        /// <param name="databaseProviderFactoryHelper"><see cref="IDbProviderFactoryHelper"/> used by the database helper.</param>
        public DatabaseTowel(IDbProviderFactoryHelper databaseProviderFactoryHelper)
        {
            if (databaseProviderFactoryHelper == null)
            {
                throw new DatabaseTowelException(
                    DatabaseTowelExceptionType.InvalidArgument,
                    "The database provider factory helper is required.",
                    new ArgumentNullException("databaseProviderFactoryHelper"));
            }

            this.DatabaseProviderFactoryHelper = databaseProviderFactoryHelper;
        }
        
        public IDbProviderFactoryHelper DatabaseProviderFactoryHelper { get; private set; }

        /// <summary>
        /// Executes the SQL.
        /// </summary>
        /// <param name="context">The context to execute after the connection is opened.</param>
        /// <param name="errorContext">The context to execute after an error occurs.</param>
        /// <exception cref="DatabaseTowelException">
        /// The connection failed to be opened or a DatabaseException was thrown during execution of the context.
        /// </exception>
        public void ExecuteSql(Action<IDbConnection> context, Action<DatabaseTowelException> errorContext)
        {
            if (context == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The context parameter is required.", new ArgumentNullException("context"));
            }

            if (errorContext == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The errorContext parameter is required.", new ArgumentNullException("errorContext"));
            }

            using (var connection = this.DatabaseProviderFactoryHelper.CreateConnection())
            {
                try
                {
                    try
                    {
                        connection.Open();
                    }
                    catch (DbException ex)
                    {
                        throw new DatabaseTowelException(DatabaseTowelExceptionType.ConnectionOpenFailed, "Failed to successfully open the connection.", ex);
                    }

                    context(connection);
                }
                catch (DatabaseTowelException ex)
                {
                    errorContext(ex);
                }
            }
        }

        /// <summary>
        /// Executes the SQL, asynchronously.
        /// </summary>
        /// <param name="context">The context to execute after the connection is opened.</param>
        /// <param name="errorContext">The context to execute after an error occurs.</param>
        /// <exception cref="DatabaseTowelException">
        /// The connection failed to be opened or a DatabaseException was thrown during execution of the context.
        /// </exception>
        /// <returns>Asynchronous task to execute query.</returns>
        public async Task ExecuteSqlAsync(Func<IDbConnection, Task> context, Action<DatabaseTowelException> errorContext)
        {
            if (context == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The context parameter is required.", new ArgumentNullException("context"));
            }

            if (errorContext == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The errorContext parameter is required.", new ArgumentNullException("errorContext"));
            }

            using (var connection = this.DatabaseProviderFactoryHelper.CreateConnection())
            {
                try
                {
                    try
                    {
                        // If we can't cast as DbConnection, then we have to do things synchronously since the async operations aren't on the interface (thanks Microsoft!).
                        // This really should be during testing only when we mock it out.
                        if (connection as DbConnection == null)
                        {
                            connection.Open();
                        }
                        else
                        {
                            await(connection as DbConnection).OpenAsync();
                        }
                    }
                    catch (DbException ex)
                    {
                        throw new DatabaseTowelException(DatabaseTowelExceptionType.ConnectionOpenFailed, "Failed to successfully open the connection.", ex);
                    }

                    await context(connection);
                }
                catch (DatabaseTowelException ex)
                {
                    errorContext(ex);
                }
            }
        }

        /// <summary>
        /// Executes the SQL transaction. If an exception is thrown in the process of opening the connection or executing the context,
        /// the transaction will not successfully complete and thus will rollback.
        /// </summary>
        /// <param name="context">The context to execute after the transaction and connection are opened.</param>
        /// <param name="errorContext">The context to execute after an error occurs.</param>
        public void ExecuteSqlTransaction(Action<IDbConnection> context, Action<DatabaseTowelException> errorContext)
        {
            using (var transactionScope = new TransactionScope())
            {
                this.ExecuteSql(context, errorContext);
                
                transactionScope.Complete();
            }
        }

        /// <summary>
        /// Executes the SQL transaction, asynchronously. If an exception is thrown in the process of opening the connection or executing
        /// the context, the transaction will not successfully complete and thus will rollback.
        /// </summary>
        /// <param name="context">The context to execute after the transaction and connection are opened.</param>
        /// <param name="errorContext">The context to execute after an error occurs.</param>
        /// <returns>Asynchronous task to execute query.</returns>
        public async Task ExecuteSqlTransactionAsync(Func<IDbConnection, Task> context, Action<DatabaseTowelException> errorContext)
        {
            using (var transactionScope = new TransactionScope())
            {
                await this.ExecuteSqlAsync(context, errorContext);

                transactionScope.Complete();
            }
        }

        /// <summary>
        /// Executes the scalar command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="DatabaseTowelException">Failed to successfully execute the command.</exception>
        /// <returns>
        /// The result of execution of the scalar command.
        /// </returns>
        public object ExecuteScalar(IDbCommand command)
        {
            if (command == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The command parameter is required.", new ArgumentNullException("command"));
            }

            try
            {
                return command.ExecuteScalar();
            }
            catch (DbException ex)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteNonQueryFailed, "Failed to successfully execute the command.", ex);
            }
        }

        /// <summary>
        /// Executes the scalar command, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="DatabaseTowelException">Failed to successfully execute the command.</exception>
        /// <returns>
        /// The result of execution of the scalar command.
        /// </returns>
        public async Task<object> ExecuteScalarAsync(IDbCommand command)
        {
            if (command == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The command parameter is required.", new ArgumentNullException("command"));
            }

            try
            {
                // If we can't cast as DbCommand, then we have to do things synchronously since the async operations aren't on the interface (thanks Microsoft!).
                // This really should be during testing only when we mock it out.
                if (command as DbCommand == null)
                {
                    return command.ExecuteScalar();
                }
                else
                {
                    return await(command as DbCommand).ExecuteScalarAsync();
                }
            }
            catch (DbException ex)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteNonQueryFailed, "Failed to successfully execute the command.", ex);
            }
        }

        /// <summary>
        /// Executes the non query command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="DatabaseTowelException">Failed to successfully execute the command.</exception>
        public void ExecuteNonQuery(IDbCommand command)
        {
            if (command == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The command parameter is required.", new ArgumentNullException("command"));
            }

            try
            {
                command.ExecuteNonQuery();
            }
            catch (DbException ex)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteNonQueryFailed, "Failed to successfully execute the command.", ex);
            }
        }

        /// <summary>
        /// Executes the non query command, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="DatabaseTowelException">Failed to successfully execute the command.</exception>
        /// <returns>Asynchronous task for executing non query.</returns>
        public async Task ExecuteNonQueryAsync(IDbCommand command)
        {
            if (command == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The command parameter is required.", new ArgumentNullException("command"));
            }

            try
            {
                // If we can't cast as DbCommand, then we have to do things synchronously since the async operations aren't on the interface (thanks Microsoft!).
                // This really should be during testing only when we mock it out.
                if (command as DbCommand == null)
                {
                    command.ExecuteNonQuery();
                }
                else
                {
                    await(command as DbCommand).ExecuteNonQueryAsync();
                }
            }
            catch (DbException ex)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteNonQueryFailed, "Failed to successfully execute the command.", ex);
            }
        }

        /// <summary>
        /// Executes the command, opening a data reader.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="context">The context.</param>
        /// <exception cref="DatabaseTowelException">Failed to successfully execute the reader.</exception>
        public void ExecuteReader(IDbCommand command, Action<IDataReader> context)
        {
            if (command == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The command parameter is required.", new ArgumentNullException("command"));
            }

            if (command == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The context parameter is required.", new ArgumentNullException("context"));
            }

            try
            {
                using (var reader = command.ExecuteReader())
                {
                    context(reader);
                }
            }
            catch (ArgumentException ex)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteReaderFailed, "Failed to successfully execute the reader.", ex);
            }
            catch (DbException ex)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteReaderFailed, "Failed to successfully execute the reader.", ex);
            }
        }

        /// <summary>
        /// Executes the command, opening a data reader, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="context">The context.</param>
        /// <exception cref="DatabaseTowelException">Failed to successfully execute the reader.</exception>
        /// <returns>Asynchronous task for executing the reader.</returns>
        public async Task ExecuteReaderAsync(IDbCommand command, Func<IDataReader, Task> context)
        {
            if (command == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The command parameter is required.", new ArgumentNullException("command"));
            }

            if (command == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The context parameter is required.", new ArgumentNullException("context"));
            }

            try
            {
                // If we can't cast as DbCommand, then we have to do things synchronously since the async operations aren't on the interface (thanks Microsoft!).
                // This really should be during testing only when we mock it out.
                if (command as DbCommand == null)
                {
                    using (var reader = command.ExecuteReader())
                    {
                        await context(reader);
                    }
                }
                else
                {
                    using (var reader = await(command as DbCommand).ExecuteReaderAsync())
                    {
                        await context(reader);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteReaderFailed, "Failed to successfully execute the reader.", ex);
            }
            catch (DbException ex)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteReaderFailed, "Failed to successfully execute the reader.", ex);
            }
        }

        /// <summary>
        /// Executes the stored procedure.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// A data table containing the results of the execution of the reader.
        /// </returns>
        public DataTable ExecuteStoredProcedure(string storedProcedure, IEnumerable<DbParameter> parameters, Action<DatabaseTowelException> errorContext)
        {
            if (parameters == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The parameters parameter is required.", new ArgumentNullException("parameters"));
            }

            DataTable dataTable = null;

            this.ExecuteSql(connection =>
            {
                using (var command = this.DatabaseProviderFactoryHelper.CreateCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddMany(parameters);
                    
                    this.ExecuteReader(
                        command, 
                        reader =>
                    {
                        try
                        {
                            dataTable = DataReaderToDataTable(reader);
                        }
                        catch (Exception ex)
                        {
                            throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteReaderFailed, "Failed to load the results from the reader.", ex);
                        }
                    });
                }
            }, errorContext);

            return dataTable;
        }

        /// <summary>
        /// Executes the stored procedure, asynchronously.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// A data table containing the results of the execution of the reader.
        /// </returns>
        public async Task<DataTable> ExecuteStoredProcedureAsync(string storedProcedure, IEnumerable<DbParameter> parameters, Action<DatabaseTowelException> errorContext)
        {
            if (parameters == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The parameters parameter is required.", new ArgumentNullException("parameters"));
            }

            DataTable dataTable = null;

            await this.ExecuteSqlAsync(async connection =>
            {
                using (var command = this.DatabaseProviderFactoryHelper.CreateCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (var p in parameters)
                    {
                        command.Parameters.Add(p);
                    }

                    await this.ExecuteReaderAsync(
                        command, 
                        async reader =>
                    {
                        try
                        {
                           dataTable = await DataReaderToDataTableAsync(reader);
                        }
                        catch (Exception ex)
                        {
                            throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteReaderFailed, "Failed to load the results from the reader.", ex);
                        }
                    });
                }
            }, errorContext);

            return dataTable;
        }

        /// <summary>
        /// Executes the scalar stored procedure.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of the execution of the scalar stored procedure.
        /// </returns>
        public object ExecuteScalarStoredProcedure(string storedProcedure, IEnumerable<DbParameter> parameters, Action<DatabaseTowelException> errorContext)
        {
            if (parameters == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The parameters parameter is required.", new ArgumentNullException("parameters"));
            }

            object result = null;

            this.ExecuteSql(connection =>
            {
                using (var command = this.DatabaseProviderFactoryHelper.CreateCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddMany(parameters);
                    
                    result = this.ExecuteScalar(command);
                }
            }, errorContext);

            return result;
        }

        /// <summary>
        /// Executes the scalar stored procedure, asynchronously.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of the execution of the scalar stored procedure.
        /// </returns>
        public async Task<object> ExecuteScalarStoredProcedureAsync(string storedProcedure, IEnumerable<DbParameter> parameters, Action<DatabaseTowelException> errorContext)
        {
            if (parameters == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The parameters parameter is required.", new ArgumentNullException("parameters"));
            }

            object result = null;

            await this.ExecuteSqlAsync(async connection =>
            {
                using (var command = this.DatabaseProviderFactoryHelper.CreateCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddMany(parameters);

                    // If we can't cast as DbCommand, then we have to do things synchronously since the async operations aren't on the interface (thanks Microsoft!).
                    // This really should be during testing only when we mock it out.
                    if (command as DbCommand == null)
                    {
                        result = this.ExecuteScalar(command);
                    }
                    else
                    {
                        result = await this.ExecuteScalarAsync(command);
                    }
                }
            }, errorContext);

            return result;
        }

        /// <summary>
        /// Loads the contents of the data reader into a data table.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>
        /// A data table loaded with the contents of the data reader.
        /// </returns>
        public DataTable DataReaderToDataTable(IDataReader dataReader)
        {
            if (dataReader == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The dataReader parameter is required.", new ArgumentNullException("dataReader"));
            }

            var dataTable = new DataTable();
            dataTable.Locale = CultureInfo.InvariantCulture;
            dataTable.Load(dataReader);

            return dataTable;            
        }

        /// <summary>
        /// Loads the contents of the data reader into a data table, asynchronously.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>
        /// A data table loaded with the contents of the data reader.
        /// </returns>
        public async Task<DataTable> DataReaderToDataTableAsync(IDataReader dataReader)
        {
            if (dataReader == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The dataReader parameter is required.", new ArgumentNullException("dataReader"));
            }

            // If we can't cast as DbDataReader, then we have to do things synchronously since the async operations aren't on the interface (thanks Microsoft!).
            // This really should be during testing only when we mock it out.
            if (dataReader as DbDataReader == null)
            {
                return this.DataReaderToDataTable(dataReader);
            }

            var columns = dataReader.GetSchemaTable().Rows.OfType<DataRow>().Select(dataRow => new DataColumn
            {
                ColumnName = dataRow[0].ToString(),
                DataType = Type.GetType(dataRow["DataType"].ToString())
            }).ToList();

            var dataTable = new DataTable();
            dataTable.Columns.AddRange(columns.ToArray());

            while (await(dataReader as DbDataReader).ReadAsync())
            {
                var dataRow = dataTable.NewRow();

                columns.ForEach(c => { dataRow[c.ColumnName] = dataReader[c.ColumnName]; });

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }
    }
}