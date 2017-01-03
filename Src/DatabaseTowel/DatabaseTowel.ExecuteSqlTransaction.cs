namespace DatabaseTowel
{
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using System.Transactions;

    public partial class DatabaseTowel : IDatabaseTowel
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
        public void ExecuteSqlTransaction(Action<IDbConnection> context)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    this.ExecuteSql(context);

                    try
                    {
                        transactionScope.Complete();
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw new DatabaseTowelException(DatabaseTowelExceptionType.TransactionCompleteFailed, "The transaction failed to complete.", ex);
                    }
                }
            }
            catch (TransactionException ex)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.TransactionFailed, "The transaction failed to successfully run.", ex);
            }
        }

        /// <summary>
        /// Executes the SQL transaction. If an exception is thrown in the process of opening the connection or executing the context,
        /// the transaction will not successfully complete and thus will rollback.
        /// </summary>
        /// <param name="context">The context to execute after the transaction and connection are opened.</param>
        /// <param name="errorContext">The context to execute after an error occurs.</param>
        public void ExecuteSqlTransaction(Action<IDbConnection> context, Action<DatabaseTowelException> errorContext)
        {
            try
            {
                this.ExecuteSqlTransaction(context);
            }
            catch (DatabaseTowelException ex)
            {
                errorContext(ex);
            }
        }

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
        public async Task ExecuteSqlTransactionAsync(Func<IDbConnection, Task> context)
        {
            try
            {
                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await this.ExecuteSqlAsync(context);

                    try
                    {
                        transactionScope.Complete();
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw new DatabaseTowelException(DatabaseTowelExceptionType.TransactionCompleteFailed, "The transaction failed to complete.", ex);
                    }
                }
            }
            catch (TransactionException ex)
            {
                throw new DatabaseTowelException(DatabaseTowelExceptionType.TransactionFailed, "The transaction failed to successfully run.", ex);
            }
        }

        /// <summary>
        /// Executes the SQL transaction, asynchronously. If an exception is thrown in the process of opening the connection or executing
        /// the context, the transaction will not successfully complete and thus will rollback.
        /// </summary>
        /// <param name="context">The context to execute after the transaction and connection are opened.</param>
        /// <param name="errorContext">The context to execute after an error occurs.</param>
        public async Task ExecuteSqlTransactionAsync(Func<IDbConnection, Task> context, Action<DatabaseTowelException> errorContext)
        {
            try
            {
                await this.ExecuteSqlTransactionAsync(context);
            }
            catch (DatabaseTowelException ex)
            {
                errorContext(ex);
            }
        }
    }
}