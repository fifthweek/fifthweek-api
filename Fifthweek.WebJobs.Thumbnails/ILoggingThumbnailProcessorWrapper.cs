namespace Fifthweek.WebJobs.Thumbnails
{
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.WebJobs.Shared;
    using Fifthweek.WebJobs.Thumbnails.Shared;

    public interface ILoggingThumbnailProcessorWrapper
    {
        Task CreateThumbnailSetAsync(
            CreateThumbnailsMessage message,
            ICloudBlockBlob input,
            ICloudStorageAccount cloudStorageAccount,
            ILogger logger,
            int dequeueCount,
            CancellationToken cancellationToken);

        Task CreatePoisonThumbnailSetAsync(
            CreateThumbnailsMessage message,
            ICloudStorageAccount cloudStorageAccount,
            ILogger logger,
            int dequeueCount,
            CancellationToken cancellationToken);
    }
}