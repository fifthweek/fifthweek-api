namespace Fifthweek.Azure
{
    using System;

    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Storage;

    public class FifthweekCloudStorageAccount : ICloudStorageAccount
    {
        private readonly CloudStorageAccount storageAccount;

        public FifthweekCloudStorageAccount()
        {
            this.storageAccount = CloudStorageAccount.Parse(AzureConfiguration.GetStorageConnectionString());
        }

        public FifthweekCloudStorageAccount(CloudStorageAccount storageAccount)
        {
            this.storageAccount = storageAccount;
        }

        public ICloudBlobClient CreateCloudBlobClient()
        {
            return new FifthweekCloudBlobClient(this.storageAccount.CreateCloudBlobClient());
        }

        public ICloudQueueClient CreateCloudQueueClient()
        {
            return new FifthweekCloudQueueClient(this.storageAccount.CreateCloudQueueClient());
        }
    }
}