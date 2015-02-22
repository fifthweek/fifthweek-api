namespace Fifthweek.WebJobs.Thumbnails
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.WebJobs.Shared;
    using Fifthweek.WebJobs.Thumbnails.Shared;

    using Microsoft.WindowsAzure.Storage.Blob;

    public interface IThumbnailProcessor
    {
        Task CreateThumbnailSetAsync(
            CreateThumbnailSetMessage message,
            ICloudBlockBlob input,
            ICloudStorageAccount cloudStorageAccount,
            ILogger logger,
            CancellationToken cancellationToken);

        Task CreatePoisonThumbnailSetAsync(
            CreateThumbnailSetMessage message,
            ICloudStorageAccount cloudStorageAccount,
            ILogger logger,
            CancellationToken cancellationToken);
    }
}