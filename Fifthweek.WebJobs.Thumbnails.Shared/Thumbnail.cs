namespace Fifthweek.WebJobs.Thumbnails.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.CodeGeneration;
    using Fifthweek.WebJobs.Files.Shared;

    public enum ResizeBehaviour
    {
        CropToAspectRatio,
        MaintainAspectRatio,
    }

    [AutoEqualityMembers]
    public partial class Thumbnail
    {
        public Thumbnail(int width, int height, ResizeBehaviour resizeBehaviour, params Thumbnail[] children)
        {
            this.Width = width;
            this.Height = height;
            this.ResizeBehaviour = resizeBehaviour;
            this.Children = children == null ? new List<Thumbnail>() : children.ToList();
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public ResizeBehaviour ResizeBehaviour { get; private set; }

        public IReadOnlyList<Thumbnail> Children { get; private set; }

        [NonEquatable]
        public string QueueName
        {
            get
            {
                return Constants.ThumbnailsQueueName;
            }
        }

        public ThumbnailSetItemMessage ToMessage(ProcessFileMessage message)
        {
            var outputMessage = new ThumbnailSetItemMessage(
                this.GetOutputBlobName(message.BlobName),
                this.Width,
                this.Height,
                this.ResizeBehaviour,
                this.Children.Select(v => v.ToMessage(message)).ToList());

            return outputMessage;
        }

        private string GetOutputBlobName(string inputBlobName)
        {
            string resizeBehaviourTag;

            switch (this.ResizeBehaviour)
            {
                case ResizeBehaviour.MaintainAspectRatio:
                    resizeBehaviourTag = string.Empty;
                    break;

                case ResizeBehaviour.CropToAspectRatio:
                    resizeBehaviourTag = "-crop";
                    break;

                default:
                    throw new Exception(
                        "Unable to generate output blob name for resize behaviour: " + this.ResizeBehaviour);
            }

            // Note we use a forwardslash here to take advantage of Azure Blob virtual hierarchy structure
            // which allows us to easily delete all blobs generated from the input blob.
            return string.Format("{0}/{1}x{2}{3}", inputBlobName, this.Width, this.Height, resizeBehaviourTag);
        }
    }
}