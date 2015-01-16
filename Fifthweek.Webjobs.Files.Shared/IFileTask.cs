namespace Fifthweek.Webjobs.Files.Shared
{
    using System.Threading.Tasks;

    using Fifthweek.Azure;

    using Microsoft.WindowsAzure.Storage.Queue;

    public interface IFileTask
    {
        string QueueName { get; }

        Task HandleAsync(ICloudQueue cloudQueue, ProcessFileMessage message);
    }
}