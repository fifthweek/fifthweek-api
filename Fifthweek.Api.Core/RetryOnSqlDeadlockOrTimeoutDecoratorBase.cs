namespace Fifthweek.Api.Core
{
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public abstract class RetryOnSqlDeadlockOrTimeoutDecoratorBase
    {
        public const int MaxTries = 5;

        public const int MaxRetryInterval = 100;

        public const int SqlDeadlockErrorCode = 1205;

        public const int SqlTimeoutErrorCode = -2;
        
        private static readonly Random Random = new Random();

        public async Task<TResult> HandleAsync<TResult>(Func<Task<TResult>> handleCommandAsync)
        {
            int tryCount = 1;
            while (true)
            {
                try
                {
                    return await handleCommandAsync();
                }
                catch (SqlException sqlEx)
                {
                    if (tryCount == MaxTries)
                    {
                        throw new Exception("Maximum retries reached.", sqlEx);
                    }

                    var errorPostfix = " on attempt " + (tryCount + 1) + ". Will try again.";
                    switch (sqlEx.Number)
                    {
                        case SqlDeadlockErrorCode:
                            Trace.TraceWarning(this.GetCommandOrQueryType().Name + " deadlocked" + errorPostfix);
                            break;

                        case SqlTimeoutErrorCode:
                            Trace.TraceWarning(this.GetCommandOrQueryType().Name + " timed out" + errorPostfix);
                            break;

                        default:
                            throw;
                    }
                }

                await Task.Delay(Random.Next(MaxRetryInterval));
                ++tryCount;
            }
        }

        protected abstract Type GetCommandOrQueryType();
    }
}