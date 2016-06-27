namespace DatabaseTowel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;

    /// <summary>
    /// An interface to abstract database access and make it easier to work with.
    /// </summary>
    public interface IDatabaseTowel
    {
        /// <summary>
        /// Gets the database provider factory helper.
        /// </summary>
        /// <returns></returns>
        /// <value>Instance of the <see cref="IDbProviderFactoryHelper"/></value>
        IDbProviderFactoryHelper DatabaseProviderFactoryHelper { get; }
        
        /// <summary>
        /// Executes the SQL.
        /// </summary>
        /// <param name="context">The context to execute after the connection is opened.</param>
        /// <param name="errorContext">The context to execute after an error occurs.</param>
        /// <exception cref="DatabaseTowelException">
        /// The connection failed to be opened or a DatabaseException was thrown during execution of the context.
        /// </exception>
        void ExecuteSql(Action<IDbConnection> context, Action<DatabaseTowelException> errorContext);

        /// <summary>
        /// Executes the SQL, asynchronously.
        /// </summary>
        /// <param name="context">The context to execute after the connection is opened.</param>
        /// <param name="errorContext">The context to execute after an error occurs.</param>
        /// <exception cref="DatabaseTowelException">
        /// The connection failed to be opened or a DatabaseException was thrown during execution of the context.
        /// </exception>
        /// <returns>Asynchronous task execute query.</returns>
        Task ExecuteSqlAsync(Func<IDbConnection, Task> context, Action<DatabaseTowelException> errorContext);

        /// <summary>
        /// Executes the SQL transaction. If an exception is thrown in the process of opening the connection or executing the context,
        /// the transaction will not successfully complete and thus will rollback.
        /// </summary>
        /// <param name="context">The context to execute after the transaction and connection are opened.</param>
        /// <param name="errorContext">The context to execute after an error occurs.</param>
        void ExecuteSqlTransaction(Action<IDbConnection> context, Action<DatabaseTowelException> errorContext);

        /// <summary>
        /// Executes the SQL transaction, asynchronously. If an exception is thrown in the process of opening the connection or executing
        /// the context, the transaction will not successfully complete and thus will rollback.
        /// </summary>
        /// <param name="context">The context to execute after the transaction and connection are opened.</param>
        /// <param name="errorContext">The context to execute after an error occurs.</param>
        /// <returns>Asynchronous task to execute query inside transaction.</returns>
        Task ExecuteSqlTransactionAsync(Func<IDbConnection, Task> context, Action<DatabaseTowelException> errorContext);

        /// <summary>
        /// Executes the scalar command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="DatabaseTowelException">Failed to successfully execute the command.</exception>
        /// <returns>
        /// The result of execution of the scalar command.
        /// </returns>
        object ExecuteScalar(IDbCommand command);

        /// <summary>
        /// Executes the scalar command, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="DatabaseTowelException">Failed to successfully execute the command.</exception>
        /// <returns>
        /// The result of execution of the scalar command.
        /// </returns>
        Task<object> ExecuteScalarAsync(IDbCommand command);

        /// <summary>
        /// Executes the non query command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="DatabaseTowelException">Failed to successfully execute the command.</exception>
        void ExecuteNonQuery(IDbCommand command);

        /// <summary>
        /// Executes the non query command, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="DatabaseTowelException">Failed to successfully execute the command.</exception>
        /// <returns>Asynchronous task for Execute Non Query.</returns>
        Task ExecuteNonQueryAsync(IDbCommand command);

        /// <summary>
        /// Executes the command, opening a data reader.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="context">The context.</param>
        /// <exception cref="DatabaseTowelException">Failed to successfully execute the reader.</exception>
        void ExecuteReader(IDbCommand command, Action<IDataReader> context);

        /// <summary>
        /// Executes the command, opening a data reader, asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="context">The context.</param>
        /// <exception cref="DatabaseTowelException">Failed to successfully execute the reader.</exception>
        /// <returns>Asynchronous task for Execute Reader.</returns>
        Task ExecuteReaderAsync(IDbCommand command, Func<IDataReader, Task> context);

        /// <summary>
        /// Executes the stored procedure.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// A data table containing the results of the execution of the reader.
        /// </returns>
        DataTable ExecuteStoredProcedure(string storedProcedure, IEnumerable<DbParameter> parameters, Action<DatabaseTowelException> errorContext);

        /// <summary>
        /// Executes the stored procedure, asynchronously.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// A data table containing the results of the execution of the reader.
        /// </returns>
        Task<DataTable> ExecuteStoredProcedureAsync(string storedProcedure, IEnumerable<DbParameter> parameters, Action<DatabaseTowelException> errorContext);

        /// <summary>
        /// Executes the scalar stored procedure.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of the execution of the scalar stored procedure.
        /// </returns>
        object ExecuteScalarStoredProcedure(string storedProcedure, IEnumerable<DbParameter> parameters, Action<DatabaseTowelException> errorContext);

        /// <summary>
        /// Executes the scalar stored procedure, asynchronously.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of the execution of the scalar stored procedure.
        /// </returns>
        Task<object> ExecuteScalarStoredProcedureAsync(string storedProcedure, IEnumerable<DbParameter> parameters, Action<DatabaseTowelException> errorContext);

        /// <summary>
        /// Loads the contents of the data reader into a data table.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>
        /// A data table loaded with the contents of the data reader.
        /// </returns>
        DataTable DataReaderToDataTable(IDataReader dataReader);

        /// <summary>
        /// Loads the contents of the data reader into a data table, asynchronously.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>
        /// A data table loaded with the contents of the data reader.
        /// </returns>
        Task<DataTable> DataReaderToDataTableAsync(IDataReader dataReader);
    }
}
