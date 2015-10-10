namespace Fifthweek.Api.Azure
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Azure;
    using Fifthweek.Shared;

    using Microsoft.WindowsAzure.Storage.Blob;

    public class BlobService : IBlobService
    {
        private readonly ICloudStorageAccount cloudStorageAccount;

        private readonly IAzureConfiguration azureConfiguration;

        public BlobService(ICloudStorageAccount cloudStorageAccount, IAzureConfiguration azureConfiguration)
        {
            this.cloudStorageAccount = cloudStorageAccount;
            this.azureConfiguration = azureConfiguration;
        }

        public async Task CreateBlobContainerAsync(string containerName)
        {
            containerName.AssertNotNull("containerName");

            var client = this.cloudStorageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();
        }

        public Task<BlobInformation> GetBlobInformationAsync(string containerName, string blobName)
        {
            containerName.AssertNotNull("containerName");
            blobName.AssertNotNull("blobName");

            var client = this.cloudStorageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName);
            var blob = container.GetBlockBlobReference(blobName);

            return Task.FromResult(new BlobInformation(containerName, blobName, blob.Uri.ToString()));
        }

        public Task<BlobSharedAccessInformation> GetBlobSharedAccessInformationForWritingAsync(string containerName, string blobName, DateTime expiry)
        {
            containerName.AssertNotNull("containerName");
            blobName.AssertNotNull("blobName");

            if (expiry.Kind != DateTimeKind.Utc)
            {
                throw new InvalidOperationException("Expiry time must be in UTC");
            }

            var client = this.cloudStorageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName);
            var blob = container.GetBlockBlobReference(blobName);

            var policy = new SharedAccessBlobPolicy
            {
                SharedAccessExpiryTime = expiry,
                Permissions = SharedAccessBlobPermissions.Write
            };

            var token = blob.GetSharedAccessSignature(policy);

            return Task.FromResult(new BlobSharedAccessInformation(containerName, blobName, blob.Uri.ToString(), token, expiry));
        }

        public Task<BlobSharedAccessInformation> GetBlobSharedAccessInformationForReadingAsync(string containerName, string blobName, DateTime expiry)
        {
            containerName.AssertNotNull("containerName");
            blobName.AssertNotNull("blobName");

            if (expiry.Kind != DateTimeKind.Utc)
            {
                throw new InvalidOperationException("Expiry time must be in UTC");
            }

            var client = this.cloudStorageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName);
            var blob = container.GetBlockBlobReference(blobName);

            var policy = new SharedAccessBlobPolicy
            {
                SharedAccessExpiryTime = expiry,
                Permissions = SharedAccessBlobPermissions.Read
            };

            var token = blob.GetSharedAccessSignature(policy);

            var blobUri = blob.Uri;
            var cdnDomain = this.azureConfiguration.CdnDomain;
            if (string.IsNullOrWhiteSpace(cdnDomain))
            {
                throw new InvalidOperationException("CDN domain has not been configured.");
            }

            var cdnUriBuilder = new UriBuilder(blobUri) { Host = cdnDomain };
            var cdnUri = cdnUriBuilder.Uri.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);

            return Task.FromResult(new BlobSharedAccessInformation(containerName, blobName, cdnUri, token, expiry));
        }

        public Task<BlobContainerSharedAccessInformation> GetBlobContainerSharedAccessInformationForReadingAsync(string containerName, DateTime expiry)
        {
            containerName.AssertNotNull("containerName");

            if (expiry.Kind != DateTimeKind.Utc)
            {
                throw new InvalidOperationException("Expiry time must be in UTC");
            }
            
            var client = this.cloudStorageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName);

            var policy = new SharedAccessBlobPolicy
            {
                SharedAccessExpiryTime = expiry,
                Permissions = SharedAccessBlobPermissions.Read
            };

            var token = container.GetSharedAccessSignature(policy);

            var blobUri = container.Uri;
            var cdnDomain = this.azureConfiguration.CdnDomain;
            if (string.IsNullOrWhiteSpace(cdnDomain))
            {
                throw new InvalidOperationException("CDN domain has not been configured.");
            }

            var cdnUriBuilder = new UriBuilder(blobUri) { Host = cdnDomain };
            var cdnUri = cdnUriBuilder.Uri.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);

            return Task.FromResult(new BlobContainerSharedAccessInformation(containerName, cdnUri, token, expiry));
        }

        public async Task<long> GetBlobLengthAndSetPropertiesAsync(string containerName, string blobName, string contentType, TimeSpan cacheControlMaxAge)
        {
            containerName.AssertNotNull("containerName");
            blobName.AssertNotNull("blobName");
            contentType.AssertNotNull("contentType");
            
            var client = this.cloudStorageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName);
            var blob = container.GetBlockBlobReference(blobName);
            await blob.FetchAttributesAsync();
            blob.Properties.ContentType = contentType;
            blob.Properties.CacheControl = "public, max-age=" + (int)cacheControlMaxAge.TotalSeconds;
            await blob.SetPropertiesAsync();
            return blob.Properties.Length;
        }
    }
}