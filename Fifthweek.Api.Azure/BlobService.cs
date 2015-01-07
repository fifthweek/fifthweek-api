namespace Fifthweek.Api.Azure
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.Storage.Blob;

    public class BlobService
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

        public async Task<string> GetBlobSasUriForWritingAsync(string containerName, string blobName)
        {
            var client = this.cloudStorageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference(blobName);
            
            var policy = new SharedAccessBlobPolicy
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1),
                Permissions = SharedAccessBlobPermissions.Write
            };

            var token = blob.GetSharedAccessSignature(policy);

            return blob.Uri + token;
        }
    }
}