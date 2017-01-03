namespace DatabaseTowel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;

    public partial class DatabaseTowel : IExecuteReader
    {
        /// <summary>
        /// Executes the command, returing the results as a data table.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// The command parameter is required.
        /// or
        /// Failed to successfully execute the command.
        /// </exception>
        public DataTable ExecuteReader(IDbCommand command)
        {
            if (command == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The command parameter is required.", new ArgumentNullException("command"));
            }

            try
            {
                using (var reader = command.ExecuteReader())
                {
                    return this.DataReaderToDataTable(reader);
                }
            }
            catch (ArgumentException ex)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteFailed, "Failed to successfully execute the command.", ex);
            }
            catch (DbException ex)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteFailed, "Failed to successfully execute the command.", ex);
            }
        }

        /// <summary>
        /// Executes the command, returing the results as a data table.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        public DataTable ExecuteReader(IDbCommand command, Func<DatabaseTowelException, DataTable> errorContext)
        {
            try
            {
                return this.ExecuteReader(command);
            }
            catch (DatabaseTowelException ex)
            {
                return errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the command, returing the results as a data table.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the command.
        /// </exception>
        public DataTable ExecuteReader(string commandText, IEnumerable<DbParameter> parameters)
        {
            DataTable result = null;

            this.ExecuteSql(connection => result = this.ExecuteReader(commandText, parameters, connection));

            return result;
        }

        /// <summary>
        /// Executes the command, returing the results as a data table.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        public DataTable ExecuteReader(string commandText, IEnumerable<DbParameter> parameters, Func<DatabaseTowelException, DataTable> errorContext)
        {
            try
            {
                return this.ExecuteReader(commandText, parameters);
            }
            catch (DatabaseTowelException ex)
            {
                return errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the command, returing the results as a data table.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the command.
        /// </exception>
        public DataTable ExecuteReader(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection)
        {
            using (var command = this.CreateCommand(commandText, connection))
            {
                command.Parameters.AddMany(parameters);

                return this.ExecuteReader(command);
            }
        }

        /// <summary>
        /// Executes the command, returing the results as a data table.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        public DataTable ExecuteReader(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<DatabaseTowelException, DataTable> errorContext)
        {
            try
            {
                return this.ExecuteReader(commandText, parameters, connection);
            }
            catch (DatabaseTowelException ex)
            {
                return errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the command, returing the results as a data table, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the command.
        /// </exception>
        public async Task<DataTable> ExecuteReaderAsync(IDbCommand command)
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
                    using (var reader = command.ExecuteReader())
                    {
                        return this.DataReaderToDataTable(reader);
                    }
                }
                else
                {
                    using (var reader = await (command as DbCommand).ExecuteReaderAsync())
                    {
                        return await this.DataReaderToDataTableAsync(reader);
                    }
                }
            }
            catch (DbException ex)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteFailed, "Failed to successfully execute the command.", ex);
            }
        }

        /// <summary>
        /// Executes the command, returing the results as a data table, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        public async Task<DataTable> ExecuteReaderAsync(IDbCommand command, Func<DatabaseTowelException, Task<DataTable>> errorContext)
        {
            try
            {
                return await this.ExecuteReaderAsync(command);
            }
            catch (DatabaseTowelException ex)
            {
                return await errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the command, returing the results as a data table, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the command.
        /// </exception>
        public async Task<DataTable> ExecuteReaderAsync(string commandText, IEnumerable<DbParameter> parameters)
        {
            DataTable result = null;

            await this.ExecuteSqlAsync(async connection => result = await this.ExecuteReaderAsync(commandText, parameters, connection));

            return result;
        }

        /// <summary>
        /// Executes the command, returing the results as a data table, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        public async Task<DataTable> ExecuteReaderAsync(string commandText, IEnumerable<DbParameter> parameters, Func<DatabaseTowelException, Task<DataTable>> errorContext)
        {
            try
            {
                return await this.ExecuteReaderAsync(commandText, parameters);
            }
            catch (DatabaseTowelException ex)
            {
                return await errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the command, returing the results as a data table, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the command.
        /// </exception>
        public async Task<DataTable> ExecuteReaderAsync(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection)
        {
            using (var command = this.CreateCommand(commandText, connection))
            {
                command.Parameters.AddMany(parameters);

                return await this.ExecuteReaderAsync(command);
            }
        }

        /// <summary>
        /// Executes the command, returing the results as a data table, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        public async Task<DataTable> ExecuteReaderAsync(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<DatabaseTowelException, Task<DataTable>> errorContext)
        {
            try
            {
                return await this.ExecuteReaderAsync(commandText, parameters, connection);
            }
            catch (DatabaseTowelException ex)
            {
                return await errorContext(ex);
            }
        }
    }
}