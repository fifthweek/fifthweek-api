namespace Fifthweek.Api.FileManagement
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class FileProcessor : IFileProcessor
    {
        private readonly IFilePurposeTasks filePurposeTasks;

        private readonly IQueueService queueService;

        public async Task ProcessFileAsync(FileId fileId, string containerName, string blobName, string filePurpose)
        {
            var tasks = this.filePurposeTasks.GetTasks(filePurpose);
            foreach (var task in tasks)
            {
                await task.HandleAsync(this.queueService, fileId, containerName, blobName, filePurpose);
            }
        }
    }
}