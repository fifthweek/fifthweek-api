namespace Fifthweek.WebJobs.Thumbnails.Shared
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.WebJobs.Files.Shared;

    using Microsoft.WindowsAzure.Storage.Queue;

    using Newtonsoft.Json;

    public enum ResizeBehaviour
    {
        CropToAspectRatio,
        MaintainAspectRatio,
    }

    [AutoEqualityMembers]
    public partial class ThumbnailFileTask : IFileTask
    {
        public ThumbnailFileTask(int width, int height, ResizeBehaviour resizeBehaviour)
        {
            this.Width = width;
            this.Height = height;
            this.ResizeBehaviour = resizeBehaviour;
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public ResizeBehaviour ResizeBehaviour { get; private set; }

        [NonEquatable]
        public string QueueName
        {
            get
            {
                return Constants.ThumbnailsQueueName;
            }
        }

        public Task HandleAsync(ICloudQueue cloudQueue, ProcessFileMessage message)
        {
            var messageContent = new CreateThumbnailMessage(
                message.ContainerName,
                message.BlobName,
                this.GetOutputBlobName(message.BlobName),
                this.Width,
                this.Height,
                this.ResizeBehaviour,
                message.Overwrite);

            var serializedContent = JsonConvert.SerializeObject(messageContent);
            return cloudQueue.AddMessageAsync(new CloudQueueMessage(serializedContent));
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