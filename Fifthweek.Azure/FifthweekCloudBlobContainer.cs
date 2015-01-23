namespace Fifthweek.Azure
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.Storage.Blob;

    public class FifthweekCloudBlobContainer : ICloudBlobContainer
    {
        private readonly CloudBlobContainer container;

        public FifthweekCloudBlobContainer(CloudBlobContainer container)
        {
            this.container = container;
        }

        public Uri Uri
        {
            get
            {
                return this.container.Uri;
            }
        }

        public Task<bool> CreateIfNotExistsAsync()
        {
            return this.container.CreateIfNotExistsAsync();
        }

        public ICloudBlockBlob GetBlockBlobReference(string blobName)
        {
            return new FifthweekCloudBlockBlob(this.container.GetBlockBlobReference(blobName));
        }

        public string GetSharedAccessSignature(SharedAccessBlobPolicy policy)
        {
            return this.container.GetSharedAccessSignature(policy);
        }
    }
}