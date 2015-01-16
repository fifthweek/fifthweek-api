namespace Fifthweek.WebJobs.Thumbnails
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.WebJobs.Thumbnails.Shared;

    using Microsoft.WindowsAzure.Storage.Blob;

    public interface IThumbnailProcessor
    {
        Task CreateThumbnailAsync(
            CreateThumbnailMessage thumbnail,
            Stream input,
            CloudBlockBlob output,
            TextWriter logger,
            CancellationToken cancellationToken);

        Task CreatePoisonThumbnailAsync(
            CreateThumbnailMessage thumbnail,
            CloudBlockBlob output,
            TextWriter logger,
            CancellationToken cancellationToken);
    }
}