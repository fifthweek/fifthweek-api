namespace Fifthweek.Api
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Queries;
    using Fifthweek.Api.QueryHandlers;

    public class RetryOnSqlDeadlockOrTimeoutQueryHandlerDecorator<TQuery, TResult> : RetryOnSqlDeadlockOrTimeoutDecoratorBase, IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> decorated;

        public RetryOnSqlDeadlockOrTimeoutQueryHandlerDecorator(IQueryHandler<TQuery, TResult> decorated)
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