namespace DatabaseTowel
{
    using System;
    using System.Data;
    using System.Data.Common;

    /// <summary>
    /// A class that abstracts the usage of Database Provider Factory features.
    /// </summary>
    public class DbProviderFactoryHelper : IDbProviderFactoryHelper
    {
        private readonly string connectionString;
        private readonly DbProviderFactory databaseProviderFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbProviderFactoryHelper" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="providerName">Name of the provider.</param>
        public DbProviderFactoryHelper(string connectionString, string providerName)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The connection string is required.", new ArgumentException("connectionString"));
            }

            if (string.IsNullOrEmpty(providerName))
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The provider name is required.", new ArgumentException("providerName"));
            }

            this.connectionString = connectionString;
            this.databaseProviderFactory = DbProviderFactories.GetFactory(providerName);
        }

        /// <summary>
        /// Creates the connection in an unopened state.
        /// </summary>
        /// <returns>
        /// The connection in an unopened state.
        /// </returns>
        public IDbConnection CreateConnection()
        {
            var connection = this.databaseProviderFactory.CreateConnection();
            connection.ConnectionString = this.connectionString;

            return connection;
        }

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// The command.
        /// </returns>
        public IDbCommand CreateCommand(string commandText, IDbConnection connection)
        {
            var command = this.databaseProviderFactory.CreateCommand();
            command.CommandText = commandText;
            command.Connection = connection as DbConnection;

            return command;
        }

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <param name="databaseType">Type of the database.</param>
        /// <returns>
        /// The parameter.
        /// </returns>
        public DbParameter CreateParameter(string parameterName, object value, DbType databaseType)
        {
            var parameter = this.databaseProviderFactory.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.DbType = databaseType;
            parameter.Value = value;

            return parameter;
        }
    }
}