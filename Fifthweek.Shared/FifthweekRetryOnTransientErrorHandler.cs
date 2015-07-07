namespace Fifthweek.Shared
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

    public class FifthweekRetryOnTransientErrorHandler : IFifthweekRetryOnTransientErrorHandler
    {
        public const int SqlDeadlockErrorCode = 1205;

        public const int SqlTimeoutErrorCode = -2;

        public const int DefaultMaxRetryCount = 5;

        public const string DefaultTaskName = "Unnamed Task";

        public static readonly TimeSpan DefaultMaxDelay = TimeSpan.FromSeconds(5);
        
        private readonly IExceptionHandler exceptionHandler;

        private readonly ITransientErrorDetectionStrategy transientErrorDetectionStrategy;

        private int retryCount;

        public FifthweekRetryOnTransientErrorHandler(IExceptionHandler exceptionHandler, ITransientErrorDetectionStrategy transientErrorDetectionStrategy)
        {
            this.exceptionHandler = exceptionHandler;
            this.transientErrorDetectionStrategy = transientErrorDetectionStrategy;
            this.TaskName = DefaultTaskName;
            this.MaxRetryCount = DefaultMaxRetryCount;
            this.MaxDelay = DefaultMaxDelay;
        }

        public int RetryCount
        {
            get
            {
                return this.retryCount;
            }
        }

        public string TaskName { get; set; }

        public int MaxRetryCount { get; set; }

        public TimeSpan MaxDelay { get; set; }

        public static RetryStrategy CreateRetryStrategy(int retryCount, TimeSpan maxDelay)
        {
            return new ExponentialBackoff(
                retryCount,
                TimeSpan.Zero,
                maxDelay,
                TimeSpan.FromMilliseconds(maxDelay.TotalMilliseconds / 3));
        }

        public Task HandleAsync(Func<Task> action)
        {
            return this.HandleAsync(() => this.CallDecorated(action));
        }

        public async Task<TResult> HandleAsync<TResult>(Func<Task<TResult>> action)
        {
            var policy =
                new RetryPolicy(
                    this.transientErrorDetectionStrategy,
                    CreateRetryStrategy(this.MaxRetryCount, this.MaxDelay));

            policy.Retrying += this.OnRetry;

            try
            {
                var result = await policy.ExecuteAsync(action, CancellationToken.None);

                if (this.retryCount > 0)
                {
                    this.ReportRetriesOccured(string.Format("{0} succeeded on attempt {1}.", this.TaskName, this.retryCount + 1));
                }

                return result;
            }
            catch (Exception t)
            {
                if (this.retryCount == this.MaxRetryCount && this.transientErrorDetectionStrategy.IsTransient(t))
                {
                    this.TraceFailedAttempt(this.retryCount + 1, t);
                    throw new RetryLimitExceededException(string.Format("{0} was attempted {1} times before exceeding retry limit.", this.TaskName, this.retryCount + 1), t);
                }

                if (this.retryCount > 0)
                {
                    this.ReportRetriesOccured(string.Format("{0} failed with a non-transient error on attempt {1}.", this.TaskName, this.retryCount + 1));
                }

                throw;
            }
        }

        private async Task<bool> CallDecorated(Func<Task> action)
        {
            await action();
            return true;
        }

        private void OnRetry(object sender, RetryingEventArgs e)
        {
            ++this.retryCount;
            this.TraceFailedAttempt(this.retryCount, e.LastException);
        }

        private void TraceFailedAttempt(int attemptNumber, Exception exception)
        {
            Trace.TraceWarning("{0} failed with a transient error (attempt {1} of {2}): {3}", this.TaskName, attemptNumber, this.MaxRetryCount + 1, exception);
        }

        private void ReportRetriesOccured(string message)
        {
            this.exceptionHandler.ReportExceptionAsync(new RetriesOccuredException(message));
        }
   }
}