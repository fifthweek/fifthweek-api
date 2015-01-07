namespace Fifthweek.Api.Azure
{
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Storage;

    public class FifthweekCloudStorageAccount : ICloudStorageAccount
    {
        private readonly CloudStorageAccount storageAccount;

        public FifthweekCloudStorageAccount()
        {
            this.storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
        }

        public ICloudBlobClient CreateCloudBlobClient()
        {
            return new FifthweekCloudBlobClient(this.storageAccount.CreateCloudBlobClient());
        }
    }
}