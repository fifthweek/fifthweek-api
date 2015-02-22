namespace Fifthweek.WebJobs.Thumbnails.Shared
{
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class ThumbnailSetItemMessage
    {
        public ThumbnailSetItemMessage()
        {
        }

        public string OutputBlobName { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public ResizeBehaviour ResizeBehaviour { get; set; }

        [Optional]
        public List<ThumbnailSetItemMessage> Children { get; set; }
    }
}