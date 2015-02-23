namespace Fifthweek.Api.FileManagement.FileTasks
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.WebJobs.Thumbnails.Shared;

    [AutoEqualityMembers]
    public partial class CreateThumbnailsTask : IFileTask
    {
        public CreateThumbnailsTask(params Thumbnail[] items)
        {
            this.Items = items == null ? new List<Thumbnail>() : items.ToList();
        }

        public IReadOnlyList<Thumbnail> Items { get; private set; }

        public async Task HandleAsync(IQueueService queueService, string containerName, string blobName, string filePurpose)
        {
            var outputMessage = new CreateThumbnailsMessage(
                containerName,
                blobName,
                this.Items.Select(v => v.ToMessage(blobName)).ToList(),
                false);

            await queueService.AddMessageToQueueAsync(Constants.ThumbnailsQueueName, outputMessage);
        }
    }
}