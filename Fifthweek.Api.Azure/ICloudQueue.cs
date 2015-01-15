namespace Fifthweek.Api.Azure
{
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.Storage.Queue;

    public interface ICloudQueue
    {
        Task CreateIfNotExistsAsync();

        Task AddMessageAsync(CloudQueueMessage message);
    }
}