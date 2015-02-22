namespace Fifthweek.WebJobs.Files
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.WebJobs.Files.Shared;
    using Fifthweek.WebJobs.Shared;

    using Microsoft.Azure.WebJobs;

    [AutoConstructor]
    public partial class FileProcessor : IFileProcessor
    {
        private readonly IFilePurposeTasks filePurposeTasks;

        private readonly ICloudQueueResolver cloudQueueResolver;

        public async Task ProcessFileAsync(
            ProcessFileMessage message,
            IBinder binder,
            ILogger logger,
            CancellationToken cancellationToken)
        {
            var sw = new Stopwatch();
            sw.Start();
            logger.Info("StartFileProcessor: " + sw.ElapsedMilliseconds);
            var tasks = this.filePurposeTasks.GetTasks(message.Purpose);
            logger.Info("GetTasks: " + sw.ElapsedMilliseconds);

            ICloudQueue queue = null;
            foreach (var task in tasks)
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    logger.Info("CheckCancellation: " + sw.ElapsedMilliseconds);

                    var queueName = task.QueueName;
                    if (queue == null || queue.Name != queueName)
                    {
                        queue = await this.cloudQueueResolver.GetQueueAsync(binder, queueName);
                    }
                    logger.Info("GetQueue: " + sw.ElapsedMilliseconds);

                    await task.HandleAsync(queue, message);
                    logger.Info("HandleTask: " + sw.ElapsedMilliseconds);
                }
                catch (Exception t)
                {
                    logger.Error("Failed to handle file task of type '{0}': {1}", task.GetType().Name, t);
                    throw;
                }
            }
        }
    }
}