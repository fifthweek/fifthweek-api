namespace Fifthweek.Api.Core
{
    using System;
    using System.Threading.Tasks;

    public class RetryOnTransientErrorCommandHandlerDecorator<TCommand> : RetryOnTransientErrorDecoratorBase,
                                                                     ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> decorated;

        public RetryOnTransientErrorCommandHandlerDecorator(ICommandHandler<TCommand> decorated)
            : base(RetryOnTransientErrorDecoratorBase.MaxRetryCount, RetryOnTransientErrorDecoratorBase.MaxDelay)
        {
            this.decorated = decorated;
        }

        public RetryOnTransientErrorCommandHandlerDecorator(ICommandHandler<TCommand> decorated, int maxRetryCount, TimeSpan maxDelay)
            : base(maxRetryCount, maxDelay)
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