namespace DatabaseTowel
{
    using System;
    using System.Data;
    using System.Threading.Tasks;

    public interface IExecuteSql
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
        void ExecuteSql(Action<IDbConnection> context);

        /// <summary>
        /// Executes the SQL.
        /// </summary>
        /// <param name="context">The context to execute after the connection is opened.</param>
        /// <param name="errorContext">The context to execute after an error occurs.</param>
        void ExecuteSql(Action<IDbConnection> context, Action<DatabaseTowelException> errorContext);

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
        Task ExecuteSqlAsync(Func<IDbConnection, Task> context);

        /// <summary>
        /// Executes the SQL, asynchronously.
        /// </summary>
        /// <param name="context">The context to execute after the connection is opened.</param>
        /// <param name="errorContext">The context to execute after an error occurs.</param>
        Task ExecuteSqlAsync(Func<IDbConnection, Task> context, Action<DatabaseTowelException> errorContext);
    }
}
