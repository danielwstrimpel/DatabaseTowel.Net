namespace DatabaseTowel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;

    public interface IExecuteNonQueryStoredProcedure
    {
        /// <summary>
        /// Executes the non query stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the non query stored procedure.
        /// </exception>
        void ExecuteNonQueryStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters);

        /// <summary>
        /// Executes the non query stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        void ExecuteNonQueryStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, Action<DatabaseTowelException> errorContext);

        /// <summary>
        /// Executes the non query stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the non query stored procedure.
        /// </exception>
        void ExecuteNonQueryStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection);

        /// <summary>
        /// Executes the non query stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        void ExecuteNonQueryStoredProcedure(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection, Action<DatabaseTowelException> errorContext);

        /// <summary>
        /// Executes the non query stored procedure, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the non query stored procedure.
        /// </exception>
        Task ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters);

        /// <summary>
        /// Executes the non query stored procedure, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorContext">The error context.</param>
        Task ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, Func<DatabaseTowelException, Task> errorContext);

        /// <summary>
        /// Executes the non query stored procedure, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        /// <exception cref="DatabaseTowelException">
        /// Failed to successfully execute the non query stored procedure.
        /// </exception>
        Task ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection);

        /// <summary>
        /// Executes the non query stored procedure, asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connection">The connection.</param>
        Task ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, IEnumerable<DbParameter> parameters, IDbConnection connection, Func<DatabaseTowelException, Task> errorContext);
    }
}
