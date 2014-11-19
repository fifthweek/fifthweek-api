namespace Fifthweek.Api.CommandHandlers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Transactions;

    public class TransactionCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> decorated;

        public TransactionCommandHandlerDecorator(ICommandHandler<TCommand> decorated)
        {
            this.decorated = decorated;
        }

        internal ICommandHandler<TCommand> Decorated
        {
            get
            {
                return this.decorated;
            }
        }

        [DebuggerStepThrough]
        public async Task HandleAsync(TCommand command)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await this.decorated.HandleAsync(command);
                scope.Complete();
            }
        }
    }
}