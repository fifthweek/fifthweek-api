namespace Fifthweek.WebJobs.Files
{
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.WebJobs.Files.Shared;

    using Microsoft.Azure.WebJobs;
    using Microsoft.WindowsAzure.Storage.Queue;

    public class CloudQueueResolver : ICloudQueueResolver
    {
        public CloudQueueResolver()
        {
        }

        public async Task<ICloudQueue> GetQueueAsync(IBinder binder, string queueName)
        {
            var queueAttribute = new QueueAttribute(queueName);
            var queue = binder.Bind<CloudQueue>(queueAttribute);
            await queue.CreateIfNotExistsAsync();
            return new FifthweekCloudQueue(queue);
        }
    }
}