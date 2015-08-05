namespace Fifthweek.Azure
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICloudBlobDirectory
    {
        ICloudBlockBlob GetBlockBlobReference(string blobName);

        Task<IReadOnlyList<ICloudBlockBlob>> ListCloudBlockBlobsAsync(bool useFlatBlobListing = false);
    }
}