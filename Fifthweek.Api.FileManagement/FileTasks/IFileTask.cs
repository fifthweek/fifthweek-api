namespace Fifthweek.Api.FileManagement.FileTasks
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;

    public interface IFileTask
    {
        Task HandleAsync(IQueueService queueService, string containerName, string blobName, string filePurpose);
    }
}