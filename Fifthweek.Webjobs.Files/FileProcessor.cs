namespace Fifthweek.Webjobs.Files
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Webjobs.Files.Shared;
    using Fifthweek.Webjobs.Thumbnails.Shared;

    using Microsoft.Azure.WebJobs;
    using Microsoft.WindowsAzure.Storage.Queue;

    [AutoConstructor]
    public partial class FileProcessor : IFileProcessor
    {
        private readonly IFilePurposeToTasksMappings filePurposeToTasksMappings;

        private readonly ICloudQueueResolver cloudQueueResolver;

        public async Task ProcessFileAsync(
            ProcessFileMessage message,
            IBinder binder,
            TextWriter logger,
            CancellationToken cancellationToken)
        {
            var tasks = this.filePurposeToTasksMappings.GetTasks(message.Purpose);

            ICloudQueue queue = null;
            foreach (var task in tasks)
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (queue == null || queue.Name != task.QueueName)
                    {
                        queue = await this.cloudQueueResolver.GetQueueAsync(binder, task);
                    }

                    await task.HandleAsync(queue, message);
                }
                catch (Exception t)
                {
                    logger.WriteLine("Failed to handle file task of type '{0}': {1}", task.GetType().Name, t);
                    throw;
                }
            }
        }
    }
}