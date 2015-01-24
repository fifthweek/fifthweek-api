namespace Fifthweek.Api.Tests
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    [Decorator(OmitDefaultDecorators = true)]
    public class TestQueryHandlerWithNoDecorator : IQueryHandler<TestQueryWithNoDecorator, bool>
    {
        public Task<bool> HandleAsync(TestQueryWithNoDecorator query)
        {
            return Task.FromResult<bool>(false);
        }
    }
}