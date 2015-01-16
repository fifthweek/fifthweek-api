namespace Fifthweek.WebJobs.Files.Shared
{
    using System.Threading.Tasks;

    using Fifthweek.Azure;

    public interface IFileTask
    {
        string QueueName { get; }

        Task HandleAsync(ICloudQueue cloudQueue, ProcessFileMessage message);
    }
}