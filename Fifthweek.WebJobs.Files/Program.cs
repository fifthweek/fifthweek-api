namespace Fifthweek.WebJobs.Files
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.WebJobs.Files.Shared;

    using Microsoft.Azure.WebJobs;

    internal class Program
    {
        private static readonly IFileProcessor FileProcessor = new FileProcessor(
            new FilePurposeTasks(), new CloudQueueResolver());

        public static Task ProcessFileAsync(
            [QueueTrigger(Constants.FilesQueueName)] ProcessFileMessage message,
            IBinder binder,
            TextWriter logger,
            CancellationToken cancellationToken)
        {
            return FileProcessor.ProcessFileAsync(message, binder, logger, cancellationToken);
        }

        public static void ProcessPoisonMessage(
            [QueueTrigger(Constants.FilesQueueName + "-poison")] ProcessFileMessage message, 
            TextWriter logger)
        {
            logger.WriteLine("Failed to process file with purpose {0} and path {1}/{2}", message.Purpose, message.ContainerName, message.BlobName);
        }

        private static void Main(string[] args)
        {
            var config = new JobHostConfiguration();
            config.Queues.BatchSize = 8;
            config.Queues.MaxDequeueCount = 3;
            config.Queues.MaxPollingInterval = TimeSpan.FromSeconds(5);
            var host = new JobHost(config);
            host.RunAndBlock();
        }
    }
}
