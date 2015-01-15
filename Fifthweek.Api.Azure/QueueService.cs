namespace Fifthweek.Api.Azure
{
    public class QueueService : IQueueService
    {
        private readonly ICloudStorageAccount cloudStorageAccount;

        public QueueService(ICloudStorageAccount cloudStorageAccount)
        {
            this.cloudStorageAccount = cloudStorageAccount;
        }
    }
}