namespace DatabaseTowel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;

    public partial class DatabaseTowel : IExecuteScalarStoredProcedure
    {
        /// <summary>
        /// Executes the scalar stored procedure, returning the scalar result.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The result of execution of the scalar stored procedure.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the scalar stored procedure.
        /// </exception>
        public object ExecuteScalarStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters)
        {
            object result = null;

            this.ExecuteSql(connection => result = this.ExecuteScalarStoredProcedure(storedProcedureName, parameters, connection));

            return result;
        }

        /// <summary>
        /// Executes the scalar stored procedure, returning the scalar result.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the scalar stored procedure.
        /// </returns>
        public object ExecuteScalarStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, Func<DatabaseTowelException, object> errorContext)
        {
            try
            {
                return this.ExecuteScalarStoredProcedure(storedProcedureName, parameters);
            }
            catch (DatabaseTowelException ex)
            {
                return errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the scalar stored procedure, returning the scalar result.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// The result of execution of the scalar stored procedure.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the scalar stored procedure.
        /// </exception>
        public object ExecuteScalarStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection)
        {
            return this.ExecuteScalarStoredProcedure(
                storedProcedureName,
                parameters,
                connection,
                ex => { throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteScalarStoredProcedureFailed, "Failed to successfully execute the scalar stored procedure.", ex); });
        }

        /// <summary>
        /// Executes the scalar stored procedure, returning the scalar result.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the scalar stored procedure.
        /// </returns>
        public object ExecuteScalarStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<DatabaseTowelException, object> errorContext)
        {
            using (var command = this.CreateCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddMany(parameters);

                return this.ExecuteScalar(command, errorContext);
            }
        }

        /// <summary>
        /// Executes the scalar stored procedure, returning the scalar result, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The result of execution of the scalar stored procedure.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the scalar stored procedure.
        /// </exception>
        public async Task<object> ExecuteScalarStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters)
        {
            object result = null;

            await this.ExecuteSqlAsync(async connection => result = await this.ExecuteScalarStoredProcedureAsync(storedProcedureName, parameters, connection));

            return result;
        }

        /// <summary>
        /// Executes the scalar stored procedure, returning the scalar result, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the scalar stored procedure.
        /// </returns>
        public async Task<object> ExecuteScalarStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, Func<DatabaseTowelException, Task<object>> errorContext)
        {
            try
            {
                return await this.ExecuteScalarStoredProcedureAsync(storedProcedureName, parameters);
            }
            catch (DatabaseTowelException ex)
            {
                return await errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the scalar stored procedure, returning the scalar result, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// The result of execution of the scalar stored procedure.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the scalar stored procedure.
        /// </exception>
        public async Task<object> ExecuteScalarStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection)
        {
            return await this.ExecuteScalarStoredProcedureAsync(
                storedProcedureName,
                parameters,
                connection,
                ex => { throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteScalarStoredProcedureFailed, "Failed to successfully execute the scalar stored procedure.", ex); });
        }

        /// <summary>
        /// Executes the scalar stored procedure, returning the scalar result, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the scalar stored procedure.
        /// </returns>
        public async Task<object> ExecuteScalarStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<DatabaseTowelException, Task<object>> errorContext)
        {
            using (var command = this.CreateCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddMany(parameters);

                return await this.ExecuteScalarAsync(command, errorContext);
            }
        }
    }
}