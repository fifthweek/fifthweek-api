namespace Fifthweek.Api.Core
{
    using System;
    using System.Threading.Tasks;

    public class RetryOnRecoverableDatabaseErrorCommandHandlerDecorator<TCommand> : RetryOnRecoverableDatabaseErrorDecoratorBase,
                                                                     ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> decorated;

        public RetryOnRecoverableDatabaseErrorCommandHandlerDecorator(ICommandHandler<TCommand> decorated)
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