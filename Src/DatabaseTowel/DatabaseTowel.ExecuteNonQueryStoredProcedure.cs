namespace DatabaseTowel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;

    public partial class DatabaseTowel : IExecuteNonQueryStoredProcedure
    {
        /// <summary>
        /// Executes the non query stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the non query stored procedure.
        /// </exception>
        public void ExecuteNonQueryStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters)
        {
            this.ExecuteSql(connection => this.ExecuteNonQueryStoredProcedure(storedProcedureName, parameters, connection));
        }

        /// <summary>
        /// Executes the non query stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        public void ExecuteNonQueryStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, Action<DatabaseTowelException> errorContext)
        {
            try
            {
                this.ExecuteScalarStoredProcedure(storedProcedureName, parameters);
            }
            catch (DatabaseTowelException ex)
            {
                errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the non query stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the non query stored procedure.
        /// </exception>
        public void ExecuteNonQueryStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection)
        {
            this.ExecuteNonQueryStoredProcedure(
                storedProcedureName,
                parameters,
                connection,
                ex => { throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteNonQueryStoredProcedureFailed, "Failed to successfully execute the non query stored procedure.", ex); });
        }

        /// <summary>
        /// Executes the non query stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="errorContext">The error context.</param>
        public void ExecuteNonQueryStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection, Action<DatabaseTowelException> errorContext)
        {
            using (var command = this.CreateCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddMany(parameters);

                this.ExecuteNonQuery(command, errorContext);
            }
        }

        /// <summary>
        /// Executes the non query stored procedure, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the non query stored procedure.
        /// </exception>
        public async Task ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters)
        {
            await this.ExecuteSqlAsync(async connection => await this.ExecuteNonQueryStoredProcedureAsync(storedProcedureName, parameters, connection));
        }

        /// <summary>
        /// Executes the non query stored procedure, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        public async Task ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, Func<DatabaseTowelException, Task> errorContext)
        {
            try
            {
                await this.ExecuteNonQueryStoredProcedureAsync(storedProcedureName, parameters);
            }
            catch (DatabaseTowelException ex)
            {
                await errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the non query stored procedure, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the non query stored procedure.
        /// </exception>
        public async Task ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection)
        {
            await this.ExecuteNonQueryStoredProcedureAsync(
                storedProcedureName,
                parameters,
                connection,
                ex => { throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteNonQueryStoredProcedureFailed, "Failed to successfully execute the non query stored procedure.", ex); });
        }

        /// <summary>
        /// Executes the non query stored procedure, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="errorContext">The error context.</param>
        public async Task ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<DatabaseTowelException, Task> errorContext)
        {
            using (var command = this.CreateCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddMany(parameters);

                await this.ExecuteNonQueryAsync(command, errorContext);
            }
        }
    }
}