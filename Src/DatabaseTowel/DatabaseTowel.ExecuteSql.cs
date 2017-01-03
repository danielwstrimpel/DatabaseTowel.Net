namespace DatabaseTowel
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;

    public partial class DatabaseTowel : IDatabaseTowel
    {
        /// <summary>
        /// Executes the SQL.
        /// </summary>
        /// <param name="context">The context to execute after the connection is opened.</param>
        /// <exception cref="DatabaseTowelException">
        /// The context was not supplied.
        /// Or
        /// The connection failed to be opened.
        /// Or
        /// Exception occurred in context.
        /// </exception>
        public void ExecuteSql(Action<IDbConnection> context)
        {
            if (context == null)
            {
                var ex = new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The context parameter is required.", new ArgumentNullException("context"));
            }

            using (var connection = this.CreateConnection())
            {
                try
                {
                    connection.Open();
                }
                catch (DbException ex)
                {
                    throw new DatabaseTowelException(DatabaseTowelExceptionType.ConnectionOpenFailed, "Failed to successfully open the connection.", ex);
                }

                context(connection);
            }
        }
        
        /// <summary>
        /// Executes the SQL.
        /// </summary>
        /// <param name="context">The context to execute after the connection is opened.</param>
        /// <param name="errorContext">The context to execute after an error occurs.</param>
        public void ExecuteSql(Action<IDbConnection> context, Action<DatabaseTowelException> errorContext)
        {
            try
            {
                this.ExecuteSql(context);
            }
            catch (DatabaseTowelException ex)
            {
                errorContext(ex);
            }
        }

        /// <summary>
        /// Executes the SQL, asynchronously.
        /// </summary>
        /// <param name="context">The context to execute after the connection is opened.</param>
        /// <param name="errorContext">The context to execute after an error occurs.</param>
        /// <returns>Asynchronous task to execute query.</returns>
        /// <exception cref="DatabaseTowelException">
        /// The context was not supplied.
        /// Or
        /// The connection failed to be opened.
        /// Or
        /// Exception occurred in context.
        /// </exception>
        public async Task ExecuteSqlAsync(Func<IDbConnection, Task> context)
        {
            if (context == null)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.InvalidArgument, "The context parameter is required.", new ArgumentNullException("context"));
            }

            using (var connection = this.CreateConnection())
            {
                try
                {
                    // If we can't cast as DbConnection, then we have to do things synchronously since the async operations aren't on the interface (thanks Microsoft!).
                    // This really should be during testing only when we mock it out.
                    if (connection as DbConnection == null)
                    {
                        connection.Open();
                    }
                    else
                    {
                        await(connection as DbConnection).OpenAsync();
                    }
                }
                catch (DbException ex)
                {
                    throw new DatabaseTowelException(DatabaseTowelExceptionType.ConnectionOpenFailed, "Failed to successfully open the connection.", ex);
                }

                await context(connection);
            }
        }

        /// <summary>
        /// Executes the SQL, asynchronously.
        /// </summary>
        /// <param name="context">The context to execute after the connection is opened.</param>
        /// <param name="errorContext">The context to execute after an error occurs.</param>
        public async Task ExecuteSqlAsync(Func<IDbConnection, Task> context, Action<DatabaseTowelException> errorContext)
        {
            try
            {
                await this.ExecuteSqlAsync(context);
            }
            catch (DatabaseTowelException ex)
            {
                errorContext(ex);
            }
        }
    }
}