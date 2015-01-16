namespace Fifthweek.WebJobs.Thumbnails
{
    using System.IO;

    using Fifthweek.WebJobs.Thumbnails.Shared;

    public interface IImageService
    {
        void Resize(Stream input, Stream output, int width, int height, ResizeBehaviour resizeBehaviour);
    }
}