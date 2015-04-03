namespace Fifthweek.Api.FileManagement
{
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement.Shared;

    public interface IFileProcessor
    {
        Task ProcessFileAsync(
            FileId fileId,
            string containerName,
            string blobName,
            string filePurpose);
    }
}