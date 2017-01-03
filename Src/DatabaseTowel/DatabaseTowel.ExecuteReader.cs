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
        /// Executes the reader command, running the context given.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="readContext">The read context.</param>
        /// <exception cref="DatabaseTowelException">
        /// The command parameter is required.
        /// or
        /// The readContext parameter is required.
        /// or
        /// Failed to successfully execute the reader command.
        /// </exception>
        public void ExecuteReader(IDbCommand command, Action<IDataReader> readContext)
        {
            if (command == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The command parameter is required.", new ArgumentNullException("command"));
            }

            if (readContext == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The readContext parameter is required.", new ArgumentNullException("readContext"));
            }

            try
            {
                using (var reader = command.ExecuteReader())
                {
                    readContext(reader);
                }
            }
            catch (ArgumentException ex)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteFailed, "Failed to successfully execute the reader command.", ex);
            }
            catch (DbException ex)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteFailed, "Failed to successfully execute the reader command.", ex);
            }
        }

        /// <summary>
        /// Executes the reader command, running the context given.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="readContext">The read context.</param>
        /// <param name="errorContext">The error context.</param>
        public void ExecuteReader(IDbCommand command, Action<IDataReader> readContext, Action<DatabaseTowelException> errorContext)
        {
            try
            {
                this.ExecuteReader(command, readContext);
            }
            catch (DatabaseTowelException ex)
            {
                errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the reader command, running the context given.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="readContext">The read context.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader command.
        /// </exception>
        public void ExecuteReader(string commandText, IEnumerable<DbParameter> parameters, Action<IDataReader> readContext)
        {
            this.ExecuteSql(connection => this.ExecuteReader(commandText, parameters, connection, readContext));
        }

        /// <summary>
        /// Executes the reader command, running the context given.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="readContext">The read context.</param>
        /// <param name="errorContext">The error context.</param>
        public void ExecuteReader(string commandText, IEnumerable<DbParameter> parameters, Action<IDataReader> readContext, Action<DatabaseTowelException> errorContext)
        {
            try
            {
                this.ExecuteReader(commandText, parameters, readContext);
            }
            catch (DatabaseTowelException ex)
            {
                errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the reader command, running the context given.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="readContext">The read context.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader command.
        /// </exception>
        public void ExecuteReader(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection, Action<IDataReader> readContext)
        {
            using (var command = this.CreateCommand(commandText, connection))
            {
                command.Parameters.AddMany(parameters);

                this.ExecuteReader(command, readContext);
            }
        }

        /// <summary>
        /// Executes the reader command, running the context given.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="readContext">The read context.</param>
        /// <param name="errorContext">The error context.</param>
        public void ExecuteReader(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection, Action<IDataReader> readContext, Action<DatabaseTowelException> errorContext)
        {
            try
            {
                this.ExecuteReader(commandText, parameters, connection, readContext);
            }
            catch (DatabaseTowelException ex)
            {
                errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the reader command, running the context given, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="readContext">The read context.</param>
        /// <exception cref="DatabaseTowelException">
        /// The command parameter is required.
        /// or
        /// The readContext parameter is required.
        /// or
        /// Failed to successfully execute the reader command.
        /// </exception>
        public async Task ExecuteReaderAsync(IDbCommand command, Func<IDataReader, Task> readContext)
        {
            if (command == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The command parameter is required.", new ArgumentNullException("command"));
            }

            if (readContext == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The readContext parameter is required.", new ArgumentNullException("readContext"));
            }

            try
            {
                // If we can't cast as DbCommand, then we have to do things synchronously since the async operations aren't on the interface (thanks Microsoft!).
                // This really should be during testing only when we mock it out.
                if (command as DbCommand == null)
                {
                    using (var reader = command.ExecuteReader())
                    {
                        await readContext(reader);
                    }
                }
                else
                {
                    using (var reader = await (command as DbCommand).ExecuteReaderAsync())
                    {
                        await readContext(reader);
                    }
                }
            }
            catch (DbException ex)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.CommandExecuteFailed, "Failed to successfully execute the command.", ex);
            }
        }

        /// <summary>
        /// Executes the reader command, running the context given, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="readContext">The read context.</param>
        /// <param name="errorContext">The error context.</param>
        public async Task ExecuteReaderAsync(IDbCommand command, Func<IDataReader, Task> readContext, Func<DatabaseTowelException, Task> errorContext)
        {
            try
            {
                await this.ExecuteReaderAsync(command, readContext);
            }
            catch (DatabaseTowelException ex)
            {
                await errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the reader command, running the context given, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="readContext">The read context.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader command.
        /// </exception>
        public async Task ExecuteReaderAsync(string commandText, IEnumerable<DbParameter> parameters, Func<IDataReader, Task> readContext)
        {
            await this.ExecuteSqlAsync(async connection => await this.ExecuteReaderAsync(commandText, parameters, connection, readContext));
        }

        /// <summary>
        /// Executes the reader command, running the context given, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="readContext">The read context.</param>
        /// <param name="errorContext">The error context.</param>
        public async Task ExecuteReaderAsync(string commandText, IEnumerable<DbParameter> parameters, Func<IDataReader, Task> readContext, Func<DatabaseTowelException, Task> errorContext)
        {
            try
            {
                await this.ExecuteReaderAsync(commandText, parameters, readContext);
            }
            catch (DatabaseTowelException ex)
            {
                await errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the reader command, running the context given, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="readContext">The read context.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader command.
        /// </exception>
        public async Task ExecuteReaderAsync(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<IDataReader, Task> readContext)
        {
            using (var command = this.CreateCommand(commandText, connection))
            {
                command.Parameters.AddMany(parameters);

                await this.ExecuteReaderAsync(command, readContext);
            }
        }

        /// <summary>
        /// Executes the reader command, running the context given, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="readContext">The read context.</param>
        /// <param name="errorContext">The error context.</param>
        public async Task ExecuteReaderAsync(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<IDataReader, Task> readContext, Func<DatabaseTowelException, Task> errorContext)
        {
            try
            {
                await this.ExecuteReaderAsync(commandText, parameters, connection, readContext);
            }
            catch (DatabaseTowelException ex)
            {
                await errorContext(ex);
            }
        }
    }
}