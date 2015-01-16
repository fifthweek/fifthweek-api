namespace Fifthweek.Webjobs.Thumbnails
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.Webjobs.Thumbnails.Shared;

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