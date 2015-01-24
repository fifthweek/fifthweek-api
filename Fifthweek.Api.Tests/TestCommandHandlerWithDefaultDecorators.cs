namespace Fifthweek.Api.Tests
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    public class TestCommandHandlerWithDefaultDecorators : ICommandHandler<TestCommandWithDefaultDecorators>
    {
        public Task HandleAsync(TestCommandWithDefaultDecorators command)
        {
            return Task.FromResult(0);
        }
    }
}