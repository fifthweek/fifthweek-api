namespace Fifthweek.Api.Core
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.SqlServer;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class RetryOnRecoverableDatabaseErrorDecoratorBase
    {
        private readonly DbExecutionStrategy executionStrategy;

        protected RetryOnRecoverableDatabaseErrorDecoratorBase()
        {
            this.executionStrategy = new CustomExecutionStrategy(this);
        }

        public async Task<TResult> HandleAsync<TResult>(Func<Task<TResult>> handleCommandAsync)
        {
            return await this.executionStrategy.ExecuteAsync(
                handleCommandAsync, 
                CancellationToken.None);
        }

        protected abstract Type GetCommandOrQueryType();

        private class CustomExecutionStrategy : SqlAzureExecutionStrategy
        {
            public const int SqlDeadlockErrorCode = 1205;

            public const int SqlTimeoutErrorCode = -2;

            private readonly RetryOnRecoverableDatabaseErrorDecoratorBase parent;

            public CustomExecutionStrategy(RetryOnRecoverableDatabaseErrorDecoratorBase parent)
            {
                this.parent = parent;
            }

            protected override bool ShouldRetryOn(Exception exception)
            {
                bool shouldRetry = false;

                var sqlException = exception as SqlException;
                if (sqlException != null)
                {
                    foreach (SqlError error in sqlException.Errors)
                    {
                        switch (error.Number)
                        {
                            case SqlTimeoutErrorCode:
                            case SqlDeadlockErrorCode:
                                shouldRetry = true;
                                break;
                        }
                    }
                }

                shouldRetry = shouldRetry || base.ShouldRetryOn(exception);

                if (shouldRetry)
                {
                    Trace.TraceWarning(this.parent.GetCommandOrQueryType().Name + " retrying: " + exception);
                }

                return shouldRetry;
            }
        }
    }
}