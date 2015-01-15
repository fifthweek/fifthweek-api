namespace Fifthweek.Api.Azure
{
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.Storage.Blob;

    public class FifthweekCloudBlobContainer : ICloudBlobContainer
    {
        private readonly CloudBlobContainer container;

        public FifthweekCloudBlobContainer(CloudBlobContainer container)
        {
            this.container = container;
        }

        public Task<bool> CreateIfNotExistsAsync()
        {
            return this.container.CreateIfNotExistsAsync();
        }

        public ICloudBlockBlob GetBlockBlobReference(string blobName)
        {
            return new FifthweekCloudBlockBlob(this.container.GetBlockBlobReference(blobName));
        }
    }
}