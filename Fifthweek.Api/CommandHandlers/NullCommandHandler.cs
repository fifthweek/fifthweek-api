namespace Fifthweek.Api.CommandHandlers
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Commands;

    public class NullCommandHandler : ICommandHandler<NullCommand>
    {
        public Task HandleAsync(NullCommand command)
        {
            return Task.FromResult(0);
        }
    }
}