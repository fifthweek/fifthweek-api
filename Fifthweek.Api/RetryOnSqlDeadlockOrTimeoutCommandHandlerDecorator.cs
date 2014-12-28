namespace Fifthweek.Api
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.CommandHandlers;

    public class RetryOnSqlDeadlockOrTimeoutCommandHandlerDecorator<TCommand> : RetryOnSqlDeadlockOrTimeoutDecoratorBase,
                                                                     ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> decorated;

        public RetryOnSqlDeadlockOrTimeoutCommandHandlerDecorator(ICommandHandler<TCommand> decorated)
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

        public Task HandleAsync(TCommand command)
        {
            return this.HandleAsync(() => this.CallDecorated(command));
        }

        protected override Type GetCommandOrQueryType()
        {
            return typeof(TCommand);
        }

        private async Task<bool> CallDecorated(TCommand command)
        {
            await this.decorated.HandleAsync(command);
            return true;
        }
    }
}