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
        /// Executes the command, returning the results as a data table.
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
        DataTable ExecuteReader(IDbCommand command);

        /// <summary>
        /// Executes the command, returning the results as a data table.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        DataTable ExecuteReader(IDbCommand command, Func<DatabaseTowelException, DataTable> errorContext);

        /// <summary>
        /// Executes the command, returning the results as a data table.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the command.
        /// </exception>
        DataTable ExecuteReader(string commandText, IEnumerable<DbParameter> parameters);

        /// <summary>
        /// Executes the command, returning the results as a data table.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        DataTable ExecuteReader(string commandText, IEnumerable<DbParameter> parameters, Func<DatabaseTowelException, DataTable> errorContext);

        /// <summary>
        /// Executes the command, returning the results as a data table.
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
        DataTable ExecuteReader(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection);

        /// <summary>
        /// Executes the command, returning the results as a data table.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        DataTable ExecuteReader(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<DatabaseTowelException, DataTable> errorContext);

        /// <summary>
        /// Executes the command, returning the results as a data table, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the command.
        /// </exception>
        Task<DataTable> ExecuteReaderAsync(IDbCommand command);

        /// <summary>
        /// Executes the command, returning the results as a data table, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        Task<DataTable> ExecuteReaderAsync(IDbCommand command, Func<DatabaseTowelException, Task<DataTable>> errorContext);

        /// <summary>
        /// Executes the command, returning the results as a data table, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the command.
        /// </exception>
        Task<DataTable> ExecuteReaderAsync(string commandText, IEnumerable<DbParameter> parameters);

        /// <summary>
        /// Executes the command, returning the results as a data table, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        Task<DataTable> ExecuteReaderAsync(string commandText, IEnumerable<DbParameter> parameters, Func<DatabaseTowelException, Task<DataTable>> errorContext);

        /// <summary>
        /// Executes the command, returning the results as a data table, asynchronously.
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
        Task<DataTable> ExecuteReaderAsync(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection);

        /// <summary>
        /// Executes the command, returning the results as a data table, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// The result of execution of the reader command.
        /// </returns>
        Task<DataTable> ExecuteReaderAsync(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<DatabaseTowelException, Task<DataTable>> errorContext);
    }
}
