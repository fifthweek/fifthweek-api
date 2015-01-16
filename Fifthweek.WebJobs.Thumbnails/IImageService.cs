namespace Fifthweek.WebJobs.Thumbnails
{
    using System.IO;

    using Fifthweek.WebJobs.Thumbnails.Shared;

    using ImageMagick;

    public interface IImageService
    {
        void Resize(MagickImage input, Stream output, int width, int height, ResizeBehaviour resizeBehaviour);
    }
}