namespace Fifthweek.Api.Tests
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    [Decorator(OmitDefaultDecorators = true)]
    public class TestCommandHandlerWithNoDecorator : ICommandHandler<TestCommandWithNoDecorator>
    {
        public Task HandleAsync(TestCommandWithNoDecorator command)
        {
            return Task.FromResult(0);
        }
    }
}