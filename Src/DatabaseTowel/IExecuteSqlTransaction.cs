namespace DatabaseTowel
{
    using System;
    using System.Data;
    using System.Threading.Tasks;

    public interface IExecuteSqlTransaction
    {
        /// <summary>
        /// Executes the SQL transaction. If an exception is thrown in the process of opening the connection or executing the context,
        /// the transaction will not successfully complete and thus will rollback.
        /// </summary>
        /// <param name="context">The context to execute after the transaction and connection are opened.</param>
        /// <exception cref="DatabaseTowelException">
        /// The transaction failed to complete.
        /// or
        /// The transaction failed to successfully run.
        /// </exception>
        void ExecuteSqlTransaction(Action<IDbConnection> context);

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
        /// <exception cref="DatabaseTowelException">
        /// The transaction failed to complete.
        /// or
        /// The transaction failed to successfully run.
        /// </exception>
        Task ExecuteSqlTransactionAsync(Func<IDbConnection, Task> context);

        /// <summary>
        /// Executes the SQL transaction, asynchronously. If an exception is thrown in the process of opening the connection or executing
        /// the context, the transaction will not successfully complete and thus will rollback.
        /// </summary>
        /// <param name="context">The context to execute after the transaction and connection are opened.</param>
        /// <param name="errorContext">The context to execute after an error occurs.</param>
        Task ExecuteSqlTransactionAsync(Func<IDbConnection, Task> context, Action<DatabaseTowelException> errorContext);
    }
}
