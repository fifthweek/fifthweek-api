namespace Fifthweek.Api.Azure
{
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.Storage.Blob;
    using Microsoft.WindowsAzure.Storage.Shared.Protocol;

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

        public Task<ServiceProperties> GetServicePropertiesAsync()
        {
            return this.cloudBlobClient.GetServicePropertiesAsync();
        }

        public Task SetServicePropertiesAsync(ServiceProperties serviceProperties)
        {
            return this.cloudBlobClient.SetServicePropertiesAsync(serviceProperties);
        }
    }
}