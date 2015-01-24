namespace Fifthweek.Api.Tests
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    [Decorator(
        typeof(TestQueryHandlerDecorator<,>),
        typeof(RetryOnTransientErrorQueryHandlerDecorator<,>))]
    public class TestQueryHandlerWithCustomOuterDecorator : IQueryHandler<TestQueryWithCustomOuterDecorator, bool>
    {
        public Task<bool> HandleAsync(TestQueryWithCustomOuterDecorator query)
        {
            return Task.FromResult<bool>(false);
        }
    }
}