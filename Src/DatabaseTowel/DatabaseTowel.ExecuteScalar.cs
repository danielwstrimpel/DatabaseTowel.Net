namespace DatabaseTowel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;

    public partial class DatabaseTowel : IDatabaseTowel
    {
        /// <summary>
        /// Executes the scalar command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>
        /// The result of execution of the scalar command.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// The command parameter is required.
        /// or
        /// Failed to successfully execute the command.
        /// </exception>
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
                throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteScalarFailed, "Failed to successfully execute the command.", ex);
            }
        }

        /// <summary>
        /// Executes the scalar command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the scalar command.
        /// </returns>
        public object ExecuteScalar(IDbCommand command, Func<DatabaseTowelException, object> errorContext)
        {
            try
            {
                return this.ExecuteScalar(command);
            }
            catch (DatabaseTowelException ex)
            {
                return errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the scalar command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The result of execution of the scalar command.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the command.
        /// </exception>
        public object ExecuteScalar(string commandText, IEnumerable<DbParameter> parameters)
        {
            object result = null;

            this.ExecuteSql(connection => result = this.ExecuteScalar(commandText, parameters, connection));

            return result;
        }

        /// <summary>
        /// Executes the scalar command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the scalar command.
        /// </returns>
        public object ExecuteScalar(string commandText, IEnumerable<DbParameter> parameters, Func<DatabaseTowelException, object> errorContext)
        {
            try
            {
                return this.ExecuteScalar(commandText, parameters);
            }
            catch (DatabaseTowelException ex)
            {
                return errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the scalar command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// The result of execution of the scalar command.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the command.
        /// </exception>
        public object ExecuteScalar(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection)
        {
            using (var command = this.CreateCommand(commandText, connection))
            {
                command.Parameters.AddMany(parameters);

                return this.ExecuteScalar(command);
            }
        }

        /// <summary>
        /// Executes the scalar command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the scalar command.
        /// </returns>
        public object ExecuteScalar(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<DatabaseTowelException, object> errorContext)
        {
            try
            {
                return this.ExecuteScalar(commandText, parameters, connection);
            }
            catch (DatabaseTowelException ex)
            {
                return errorContext(ex);
            }
        }
        
        /// <summary>
        /// Executes the scalar command, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>
        /// The result of execution of the scalar command.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// The command parameter is required.
        /// or
        /// Failed to successfully execute the command.
        /// </exception>
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
                    return await (command as DbCommand).ExecuteScalarAsync();
                }
            }
            catch (DbException ex)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteScalarFailed, "Failed to successfully execute the command.", ex);
            }
        }

        /// <summary>
        /// Executes the scalar command, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the scalar command.
        /// </returns>
        public async Task<object> ExecuteScalarAsync(IDbCommand command, Func<DatabaseTowelException, Task<object>> errorContext)
        {
            try
            {
                return await this.ExecuteScalarAsync(command);
            }
            catch (DatabaseTowelException ex)
            {
                return await errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the scalar command, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The result of execution of the scalar command.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the command.
        /// </exception>
        public async Task<object> ExecuteScalarAsync(string commandText, IEnumerable<DbParameter> parameters)
        {
            object result = null;

            await this.ExecuteSqlAsync(async connection => result = await this.ExecuteScalarAsync(commandText, parameters, connection));

            return result;
        }

        /// <summary>
        /// Executes the scalar command, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the scalar command.
        /// </returns>
        public async Task<object> ExecuteScalarAsync(string commandText, IEnumerable<DbParameter> parameters, Func<DatabaseTowelException, Task<object>> errorContext)
        {
            try
            {
                return await this.ExecuteScalarAsync(commandText, parameters);
            }
            catch (DatabaseTowelException ex)
            {
                return await errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the scalar command, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// The result of execution of the scalar command.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the command.
        /// </exception>
        public async Task<object> ExecuteScalarAsync(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection)
        {
            using (var command = this.CreateCommand(commandText, connection))
            {
                command.Parameters.AddMany(parameters);

                return await this.ExecuteScalarAsync(command);
            }
        }

        /// <summary>
        /// Executes the scalar command, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the scalar command.
        /// </returns>
        public async Task<object> ExecuteScalarAsync(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<DatabaseTowelException, Task<object>> errorContext)
        {
            try
            {
                return await this.ExecuteScalarAsync(commandText, parameters, connection);
            }
            catch (DatabaseTowelException ex)
            {
                return await errorContext(ex);
            }
        }
    }
}