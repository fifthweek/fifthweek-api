namespace Fifthweek.Api.Core
{
    using System;
    using System.Threading.Tasks;

    public class RetryOnTransientErrorQueryHandlerDecorator<TQuery, TResult> : RetryOnTransientErrorDecoratorBase, IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> decorated;

        public RetryOnTransientErrorQueryHandlerDecorator(IQueryHandler<TQuery, TResult> decorated)
            : base(RetryOnTransientErrorDecoratorBase.MaxRetryCount, RetryOnTransientErrorDecoratorBase.MaxDelay)
        {
            this.decorated = decorated;
        }

        public RetryOnTransientErrorQueryHandlerDecorator(IQueryHandler<TQuery, TResult> decorated, int maxRetryCount, TimeSpan maxDelay)
            : base(maxRetryCount, maxDelay)
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

        protected override Type GetCommandOrQueryType()
        {
            return typeof(TQuery);
        }
    }
}