namespace Fifthweek.Azure
{
    using System;

    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Storage;

    public class FifthweekCloudStorageAccount : ICloudStorageAccount
    {
        private static readonly CloudStorageAccount DefaultStorageAccount;

        private readonly CloudStorageAccount storageAccount;

        static FifthweekCloudStorageAccount()
        {
            var connectionString = AzureConfiguration.TryGetStorageConnectionString();
            if (connectionString != null)
            {
                DefaultStorageAccount = CloudStorageAccount.Parse(connectionString);
            }
        }

        public FifthweekCloudStorageAccount()
        {
            this.storageAccount = DefaultStorageAccount;
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