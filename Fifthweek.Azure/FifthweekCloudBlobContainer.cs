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

        public string Name
        {
            get
            {
                return this.container.Name;
            }
        }

        public BlobContainerProperties Properties
        {
            get
            {
                return this.container.Properties;
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

        public ICloudBlobDirectory GetDirectoryReference(string relativeAddress)
        {
            return new FifthweekCloudBlobDirectory(this.container.GetDirectoryReference(relativeAddress));
        }

        public Task DeleteAsync()
        {
            return this.container.DeleteAsync();
        }
    }
}