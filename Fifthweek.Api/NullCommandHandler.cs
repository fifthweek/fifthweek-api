namespace Fifthweek.Api
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    [Decorator(
        typeof(RetryOnSqlDeadlockOrTimeoutCommandHandlerDecorator<>),
        typeof(TransactionCommandHandlerDecorator<>))]
    public class NullCommandHandler : ICommandHandler<NullCommand>
    {
        public Task HandleAsync(NullCommand command)
        {
            return Task.FromResult(0);
        }
    }
}