namespace Fifthweek.Api.Tests
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    [Decorator(typeof(TestCommandHandlerDecorator<>))]
    public class TestCommandHandlerWithCustomDecorator : ICommandHandler<TestCommandWithCustomDecorator>
    {
        public Task HandleAsync(TestCommandWithCustomDecorator command)
        {
            return Task.FromResult(0);
        }
    }
}