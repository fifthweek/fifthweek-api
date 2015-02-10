namespace Fifthweek.Api.Core
{
    using System;
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

        private readonly ITransientErrorDetectionStrategy transientErrorDetectionStrategy;

        private readonly string commandOrQueryName;

        private readonly int maxRetryCount;

        private readonly TimeSpan maxDelay;

        private int retryCount;

        protected RetryOnTransientErrorDecoratorBase(IExceptionHandler exceptionHandler, ITransientErrorDetectionStrategy transientErrorDetectionStrategy, string commandOrQueryName, int maxRetryCount, TimeSpan maxDelay)
        {
            this.exceptionHandler = exceptionHandler;
            this.transientErrorDetectionStrategy = transientErrorDetectionStrategy;
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
                    this.transientErrorDetectionStrategy,
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
                if (this.retryCount == this.maxRetryCount && this.transientErrorDetectionStrategy.IsTransient(t))
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
    }
}