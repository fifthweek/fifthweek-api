namespace Fifthweek.Api.FileManagement
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class FileProcessor : IFileProcessor
    {
        private readonly IFilePurposeTasks filePurposeTasks;

        private readonly IQueueService queueService;

        public async Task ProcessFileAsync(string containerName, string blobName, string filePurpose)
        {
            var tasks = this.filePurposeTasks.GetTasks(filePurpose);
            foreach (var task in tasks)
            {
                await task.HandleAsync(this.queueService, containerName, blobName, filePurpose);
            }
        }
    }
}