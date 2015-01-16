namespace Fifthweek.Api.Azure
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Azure;

    using Microsoft.WindowsAzure.Storage.Blob;

    public class BlobService : IBlobService
    {
        private readonly ICloudStorageAccount cloudStorageAccount;

        public BlobService(ICloudStorageAccount cloudStorageAccount)
        {
            this.cloudStorageAccount = cloudStorageAccount;
        }

        public async Task CreateBlobContainerAsync(string containerName)
        {
            var client = this.cloudStorageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();
        }

        public Task<string> GetBlobSasUriForWritingAsync(string containerName, string blobName)
        {
            var client = this.cloudStorageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName);
            var blob = container.GetBlockBlobReference(blobName);

            var policy = new SharedAccessBlobPolicy
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1),
                Permissions = SharedAccessBlobPermissions.Write
            };

            var token = blob.GetSharedAccessSignature(policy);

            return Task.FromResult(blob.Uri + token);
        }

        public Task<string> GetBlobSasUriForReadingAsync(string containerName, string blobName)
        {
            var client = this.cloudStorageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName);
            var blob = container.GetBlockBlobReference(blobName);

            var policy = new SharedAccessBlobPolicy
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1),
                Permissions = SharedAccessBlobPermissions.Read
            };

            var token = blob.GetSharedAccessSignature(policy);

            return Task.FromResult(blob.Uri + token);
        }

        public async Task<long> GetBlobLengthAndSetContentTypeAsync(string containerName, string blobName, string contentType)
        {
            var client = this.cloudStorageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName);
            var blob = container.GetBlockBlobReference(blobName);
            await blob.FetchAttributesAsync();
            blob.Properties.ContentType = contentType;
            await blob.SetPropertiesAsync();
            return blob.Properties.Length;
        }
    }
}