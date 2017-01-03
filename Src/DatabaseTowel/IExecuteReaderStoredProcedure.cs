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
        /// Executes the reader stored procedure, returning the results as a data table.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The result of execution of the reader stored procedure.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader stored procedure.
        /// </exception>
        DataTable ExecuteReaderStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters);

        /// <summary>
        /// Executes the reader stored procedure, returning the results as a data table.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the reader stored procedure.
        /// </returns>
        DataTable ExecuteReaderStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, Func<DatabaseTowelException, DataTable> errorContext);

        /// <summary>
        /// Executes the reader stored procedure, returning the results as a data table.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// The result of execution of the reader stored procedure.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader stored procedure.
        /// </exception>
        DataTable ExecuteReaderStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection);

        /// <summary>
        /// Executes the reader stored procedure, returning the results as a data table.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// The result of execution of the reader stored procedure.
        /// </returns>
        DataTable ExecuteReaderStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<DatabaseTowelException, DataTable> errorContext);

        /// <summary>
        /// Executes the reader stored procedure, returning the results as a data table, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The result of execution of the reader stored procedure.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader stored procedure.
        /// </exception>
        Task<DataTable> ExecuteReaderStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters);

        /// <summary>
        /// Executes the reader stored procedure, returning the results as a data table, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        /// <returns>
        /// The result of execution of the reader stored procedure.
        /// </returns>
        Task<DataTable> ExecuteReaderStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, Func<DatabaseTowelException, Task<DataTable>> errorContext);

        /// <summary>
        /// Executes the reader stored procedure, returning the results as a data table, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// The result of execution of the reader stored procedure.
        /// </returns>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the reader stored procedure.
        /// </exception>
        Task<DataTable> ExecuteReaderStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection);

        /// <summary>
        /// Executes the reader stored procedure, returning the results as a data table, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// The result of execution of the reader stored procedure.
        /// </returns>
        Task<DataTable> ExecuteReaderStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<DatabaseTowelException, Task<DataTable>> errorContext);
    }
}
