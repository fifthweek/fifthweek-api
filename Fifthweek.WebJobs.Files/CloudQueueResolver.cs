namespace Fifthweek.WebJobs.Files
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.WebJobs.Files.Shared;

    using Microsoft.Azure.WebJobs;
    using Microsoft.WindowsAzure.Storage.Queue;

    public class CloudQueueResolver : ICloudQueueResolver
    {
        public async Task<ICloudQueue> GetQueueAsync(IBinder binder, string queueName)
        {
            var queueAttribute = new QueueAttribute(queueName);
            var queue = await binder.BindAsync<CloudQueue>(queueAttribute);
            return new FifthweekCloudQueue(queue);
        }
    }
}