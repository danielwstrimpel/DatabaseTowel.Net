namespace DatabaseTowel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;

    public partial class DatabaseTowel : IExecuteReaderStoredProcedure
    {
        /// <summary>
        /// Executes the reader stored procedure, running the context given.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="readContext">The read context.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader stored procedure.
        /// </exception>
        public void ExecuteReaderStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, Action<IDataReader> readContext)
        {
            this.ExecuteSql(connection => this.ExecuteReaderStoredProcedure(storedProcedureName, parameters, connection, readContext));
        }

        /// <summary>
        /// Executes the reader stored procedure, running the context given.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="readContext">The read context.</param>
        /// <param name="errorContext">The error context.</param>
        public void ExecuteReaderStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, Action<IDataReader> readContext, Action<DatabaseTowelException> errorContext)
        {
            try
            {
                this.ExecuteReaderStoredProcedure(storedProcedureName, parameters, readContext);
            }
            catch (DatabaseTowelException ex)
            {
                errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the reader stored procedure, running the context given.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="readContext">The read context.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader stored procedure.
        /// </exception>
        public void ExecuteReaderStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection, Action<IDataReader> readContext)
        {
            this.ExecuteReaderStoredProcedure(
                storedProcedureName,
                parameters,
                connection,
                readContext,
                ex => { throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteReaderStoredProcedureFailed, "Failed to successfully execute the reader stored procedure.", ex); });
        }

        /// <summary>
        /// Executes the reader stored procedure, running the context given.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="readContext">The read context.</param>
        /// <param name="errorContext">The error context.</param>
        public void ExecuteReaderStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection, Action<IDataReader> readContext, Action<DatabaseTowelException> errorContext)
        {
            using (var command = this.CreateCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddMany(parameters);

                this.ExecuteReader(command, readContext, errorContext);
            }
        }

        /// <summary>
        /// Executes the reader stored procedure, running the context given, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="readContext">The read context.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader stored procedure.
        /// </exception>
        public async Task ExecuteReaderStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, Func<IDataReader, Task> readContext)
        {
            await this.ExecuteSqlAsync(async connection => await this.ExecuteReaderStoredProcedureAsync(storedProcedureName, parameters, connection, readContext));
        }

        /// <summary>
        /// Executes the reader stored procedure, running the context given, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="readContext">The read context.</param>
        /// <param name="errorContext">The error context.</param>
        public async Task ExecuteReaderStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, Func<IDataReader, Task> readContext, Func<DatabaseTowelException, Task<DataTable>> errorContext)
        {
            try
            {
                await this.ExecuteReaderStoredProcedureAsync(storedProcedureName, parameters, readContext);
            }
            catch (DatabaseTowelException ex)
            {
                await errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the reader stored procedure, running the context given, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="readContext">The read context.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader stored procedure.
        /// </exception>
        public async Task ExecuteReaderStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<IDataReader, Task> readContext)
        {
            await this.ExecuteReaderStoredProcedureAsync(
                storedProcedureName,
                parameters,
                connection,
                readContext,
                ex => { throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteReaderStoredProcedureFailed, "Failed to successfully execute the reader stored procedure.", ex); });
        }

        /// <summary>
        /// Executes the reader stored procedure, running the context given, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="readContext">The read context.</param>
        /// <param name="errorContext">The error context.</param>
        public async Task ExecuteReaderStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<IDataReader, Task> readContext, Func<DatabaseTowelException, Task<DataTable>> errorContext)
        {
            using (var command = this.CreateCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddMany(parameters);

                await this.ExecuteReaderAsync(command, readContext, errorContext);
            }
        }
    }
}