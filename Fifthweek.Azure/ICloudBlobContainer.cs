namespace Fifthweek.Azure
{
    using System.Threading.Tasks;

    public interface ICloudBlobContainer
    {
        Task<bool> CreateIfNotExistsAsync();

        ICloudBlockBlob GetBlockBlobReference(string blobName);
    }
}