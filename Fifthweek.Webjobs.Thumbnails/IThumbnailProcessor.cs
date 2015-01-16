namespace Fifthweek.WebJobs.Thumbnails
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.WebJobs.Thumbnails.Shared;

    public interface IThumbnailProcessor
    {
        Task CreateThumbnailAsync(
            CreateThumbnailMessage thumbnail,
            Stream input,
            Stream output,
            TextWriter logger,
            CancellationToken cancellationToken);
    }
}