namespace Fifthweek.Api.Core
{
    using System;
    using System.Threading.Tasks;

    public interface IFifthweekRetryOnTransientErrorHandler
    {
        int RetryCount { get; }

        string TaskName { get; set; }

        int MaxRetryCount { get; set; }

        TimeSpan MaxDelay { get; set; }

        Task HandleAsync(Func<Task> action);

        Task<TResult> HandleAsync<TResult>(Func<Task<TResult>> action);
    }
}