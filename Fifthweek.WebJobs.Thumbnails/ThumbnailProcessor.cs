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
        private readonly IImageService imageService;

        public Task CreateThumbnailAsync(
            CreateThumbnailMessage thumbnail,
            Stream input,
            Stream output,
            TextWriter logger,
            CancellationToken cancellationToken)
        {
            if (thumbnail.Overwrite || output.Length == 0)
            {
                this.imageService.Resize(input, output, thumbnail.Width, thumbnail.Height, thumbnail.ResizeBehaviour);
            }

            return Task.FromResult(0);
        }
    }
}