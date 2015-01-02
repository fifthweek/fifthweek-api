namespace Fifthweek.Api.Core
{
    using System.Threading.Tasks;

    public interface ICommandHandler<in TCommand>
    {
        Task HandleAsync(TCommand command);
    }
}