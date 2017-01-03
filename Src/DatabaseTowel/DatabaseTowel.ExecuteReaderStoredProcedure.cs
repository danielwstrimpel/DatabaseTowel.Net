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
        /// Executes the reader stored procedure, returning the results as a data table.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The result of execution of the reader stored procedure.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader stored procedure.
        /// </exception>
        public DataTable ExecuteReaderStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters)
        {
            DataTable result = null;

            this.ExecuteSql(connection => result = this.ExecuteReaderStoredProcedure(storedProcedureName, parameters, connection));

            return result;
        }

        /// <summary>
        /// Executes the reader stored procedure, returning the results as a data table.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the reader stored procedure.
        /// </returns>
        public DataTable ExecuteReaderStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, Func<DatabaseTowelException, DataTable> errorContext)
        {
            try
            {
                return this.ExecuteReaderStoredProcedure(storedProcedureName, parameters);
            }
            catch (DatabaseTowelException ex)
            {
                return errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the reader stored procedure, returning the results as a data table.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// The result of execution of the reader stored procedure.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader stored procedure.
        /// </exception>
        public DataTable ExecuteReaderStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection)
        {
            return this.ExecuteReaderStoredProcedure(
                storedProcedureName,
                parameters,
                connection,
                ex => { throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteReaderStoredProcedureFailed, "Failed to successfully execute the reader stored procedure.", ex); });
        }

        /// <summary>
        /// Executes the reader stored procedure, returning the results as a data table.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// The result of execution of the reader stored procedure.
        /// </returns>
        public DataTable ExecuteReaderStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<DatabaseTowelException, DataTable> errorContext)
        {
            using (var command = this.CreateCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddMany(parameters);

                return this.ExecuteReader(command, errorContext);
            }
        }

        /// <summary>
        /// Executes the reader stored procedure, returning the results as a data table, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The result of execution of the reader stored procedure.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader stored procedure.
        /// </exception>
        public async Task<DataTable> ExecuteReaderStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters)
        {
            DataTable result = null;

            await this.ExecuteSqlAsync(async connection => result = await this.ExecuteReaderStoredProcedureAsync(storedProcedureName, parameters, connection));

            return result;
        }

        /// <summary>
        /// Executes the reader stored procedure, returning the results as a data table, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the reader stored procedure.
        /// </returns>
        public async Task<DataTable> ExecuteReaderStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, Func<DatabaseTowelException, Task<DataTable>> errorContext)
        {
            try
            {
                return await this.ExecuteReaderStoredProcedureAsync(storedProcedureName, parameters);
            }
            catch (DatabaseTowelException ex)
            {
                return await errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the reader stored procedure, returning the results as a data table, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// The result of execution of the reader stored procedure.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader stored procedure.
        /// </exception>
        public async Task<DataTable> ExecuteReaderStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection)
        {
            return await this.ExecuteReaderStoredProcedureAsync(
                storedProcedureName,
                parameters,
                connection,
                ex => { throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteReaderStoredProcedureFailed, "Failed to successfully execute the reader stored procedure.", ex); });
        }

        /// <summary>
        /// Executes the reader stored procedure, returning the results as a data table, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// The result of execution of the reader stored procedure.
        /// </returns>
        public async Task<DataTable> ExecuteReaderStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<DatabaseTowelException, Task<DataTable>> errorContext)
        {
            using (var command = this.CreateCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddMany(parameters);

                return await this.ExecuteReaderAsync(command, errorContext);
            }
        }
    }
}