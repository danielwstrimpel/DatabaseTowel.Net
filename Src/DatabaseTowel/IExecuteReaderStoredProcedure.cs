namespace DatabaseTowel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;

    public interface IExecuteReaderStoredProcedure
    {
        /// <summary>
        /// Executes the reader stored procedure, running the context given.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="readContext">The read context.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader stored procedure.
        /// </exception>
        void ExecuteReaderStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, Action<IDataReader> readContext);

        /// <summary>
        /// Executes the reader stored procedure, running the context given.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="readContext">The read context.</param>
        /// <param name="errorContext">The error context.</param>
        void ExecuteReaderStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, Action<IDataReader> readContext, Action<DatabaseTowelException> errorContext);

        /// <summary>
        /// Executes the reader stored procedure, running the context given.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="readContext">The read context.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader stored procedure.
        /// </exception>
        void ExecuteReaderStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection, Action<IDataReader> readContext);

        /// <summary>
        /// Executes the reader stored procedure, running the context given.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="readContext">The read context.</param>
        /// <param name="errorContext">The error context.</param>
        void ExecuteReaderStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection, Action<IDataReader> readContext, Action<DatabaseTowelException> errorContext);

        /// <summary>
        /// Executes the reader stored procedure, running the context given, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="readContext">The read context.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader stored procedure.
        /// </exception>
        Task ExecuteReaderStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, Func<IDataReader, Task> readContext);

        /// <summary>
        /// Executes the reader stored procedure, running the context given, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="readContext">The read context.</param>
        /// <param name="errorContext">The error context.</param>
        Task ExecuteReaderStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, Func<IDataReader, Task> readContext, Func<DatabaseTowelException, Task<DataTable>> errorContext);

        /// <summary>
        /// Executes the reader stored procedure, running the context given, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="readContext">The read context.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader stored procedure.
        /// </exception>
        Task ExecuteReaderStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<IDataReader, Task> readContext);

        /// <summary>
        /// Executes the reader stored procedure, running the context given, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="readContext">The read context.</param>
        /// <param name="errorContext">The error context.</param>
        Task ExecuteReaderStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<IDataReader, Task> readContext, Func<DatabaseTowelException, Task<DataTable>> errorContext);
    }
}
