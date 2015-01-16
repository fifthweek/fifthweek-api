namespace Fifthweek.WebJobs.Thumbnails.Shared
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class CreateThumbnailMessage
    {
        public CreateThumbnailMessage()
        {
        }

        public string ContainerName { get; set; }

        public string InputBlobName { get; set; }
        
        public string OutputBlobName { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public ResizeBehaviour ResizeBehaviour { get; set; }

        public bool Overwrite { get; set; }
    }
}