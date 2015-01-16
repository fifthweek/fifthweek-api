namespace Fifthweek.Api.Azure
{
    using System.Threading.Tasks;

    public interface IQueueService
    {
        Task PostFileUploadCompletedMessageToQueueAsync(string containerName, string blobName, string purpose);
    }
}