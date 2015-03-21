namespace Fifthweek.Api.FileManagement.FileTasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.CodeGeneration;
    using Fifthweek.WebJobs.Thumbnails.Shared;

    [AutoEqualityMembers]
    public partial class Thumbnail
    {
        public Thumbnail(int width, int height, string alias, ResizeBehaviour resizeBehaviour, params Thumbnail[] children)
        {
            this.Width = width;
            this.Height = height;
            this.Alias = alias;
            this.ResizeBehaviour = resizeBehaviour;
            this.Children = children == null ? new List<Thumbnail>() : children.ToList();
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public string Alias { get; private set; }

        public ResizeBehaviour ResizeBehaviour { get; private set; }

        public IReadOnlyList<Thumbnail> Children { get; private set; }

        public ThumbnailDefinition ToMessage(string blobName)
        {
            var outputMessage = new ThumbnailDefinition(
                this.GetOutputBlobName(blobName),
                this.Width,
                this.Height,
                this.ResizeBehaviour,
                this.Children.Select(v => v.ToMessage(blobName)).ToList());

            return outputMessage;
        }

        private string GetOutputBlobName(string inputBlobName)
        {
            // Note we use a forwardslash here to take advantage of Azure Blob virtual hierarchy structure
            // which allows us to easily delete all blobs generated from the input blob.
            return string.Format("{0}/{1}", inputBlobName, this.Alias);
        }
    }
}