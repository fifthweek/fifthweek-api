namespace Fifthweek.Api.Core
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

    public class RetryOnTransientErrorCommandHandlerDecorator<TCommand> : RetryOnTransientErrorDecoratorBase,
                                                                     ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> decorated;

        public RetryOnTransientErrorCommandHandlerDecorator(IExceptionHandler exceptionHandler, ITransientErrorDetectionStrategy transientErrorDetectionStrategy, ICommandHandler<TCommand> decorated)
            : this(exceptionHandler, transientErrorDetectionStrategy, decorated, RetryOnTransientErrorDecoratorBase.MaxRetryCount, RetryOnTransientErrorDecoratorBase.MaxDelay)
        {
        }

        public RetryOnTransientErrorCommandHandlerDecorator(IExceptionHandler exceptionHandler, ITransientErrorDetectionStrategy transientErrorDetectionStrategy, ICommandHandler<TCommand> decorated, int maxRetryCount, TimeSpan maxDelay)
            : base(exceptionHandler,transientErrorDetectionStrategy, typeof(TCommand).Name, maxRetryCount, maxDelay)
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

        private async Task<bool> CallDecorated(TCommand command)
        {
            await this.decorated.HandleAsync(command);
            return true;
        }
    }
}