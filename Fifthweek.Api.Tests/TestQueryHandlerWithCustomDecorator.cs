namespace Fifthweek.Api.Tests
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    [Decorator(typeof(TestQueryHandlerDecorator<,>))]
    public class TestQueryHandlerWithCustomDecorator : IQueryHandler<TestQueryWithCustomDecorator, bool>
    {
        public Task<bool> HandleAsync(TestQueryWithCustomDecorator query)
        {
            return Task.FromResult<bool>(false);
        }
    }
}