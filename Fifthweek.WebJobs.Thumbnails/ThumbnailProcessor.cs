namespace Fifthweek.WebJobs.Thumbnails
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.CodeGeneration;
    using Fifthweek.WebJobs.Thumbnails.Shared;

    using ImageMagick;

    [AutoConstructor]
    public partial class ThumbnailProcessor : IThumbnailProcessor
    {
        public Task CreateThumbnailAsync(
            CreateThumbnailMessage thumbnail,
            Stream input,
            Stream output,
            TextWriter logger,
            CancellationToken cancellationToken)
        {
            using (var image = new MagickImage(input))
            {
                image.Resize(800, 600);
                image.Write(output);
            }

            return Task.FromResult(0);
        }
    }
}