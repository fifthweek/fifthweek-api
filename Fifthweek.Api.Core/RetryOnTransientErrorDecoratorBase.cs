namespace Fifthweek.Api.Core
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.SqlServer;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class RetryOnTransientErrorDecoratorBase
    {
        public const int SqlDeadlockErrorCode = 1205;

        public const int SqlTimeoutErrorCode = -2;

        public const int MaxRetryCount = 5;

        public static readonly TimeSpan MaxDelay = TimeSpan.FromSeconds(5);

        private readonly DbExecutionStrategy executionStrategy;

        protected RetryOnTransientErrorDecoratorBase(int maxRetryCount, TimeSpan maxDelay)
        {
            this.executionStrategy = new CustomExecutionStrategy(this, maxRetryCount, maxDelay);
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
            private readonly RetryOnTransientErrorDecoratorBase parent;

            public CustomExecutionStrategy(
                RetryOnTransientErrorDecoratorBase parent,
                int maxRetryCount,
                TimeSpan maxDelay)
                : base(maxRetryCount, maxDelay)
            {
                this.parent = parent;
            }

            protected override bool ShouldRetryOn(Exception exception)
            {
                exception = this.FindSqlOrTimeoutException(exception);

                if (exception == null)
                {
                    return false;
                }

                bool shouldRetry = false;

                var sqlException = exception as SqlException;
                if (sqlException != null)
                {
                    foreach (SqlError error in sqlException.Errors)
                    {
                        switch (error.Number)
                        {
                            case RetryOnTransientErrorDecoratorBase.SqlTimeoutErrorCode:
                            case RetryOnTransientErrorDecoratorBase.SqlDeadlockErrorCode:
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

            private Exception FindSqlOrTimeoutException(Exception exception)
            {
                var sqlException = exception as SqlException;
                if (sqlException != null)
                {
                    return sqlException;
                }

                var timeoutException = exception as TimeoutException;
                if (timeoutException != null)
                {
                    return timeoutException;
                }

                var aggregateException = exception as AggregateException;
                if (aggregateException != null)
                {
                    foreach (var child in aggregateException.InnerExceptions)
                    {
                        var childResult = this.FindSqlOrTimeoutException(child);

                        if (childResult != null)
                        {
                            return childResult;
                        }
                    }

                    return null;
                }

                if (exception.InnerException != null)
                {
                    return this.FindSqlOrTimeoutException(exception.InnerException);
                }

                return null;
            }
        }
    }
}