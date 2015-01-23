namespace Fifthweek.Api.Azure
{
    using System.Threading.Tasks;

    public interface IBlobService
    {
        Task CreateBlobContainerAsync(string containerName);

        Task<BlobSharedAccessInformation> GetBlobSharedAccessInformationForWritingAsync(string containerName, string blobName);

        Task<BlobContainerSharedAccessInformation> GetBlobContainerSharedAccessInformationForReadingAsync(string containerName);

        Task<long> GetBlobLengthAndSetContentTypeAsync(string containerName, string blobName, string contentType);
    }
}