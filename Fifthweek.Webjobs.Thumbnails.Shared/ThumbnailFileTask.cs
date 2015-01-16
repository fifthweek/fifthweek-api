namespace Fifthweek.Webjobs.Thumbnails.Shared
{
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Webjobs.Files.Shared;

    using Microsoft.WindowsAzure.Storage.Queue;

    using Newtonsoft.Json;

    [AutoEqualityMembers]
    public partial class ThumbnailFileTask : IFileTask
    {
        public ThumbnailFileTask(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

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
                string.Format("{0}-{1}-{2}", message.BlobName, this.Width, this.Height),
                this.Width,
                this.Height,
                false);

            var serializedContent = JsonConvert.SerializeObject(messageContent);
            return cloudQueue.AddMessageAsync(new CloudQueueMessage(serializedContent));
        }
    }
}