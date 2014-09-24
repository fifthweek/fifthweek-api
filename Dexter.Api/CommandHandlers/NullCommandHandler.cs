namespace Dexter.Api.CommandHandlers
{
    using System.Threading.Tasks;

    using Dexter.Api.Commands;

    public class NullCommandHandler : ICommandHandler<NullCommand>
    {
        public Task HandleAsync(NullCommand command)
        {
            return Task.FromResult(0);
        }
    }
}