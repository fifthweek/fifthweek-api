namespace Fifthweek.Api.Azure
{
    using System;
    using System.Threading.Tasks;

    public interface IBlobService
    {
        Task CreateBlobContainerAsync(string containerName);

        Task<BlobSharedAccessInformation> GetBlobSharedAccessInformationForWritingAsync(string containerName, string blobName, DateTime expiry);

        Task<BlobContainerSharedAccessInformation> GetBlobContainerSharedAccessInformationForReadingAsync(string containerName, DateTime expiry);

        Task<long> GetBlobLengthAndSetPropertiesAsync(string containerName, string blobName, string contentType, TimeSpan cacheControlMaxAge);

        Task<BlobInformation> GetBlobInformationAsync(string containerName, string blobName);

        Task<BlobSharedAccessInformation> GetBlobSharedAccessInformationForReadingAsync(string containerName, string blobName, DateTime expiry);
    }
}