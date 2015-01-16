namespace Fifthweek.Azure
{
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.Storage.Queue;

    public interface ICloudQueue
    {
        string Name { get; }

        Task CreateIfNotExistsAsync();

        Task AddMessageAsync(CloudQueueMessage message);
    }
}