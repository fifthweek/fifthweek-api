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
        Task CreateThumbnailAsync(
            CreateThumbnailMessage thumbnail,
            Stream input,
            ICloudBlockBlob output,
            ILogger logger,
            CancellationToken cancellationToken);

        Task CreatePoisonThumbnailAsync(
            CreateThumbnailMessage thumbnail,
            ICloudBlockBlob output,
            ILogger logger,
            CancellationToken cancellationToken);
    }
}