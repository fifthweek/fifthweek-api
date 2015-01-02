namespace Fifthweek.Api
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    [Decorator(typeof(RetryOnSqlDeadlockOrTimeoutQueryHandlerDecorator<,>))]
    public class NullQueryHandler : IQueryHandler<NullQuery, bool>
    {
        public Task<bool> HandleAsync(NullQuery query)
        {
            return Task.FromResult<bool>(false);
        }
    }
}