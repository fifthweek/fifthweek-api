namespace Fifthweek.Api.Azure
{
    using System.Threading.Tasks;

    public interface IBlobService
    {
        Task CreateBlobContainerAsync(string containerName);

        Task<string> GetBlobSasUriForWritingAsync(string containerName, string blobName);

        Task<string> GetBlobSasUriForReadingAsync(string containerName, string blobName);

        Task<long> GetBlobLengthAndSetContentTypeAsync(string containerName, string blobName, string contentType);
    }
}