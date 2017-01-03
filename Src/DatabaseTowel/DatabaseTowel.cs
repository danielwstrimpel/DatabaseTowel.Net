namespace DatabaseTowel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// A class that abstracts database access and makes it easier to work with (DRY and testable code).
    /// </summary>
    public partial class DatabaseTowel : IDatabaseTowel
    {
        private readonly IDbProviderFactoryHelper databaseProviderFactoryHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseTowel" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="providerName">Name of the provider.</param>
        public DatabaseTowel(string connectionString, string providerName)
            : this(new DbProviderFactoryHelper(connectionString, providerName))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseTowel" /> class.
        /// </summary>
        /// <param name="databaseProviderFactoryHelper"><see cref="IDbProviderFactoryHelper" /> used by the database helper.</param>
        /// <exception cref="DatabaseTowelException">The database provider factory helper is required.</exception>
        public DatabaseTowel(IDbProviderFactoryHelper databaseProviderFactoryHelper)
        {
            if (databaseProviderFactoryHelper == null)
            {
                throw new DatabaseTowelException(
                    DatabaseTowelExceptionType.InvalidArgument,
                    "The database provider factory helper is required.",
                    new ArgumentNullException("databaseProviderFactoryHelper"));
            }

            this.databaseProviderFactoryHelper = databaseProviderFactoryHelper;
        }
        
        /// <summary>
        /// Creates the connection in an unopened state.
        /// </summary>
        /// <returns>
        /// The connection in an unopened state.
        /// </returns>
        public IDbConnection CreateConnection()
        {
            return this.databaseProviderFactoryHelper.CreateConnection();
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
            return this.databaseProviderFactoryHelper.CreateCommand(commandText, connection);
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
            return this.databaseProviderFactoryHelper.CreateParameter(parameterName, value, databaseType);
        }
        
        /// <summary>
        /// Loads the contents of the data reader into a data table.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>
        /// A data table loaded with the contents of the data reader.
        /// </returns>
        public DataTable DataReaderToDataTable(IDataReader dataReader)
        {
            if (dataReader == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The dataReader parameter is required.", new ArgumentNullException("dataReader"));
            }

            var dataTable = new DataTable();
            dataTable.Locale = CultureInfo.InvariantCulture;
            dataTable.Load(dataReader);

            return dataTable;            
        }

        /// <summary>
        /// Loads the contents of the data reader into a data table, asynchronously.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>
        /// A data table loaded with the contents of the data reader.
        /// </returns>
        public async Task<DataTable> DataReaderToDataTableAsync(IDataReader dataReader)
        {
            if (dataReader == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The dataReader parameter is required.", new ArgumentNullException("dataReader"));
            }

            // If we can't cast as DbDataReader, then we have to do things synchronously since the async operations aren't on the interface (thanks Microsoft!).
            // This really should be during testing only when we mock it out.
            if (dataReader as DbDataReader == null)
            {
                return this.DataReaderToDataTable(dataReader);
            }

            var columns = dataReader.GetSchemaTable().Rows.OfType<DataRow>().Select(dataRow => new DataColumn
            {
                ColumnName = dataRow[0].ToString(),
                DataType = Type.GetType(dataRow["DataType"].ToString())
            }).ToList();

            var dataTable = new DataTable();
            dataTable.Columns.AddRange(columns.ToArray());

            while (await(dataReader as DbDataReader).ReadAsync())
            {
                var dataRow = dataTable.NewRow();

                columns.ForEach(c => { dataRow[c.ColumnName] = dataReader[c.ColumnName]; });

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }
    }
}