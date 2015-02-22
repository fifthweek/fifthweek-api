namespace Fifthweek.WebJobs.Files.Shared
{
    using System.Threading.Tasks;

    using Fifthweek.Azure;

    public interface IFileTask
    {
        Task HandleAsync(string containerName, string blobName, string filePurpose);
    }
}