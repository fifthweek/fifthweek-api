namespace Fifthweek.Api.Azure
{
    public interface ICloudStorageAccount
    {
        ICloudBlobClient CreateCloudBlobClient();
    }
}