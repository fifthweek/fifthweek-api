namespace Fifthweek.Api.Core
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

    public class RetryOnTransientErrorQueryHandlerDecorator<TQuery, TResult> : RetryOnTransientErrorDecoratorBase, IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> decorated;

        public RetryOnTransientErrorQueryHandlerDecorator(IExceptionHandler exceptionHandler, ITransientErrorDetectionStrategy transientErrorDetectionStrategy, IQueryHandler<TQuery, TResult> decorated)
            : this(exceptionHandler, transientErrorDetectionStrategy, decorated, RetryOnTransientErrorDecoratorBase.MaxRetryCount, RetryOnTransientErrorDecoratorBase.MaxDelay)
        {
        }

        public RetryOnTransientErrorQueryHandlerDecorator(IExceptionHandler exceptionHandler, ITransientErrorDetectionStrategy transientErrorDetectionStrategy, IQueryHandler<TQuery, TResult> decorated, int maxRetryCount, TimeSpan maxDelay)
            : base(exceptionHandler, transientErrorDetectionStrategy, typeof(TQuery).Name, maxRetryCount, maxDelay)
        {
            this.decorated = decorated;
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
            return this.HandleAsync(() => this.decorated.HandleAsync(query));
        }
    }
}