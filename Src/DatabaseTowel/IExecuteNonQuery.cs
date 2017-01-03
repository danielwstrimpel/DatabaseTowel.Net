namespace DatabaseTowel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;

    public interface IExecuteNonQuery
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
        void ExecuteNonQuery(IDbCommand command);

        /// <summary>
        /// Executes the non query command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="errorContext">The error context.</param>
        void ExecuteNonQuery(IDbCommand command, Action<DatabaseTowelException> errorContext);

        /// <summary>
        /// Executes the non query command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the command.
        /// </exception>
        void ExecuteNonQuery(string commandText, IEnumerable<DbParameter> parameters);

        /// <summary>
        /// Executes the non query command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        void ExecuteNonQuery(string commandText, IEnumerable<DbParameter> parameters, Action<DatabaseTowelException> errorContext);

        /// <summary>
        /// Executes the non query command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the command.
        /// </exception>
        void ExecuteNonQuery(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection);

        /// <summary>
        /// Executes the non query command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="errorContext">The error context.</param>
        void ExecuteNonQuery(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection, Action<DatabaseTowelException> errorContext);

        /// <summary>
        /// Executes the non query command, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="DatabaseTowelException">
        /// The command parameter is required.
        /// or
        /// Failed to successfully execute the command.
        /// </exception>
        Task ExecuteNonQueryAsync(IDbCommand command);

        /// <summary>
        /// Executes the non query command, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="errorContext">The error context.</param>
        Task ExecuteNonQueryAsync(IDbCommand command, Func<DatabaseTowelException, Task> errorContext);

        /// <summary>
        /// Executes the non query command, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the command.
        /// </exception>
        Task ExecuteNonQueryAsync(string commandText, IEnumerable<DbParameter> parameters);

        /// <summary>
        /// Executes the non query command, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        Task ExecuteNonQueryAsync(string commandText, IEnumerable<DbParameter> parameters, Func<DatabaseTowelException, Task> errorContext);

        /// <summary>
        /// Executes the non query command, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the command.
        /// </exception>
        Task ExecuteNonQueryAsync(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection);

        /// <summary>
        /// Executes the non query command, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="errorContext">The error context.</param>
        Task ExecuteNonQueryAsync(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<DatabaseTowelException, Task> errorContext);
    }
}
