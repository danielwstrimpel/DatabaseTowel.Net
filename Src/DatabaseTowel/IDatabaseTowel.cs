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
    public interface IDatabaseTowel : IExecuteNonQuery, IExecuteNonQueryStoredProcedure, IExecuteReader, IExecuteReaderStoredProcedure, IExecuteScalar, IExecuteScalarStoredProcedure, IExecuteSql, IExecuteSqlTransaction
    {
        /// <summary>
        /// Creates the connection in an unopened state.
        /// </summary>
        /// <returns>
        /// The connection in an unopened state.
        /// </returns>
        IDbConnection CreateConnection();

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// The command.
        /// </returns>
        IDbCommand CreateCommand(string commandText, IDbConnection connection);

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <param name="databaseType">Type of the database.</param>
        /// <returns>
        /// The parameter.
        /// </returns>
        DbParameter CreateParameter(string parameterName, object value, DbType databaseType);

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
