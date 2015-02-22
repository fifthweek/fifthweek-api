namespace Fifthweek.WebJobs.Thumbnails.Shared
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.WebJobs.Files.Shared;

    using Microsoft.WindowsAzure.Storage.Queue;

    using Newtonsoft.Json;

    [AutoEqualityMembers]
    public partial class ThumbnailSetFileTask : IFileTask
    {
        public ThumbnailSetFileTask(params Thumbnail[] items)
        {
            this.Items = items == null ? new List<Thumbnail>() : items.ToList();
        }

        public IReadOnlyList<Thumbnail> Items { get; private set; }

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
            var outputMessage = new CreateThumbnailSetMessage(
                message.ContainerName,
                message.BlobName,
                this.Items.Select(v => v.ToMessage(message)).ToList(),
                message.Overwrite);

            var serializedContent = JsonConvert.SerializeObject(outputMessage);
            return cloudQueue.AddMessageAsync(new CloudQueueMessage(serializedContent));
        }
    }
}