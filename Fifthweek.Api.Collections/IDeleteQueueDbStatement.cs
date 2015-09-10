namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;

    public interface IDeleteQueueDbStatement
    {
        Task ExecuteAsync(QueueId queueId);
    }
}