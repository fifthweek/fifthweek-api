namespace Fifthweek.Api.Core
{
    using System.Threading.Tasks;

    public interface IEventHandler<in TEvent>
    {
        /// <remarks>
        /// Return type should be `void` in future for true asynchrony. Until we have a separate back-channel, exceptions and completion are 
        /// synchronized back through the task.
        /// </remarks>
        Task HandleAsync(TEvent @event);
    }
}