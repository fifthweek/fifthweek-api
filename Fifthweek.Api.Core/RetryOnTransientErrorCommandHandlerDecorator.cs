namespace Fifthweek.Api.Core
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Shared;

    public class RetryOnTransientErrorCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly IFifthweekRetryOnTransientErrorHandler fifthweekRetryOnTransientErrorHandler;

        private readonly ICommandHandler<TCommand> decorated;

        public RetryOnTransientErrorCommandHandlerDecorator(IFifthweekRetryOnTransientErrorHandler fifthweekRetryOnTransientErrorHandler, ICommandHandler<TCommand> decorated)
        {
            this.decorated = decorated;
            this.fifthweekRetryOnTransientErrorHandler = fifthweekRetryOnTransientErrorHandler;
            this.fifthweekRetryOnTransientErrorHandler.TaskName = typeof(TCommand).Name;
        }

        public RetryOnTransientErrorCommandHandlerDecorator(IFifthweekRetryOnTransientErrorHandler fifthweekRetryOnTransientErrorHandler, ICommandHandler<TCommand> decorated, int maxRetryCount, TimeSpan maxDelay)
        {
            this.decorated = decorated;
            this.fifthweekRetryOnTransientErrorHandler = fifthweekRetryOnTransientErrorHandler;
            this.fifthweekRetryOnTransientErrorHandler.TaskName = typeof(TCommand).Name;
            this.fifthweekRetryOnTransientErrorHandler.MaxRetryCount = maxRetryCount;
            this.fifthweekRetryOnTransientErrorHandler.MaxDelay = maxDelay;
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
            return this.fifthweekRetryOnTransientErrorHandler.HandleAsync(() => this.decorated.HandleAsync(command));
        }
    }
}