namespace Fifthweek.Api.Azure
{
    using Microsoft.WindowsAzure.Storage.Blob;

    public class FifthweekCloudBlobClient : ICloudBlobClient
    {
        private readonly CloudBlobClient cloudBlobClient;

        public FifthweekCloudBlobClient(CloudBlobClient cloudBlobClient)
        {
            this.cloudBlobClient = cloudBlobClient;
        }

        public ICloudBlobContainer GetContainerReference(string containerName)
        {
            return new FifthweekCloudBlobContainer(this.cloudBlobClient.GetContainerReference(containerName));
        }
    }
}