namespace Fifthweek.Webjobs.Thumbnails.Shared
{
    using Fifthweek.Webjobs.Files.Shared;

    public class ThumbnailFileTask : IFileTask
    {
        public ThumbnailFileTask(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public int Width { get; private set; }

        public int Height { get; private set; }
    }
}