namespace Fifthweek.Api.Azure
{
    using Microsoft.WindowsAzure.Storage.Queue;

    public class FifthweekCloudQueueClient : ICloudQueueClient
    {
        private readonly CloudQueueClient cloudQueueClient;

        public FifthweekCloudQueueClient(CloudQueueClient cloudQueueClient)
        {
            this.cloudQueueClient = cloudQueueClient;
        }

        public ICloudQueue GetQueueReference(string queueName)
        {
            return new FifthweekCloudQueue(this.cloudQueueClient.GetQueueReference(queueName));
        }
    }
}