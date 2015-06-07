namespace Fifthweek.Shared
{
    using System.Transactions;

    // http://blogs.msdn.com/b/dbrowne/archive/2010/06/03/using-new-transactionscope-considered-harmful.aspx
    public class TransactionScopeBuilder
    {
        public static TransactionScope CreateAsync()
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            };

            return new TransactionScope(
                TransactionScopeOption.Required, 
                transactionOptions, 
                TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}