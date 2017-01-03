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
        /// Executes the non query command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="DatabaseTowelException">
        /// The command parameter is required.
        /// or
        /// Failed to successfully execute the command.
        /// </exception>
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
        /// Executes the non query command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="errorContext">The error context.</param>
        public void ExecuteNonQuery(IDbCommand command, Action<DatabaseTowelException> errorContext)
        {
            try
            {
                this.ExecuteNonQuery(command);
            }
            catch (DatabaseTowelException ex)
            {
                errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the non query command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <exception cref="DatabaseTowelException">
        /// The command parameter is required.
        /// or
        /// Failed to successfully execute the command.
        /// </exception>
        public void ExecuteNonQuery(string commandText, IEnumerable<DbParameter> parameters)
        {
            this.ExecuteSql(connection => this.ExecuteNonQuery(commandText, parameters, connection));
        }

        /// <summary>
        /// Executes the non query command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        public void ExecuteNonQuery(string commandText, IEnumerable<DbParameter> parameters, Action<DatabaseTowelException> errorContext)
        {
            try
            {
                this.ExecuteNonQuery(commandText, parameters);
            }
            catch (DatabaseTowelException ex)
            {
                errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the non query command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <exception cref="DatabaseTowelException">
        /// The command parameter is required.
        /// or
        /// Failed to successfully execute the command.
        /// </exception>
        public void ExecuteNonQuery(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection)
        {
            using (var command = this.CreateCommand(commandText, connection))
            {
                command.Parameters.AddMany(parameters);

                this.ExecuteNonQuery(command);
            }
        }

        /// <summary>
        /// Executes the non query command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="errorContext">The error context.</param>
        public void ExecuteNonQuery(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection, Action<DatabaseTowelException> errorContext)
        {
            try
            {
                this.ExecuteNonQuery(commandText, parameters, connection);
            }
            catch (DatabaseTowelException ex)
            {
                errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the non query command, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="DatabaseTowelException">
        /// The command parameter is required.
        /// or
        /// Failed to successfully execute the command.
        /// </exception>
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
                    await (command as DbCommand).ExecuteNonQueryAsync();
                }
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
        /// <param name="errorContext">The error context.</param>
        public async Task ExecuteNonQueryAsync(IDbCommand command, Func<DatabaseTowelException, Task> errorContext)
        {
            try
            {
                await this.ExecuteNonQueryAsync(command);
            }
            catch (DatabaseTowelException ex)
            {
                await errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the non query command, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <exception cref="DatabaseTowelException">
        /// The command parameter is required.
        /// or
        /// Failed to successfully execute the command.
        /// </exception>
        public async Task ExecuteNonQueryAsync(string commandText, IEnumerable<DbParameter> parameters)
        {
            await this.ExecuteSqlAsync(async connection => await this.ExecuteNonQueryAsync(commandText, parameters, connection));
        }

        /// <summary>
        /// Executes the non query command, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        public async Task ExecuteNonQueryAsync(string commandText, IEnumerable<DbParameter> parameters, Func<DatabaseTowelException, Task> errorContext)
        {
            try
            {
                await this.ExecuteNonQueryAsync(commandText, parameters);
            }
            catch (DatabaseTowelException ex)
            {
                await errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the non query command, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <exception cref="DatabaseTowelException">
        /// The command parameter is required.
        /// or
        /// Failed to successfully execute the command.
        /// </exception>
        public async Task ExecuteNonQueryAsync(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection)
        {
            using (var command = this.CreateCommand(commandText, connection))
            {
                command.Parameters.AddMany(parameters);

                await this.ExecuteNonQueryAsync(command);
            }
        }

        /// <summary>
        /// Executes the non query command, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="errorContext">The error context.</param>
        public async Task ExecuteNonQueryAsync(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<DatabaseTowelException, Task> errorContext)
        {
            try
            {
                await this.ExecuteNonQueryAsync(commandText, parameters, connection);
            }
            catch (DatabaseTowelException ex)
            {
                await errorContext(ex);
            }
        }
    }
}