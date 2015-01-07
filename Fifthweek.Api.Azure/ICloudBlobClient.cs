namespace Fifthweek.Api.Azure
{
    public interface ICloudBlobClient
    {
        ICloudBlobContainer GetContainerReference(string containerName);
    }
}