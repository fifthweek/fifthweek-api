namespace Fifthweek.Api.Tests
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    public class TestQueryHandlerWithDefaultDecorators : IQueryHandler<TestQueryWithDefaultDecorators, bool>
    {
        public Task<bool> HandleAsync(TestQueryWithDefaultDecorators query)
        {
            return Task.FromResult<bool>(false);
        }
    }
}