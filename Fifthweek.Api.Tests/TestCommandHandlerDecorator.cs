namespace Fifthweek.Api.Tests
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    public class TestCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> decorated;

        public TestCommandHandlerDecorator(ICommandHandler<TCommand> decorated)
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
            return this.decorated.HandleAsync(command);
        }
    }
}