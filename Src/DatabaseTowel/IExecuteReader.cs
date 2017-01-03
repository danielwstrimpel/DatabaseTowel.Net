namespace DatabaseTowel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;

    public interface IExecuteReader
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
        void ExecuteReader(IDbCommand command, Action<IDataReader> readContext);

        /// <summary>
        /// Executes the reader command, running the context given.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="readContext">The read context.</param>
        /// <param name="errorContext">The error context.</param>
        void ExecuteReader(IDbCommand command, Action<IDataReader> readContext, Action<DatabaseTowelException> errorContext);

        /// <summary>
        /// Executes the reader command, running the context given.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="readContext">The read context.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader command.
        /// </exception>
        void ExecuteReader(string commandText, IEnumerable<DbParameter> parameters, Action<IDataReader> readContext);

        /// <summary>
        /// Executes the reader command, running the context given.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="readContext">The read context.</param>
        /// <param name="errorContext">The error context.</param>
        void ExecuteReader(string commandText, IEnumerable<DbParameter> parameters, Action<IDataReader> readContext, Action<DatabaseTowelException> errorContext);

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
        void ExecuteReader(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection, Action<IDataReader> readContext);

        /// <summary>
        /// Executes the reader command, running the context given.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="readContext">The read context.</param>
        /// <param name="errorContext">The error context.</param>
        void ExecuteReader(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection, Action<IDataReader> readContext, Action<DatabaseTowelException> errorContext);

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
        Task ExecuteReaderAsync(IDbCommand command, Func<IDataReader, Task> readContext);

        /// <summary>
        /// Executes the reader command, running the context given, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="readContext">The read context.</param>
        /// <param name="errorContext">The error context.</param>
        Task ExecuteReaderAsync(IDbCommand command, Func<IDataReader, Task> readContext, Func<DatabaseTowelException, Task> errorContext);

        /// <summary>
        /// Executes the reader command, running the context given, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="readContext">The read context.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader command.
        /// </exception>
        Task ExecuteReaderAsync(string commandText, IEnumerable<DbParameter> parameters, Func<IDataReader, Task> readContext);

        /// <summary>
        /// Executes the reader command, running the context given, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="readContext">The read context.</param>
        /// <param name="errorContext">The error context.</param>
        Task ExecuteReaderAsync(string commandText, IEnumerable<DbParameter> parameters, Func<IDataReader, Task> readContext, Func<DatabaseTowelException, Task> errorContext);

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
        Task ExecuteReaderAsync(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<IDataReader, Task> readContext);

        /// <summary>
        /// Executes the reader command, running the context given, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="readContext">The read context.</param>
        /// <param name="errorContext">The error context.</param>
        Task ExecuteReaderAsync(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<IDataReader, Task> readContext, Func<DatabaseTowelException, Task> errorContext);
    }
}
