namespace Fifthweek.Api.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Entity.Core;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

    public abstract class RetryOnTransientErrorDecoratorBase
    {
        public const int SqlDeadlockErrorCode = 1205;

        public const int SqlTimeoutErrorCode = -2;

        public const int MaxRetryCount = 5;

        public static readonly TimeSpan MaxDelay = TimeSpan.FromSeconds(5);
        
        private readonly IExceptionHandler exceptionHandler;

        private readonly string commandOrQueryName;

        private readonly int maxRetryCount;

        private readonly TimeSpan maxDelay;

        private int retryCount;

        protected RetryOnTransientErrorDecoratorBase(IExceptionHandler exceptionHandler, string commandOrQueryName, int maxRetryCount, TimeSpan maxDelay)
        {
            this.exceptionHandler = exceptionHandler;
            this.commandOrQueryName = commandOrQueryName;
            this.maxRetryCount = maxRetryCount;
            this.maxDelay = maxDelay;
        }

        public int RetryCount
        {
            get
            {
                return this.retryCount;
            }
        }

        public static RetryStrategy CreateRetryStrategy(int retryCount, TimeSpan maxDelay)
        {
            return new ExponentialBackoff(
                retryCount,
                TimeSpan.Zero,
                maxDelay,
                TimeSpan.FromMilliseconds(maxDelay.TotalMilliseconds / 3));
        }

        public async Task<TResult> HandleAsync<TResult>(Func<Task<TResult>> handleCommandAsync)
        {
            var policy =
                new RetryPolicy(
                    CustomTransientErrorDetectionStrategy.Instance,
                    CreateRetryStrategy(this.maxRetryCount, this.maxDelay));

            policy.Retrying += this.OnRetry;

            try
            {
                var result = await policy.ExecuteAsync(handleCommandAsync, CancellationToken.None);

                if (this.retryCount > 0)
                {
                    this.ReportRetriesOccured(string.Format("{0} succeeded on attempt {1}.", this.commandOrQueryName, this.retryCount + 1));
                }

                return result;
            }
            catch (Exception t)
            {
                if (this.retryCount == this.maxRetryCount && CustomTransientErrorDetectionStrategy.Instance.IsTransient(t))
                {
                    this.TraceFailedAttempt(this.retryCount + 1, t);
                    throw new RetryLimitExceededException(string.Format("{0} was attempted {1} times before exceeding retry limit.", this.commandOrQueryName, this.retryCount + 1), t);
                }

                if (this.retryCount > 0)
                {
                    this.ReportRetriesOccured(string.Format("{0} failed with a non-transient error on attempt {1}.", this.commandOrQueryName, this.retryCount + 1));
                }

                throw;
            }
        }

        private void OnRetry(object sender, RetryingEventArgs e)
        {
            ++this.retryCount;
            this.TraceFailedAttempt(this.retryCount, e.LastException);
        }

        private void TraceFailedAttempt(int attemptNumber, Exception exception)
        {
            Trace.TraceWarning("{0} failed with a transient error (attempt {1} of {2}): {3}", this.commandOrQueryName, attemptNumber, this.maxRetryCount + 1, exception);
        }

        private void ReportRetriesOccured(string message)
        {
            this.exceptionHandler.ReportExceptionAsync(new RetriesOccuredException(message));
        }

        private class CustomTransientErrorDetectionStrategy : ITransientErrorDetectionStrategy
        {
            public static readonly CustomTransientErrorDetectionStrategy Instance = new CustomTransientErrorDetectionStrategy();

            private static readonly SqlDatabaseTransientErrorDetectionStrategy SqlDatabaseStrategy = new SqlDatabaseTransientErrorDetectionStrategy();
           
            private static readonly StorageTransientErrorDetectionStrategy StorageStrategy = new StorageTransientErrorDetectionStrategy();

            public bool IsTransient(Exception outerException)
            {
                if (outerException == null)
                {
                    return false;
                }

                var isTransient = false;
                foreach (var exception in this.Flatten(outerException))
                {
                    isTransient = this.IsTransientInner(exception);

                    if (isTransient)
                    {
                        break;
                    }
                }

                return isTransient;
            }

            private bool IsTransientInner(Exception exception)
            {
                return this.CustomStrategyIsTransient(exception) || SqlDatabaseStrategy.IsTransient(exception) || StorageStrategy.IsTransient(exception);
            }

            private bool CustomStrategyIsTransient(Exception exception)
            {
                var sqlException = exception as SqlException;
                if (sqlException != null)
                {
                    foreach (SqlError error in sqlException.Errors)
                    {
                        switch (error.Number)
                        {
                            case RetryOnTransientErrorDecoratorBase.SqlTimeoutErrorCode:
                            case RetryOnTransientErrorDecoratorBase.SqlDeadlockErrorCode:
                                return true;
                        }
                    }
                }

                var entityException = exception as EntityException;
                if (entityException != null)
                {
                    if (entityException.ToString().Contains("transient failure"))
                    {
                        return true;
                    }
                }

                var optimisticConcurrencyException = exception as OptimisticConcurrencyException;
                if (optimisticConcurrencyException != null)
                {
                    return true;
                }

                return false;
            }

            private IReadOnlyList<Exception> Flatten(Exception exception)
            {
                exception.AssertNotNull("exception");

                var output = new List<Exception>();
                this.Flatten(exception, output);
                return output;
            }

            private void Flatten(Exception exception, List<Exception> output)
            {
                output.Add(exception);

                var aggregateException = exception as AggregateException;
                if (aggregateException != null && aggregateException.InnerExceptions != null)
                {
                    foreach (var child in aggregateException.InnerExceptions)
                    {
                        if (child != null)
                        {
                            this.Flatten(child, output);
                        }
                    }
                }
                else if (exception.InnerException != null)
                {
                    this.Flatten(exception.InnerException, output);
                }
            }
        }
    }
}