namespace Dexter.Api.CommandHandlers
{
    using System.Threading.Tasks;

    public interface ICommandHandler<in TCommand>
    {
        Task HandleAsync(TCommand command);
    }
}