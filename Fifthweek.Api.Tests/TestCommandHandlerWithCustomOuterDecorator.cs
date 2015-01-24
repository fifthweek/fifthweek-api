namespace Fifthweek.Api.Tests
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    [Decorator(
        typeof(TestCommandHandlerDecorator<>),
        typeof(RetryOnTransientErrorCommandHandlerDecorator<>))]
    public class TestCommandHandlerWithCustomOuterDecorator : ICommandHandler<TestCommandWithCustomOuterDecorator>
    {
        public Task HandleAsync(TestCommandWithCustomOuterDecorator command)
        {
            return Task.FromResult(0);
        }
    }
}