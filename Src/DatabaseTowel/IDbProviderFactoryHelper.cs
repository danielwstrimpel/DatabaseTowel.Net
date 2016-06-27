namespace DatabaseTowel
{
    using System.Data;
    using System.Data.Common;

    /// <summary>
    /// An interface to abstract the usage of Database Provider Factory features.
    /// </summary>
    public interface IDbProviderFactoryHelper
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
    }
}
