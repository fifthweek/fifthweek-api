namespace Fifthweek.Api.FileManagement
{
    using System.Threading.Tasks;

    public interface IFileProcessor
    {
        Task ProcessFileAsync(
            string containerName,
            string blobName,
            string filePurpose);
    }
}