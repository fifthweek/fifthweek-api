namespace Fifthweek.Webjobs.Files
{
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.Webjobs.Files.Shared;

    using Microsoft.Azure.WebJobs;
    using Microsoft.WindowsAzure.Storage.Queue;

    public class CloudQueueResolver : ICloudQueueResolver
    {
        public CloudQueueResolver()
        {
        }

        public async Task<ICloudQueue> GetQueueAsync(IBinder binder, IFileTask task)
        {
            var queueAttribute = new QueueAttribute(task.QueueName);
            var queue = binder.Bind<CloudQueue>(queueAttribute);
            await queue.CreateIfNotExistsAsync();
            return new FifthweekCloudQueue(queue);
        }
    }
}