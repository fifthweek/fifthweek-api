namespace Fifthweek.Azure
{
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.Storage.Queue;

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
    }
}