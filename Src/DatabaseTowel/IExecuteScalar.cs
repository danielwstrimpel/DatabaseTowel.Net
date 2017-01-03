namespace DatabaseTowel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;
    
    public interface IExecuteScalar
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
        object ExecuteScalar(IDbCommand command);

        /// <summary>
        /// Executes the scalar command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the scalar command.
        /// </returns>
        object ExecuteScalar(IDbCommand command, Func<DatabaseTowelException, object> errorContext);

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
        object ExecuteScalar(string commandText, IEnumerable<DbParameter> parameters);

        /// <summary>
        /// Executes the scalar command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the scalar command.
        /// </returns>
        object ExecuteScalar(string commandText, IEnumerable<DbParameter> parameters, Func<DatabaseTowelException, object> errorContext);

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
        object ExecuteScalar(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection);

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
        object ExecuteScalar(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<DatabaseTowelException, object> errorContext);

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
        Task<object> ExecuteScalarAsync(IDbCommand command);

        /// <summary>
        /// Executes the scalar command, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the scalar command.
        /// </returns>
        Task<object> ExecuteScalarAsync(IDbCommand command, Func<DatabaseTowelException, Task<object>> errorContext);

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
        Task<object> ExecuteScalarAsync(string commandText, IEnumerable<DbParameter> parameters);

        /// <summary>
        /// Executes the scalar command, asynchronously.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the scalar command.
        /// </returns>
        Task<object> ExecuteScalarAsync(string commandText, IEnumerable<DbParameter> parameters, Func<DatabaseTowelException, Task<object>> errorContext);

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
        Task<object> ExecuteScalarAsync(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection);

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
        Task<object> ExecuteScalarAsync(string commandText, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<DatabaseTowelException, Task<object>> errorContext);
    }
}
