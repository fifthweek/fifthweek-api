namespace Fifthweek.Azure
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.Storage.Queue;

    public interface ICloudQueue
    {
        string Name { get; }

        Task CreateIfNotExistsAsync();

        Task AddMessageAsync(CloudQueueMessage message);

        Task AddMessageAsync(CloudQueueMessage message, TimeSpan? timeToLive, TimeSpan? initialVisibilityDelay);
    }
}