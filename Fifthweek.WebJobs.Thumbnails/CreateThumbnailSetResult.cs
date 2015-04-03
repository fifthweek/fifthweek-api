namespace Fifthweek.WebJobs.Thumbnails
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class CreateThumbnailSetResult
    {
        public int RenderWidth { get; private set; }

        public int RenderHeight { get; private set; }
    }
}