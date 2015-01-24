namespace Fifthweek.Api.Tests
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    public class TestQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> decorated;

        public TestQueryHandlerDecorator(IQueryHandler<TQuery, TResult> decorated)
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
            return this.decorated.HandleAsync(query);
        }
    }
}