namespace Fifthweek.Azure
{
    public interface ICloudStorageAccount
    {
        ICloudBlobClient CreateCloudBlobClient();

        ICloudQueueClient CreateCloudQueueClient();
    }
}