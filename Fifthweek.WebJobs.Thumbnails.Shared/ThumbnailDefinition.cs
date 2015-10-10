namespace Fifthweek.WebJobs.Thumbnails.Shared
{
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class ThumbnailDefinition
    {
        public ThumbnailDefinition()
        {
        }

        public string OutputBlobName { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public ResizeBehaviour ResizeBehaviour { get; set; }

        public ProcessingBehaviour ProcessingBehaviour { get; set; }

        [Optional]
        public List<ThumbnailDefinition> Children { get; set; }
    }
}