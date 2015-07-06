namespace Fifthweek.Api.Core
{
    using System;
    using System.Threading.Tasks;

    public class RetryOnTransientErrorQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        private readonly IFifthweekRetryOnTransientErrorHandler fifthweekRetryOnTransientErrorHandler;

        private readonly IQueryHandler<TQuery, TResult> decorated;

        public RetryOnTransientErrorQueryHandlerDecorator(IFifthweekRetryOnTransientErrorHandler fifthweekRetryOnTransientErrorHandler, IQueryHandler<TQuery, TResult> decorated)
        {
            this.decorated = decorated;
            this.fifthweekRetryOnTransientErrorHandler = fifthweekRetryOnTransientErrorHandler;
            this.fifthweekRetryOnTransientErrorHandler.TaskName = typeof(TQuery).Name;
        }

        public RetryOnTransientErrorQueryHandlerDecorator(IFifthweekRetryOnTransientErrorHandler fifthweekRetryOnTransientErrorHandler, IQueryHandler<TQuery, TResult> decorated, int maxRetryCount, TimeSpan maxDelay)
        {
            this.decorated = decorated;
            this.fifthweekRetryOnTransientErrorHandler = fifthweekRetryOnTransientErrorHandler;
            this.fifthweekRetryOnTransientErrorHandler.TaskName = typeof(TQuery).Name;
            this.fifthweekRetryOnTransientErrorHandler.MaxRetryCount = maxRetryCount;
            this.fifthweekRetryOnTransientErrorHandler.MaxDelay = maxDelay;
        }

        internal IQueryHandler<TQuery, TResult> Decorated
        {
            get
            {
                return this.decorated;
            }
        }

        public Task<TResult> HandleAsync(TQuery query)
        {
            return this.fifthweekRetryOnTransientErrorHandler.HandleAsync(() => this.decorated.HandleAsync(query));
        }
    }
}