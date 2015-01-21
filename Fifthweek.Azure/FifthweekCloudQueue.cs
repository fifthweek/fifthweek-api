namespace Fifthweek.Azure
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.Storage.Queue;
    using Microsoft.WindowsAzure.Storage.RetryPolicies;

    public class FifthweekCloudQueue : ICloudQueue
    {
        private readonly CloudQueue cloudQueue;

        public FifthweekCloudQueue(CloudQueue cloudQueue)
        {
            this.cloudQueue = cloudQueue;
        }

        public string Name
        {
            get
            {
                return this.cloudQueue.Name;
            }
        }

        public Task CreateIfNotExistsAsync()
        {
            return this.cloudQueue.CreateIfNotExistsAsync();
        }

        public Task AddMessageAsync(CloudQueueMessage message)
        {
            return this.cloudQueue.AddMessageAsync(message);
        }

        public Task AddMessageAsync(CloudQueueMessage message, TimeSpan? timeToLive, TimeSpan? initialVisibilityDelay)
        {
            return this.cloudQueue.AddMessageAsync(message, timeToLive, initialVisibilityDelay, null, null);
        }
    }
}