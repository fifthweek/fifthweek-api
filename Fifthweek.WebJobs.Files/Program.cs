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
            new FilePurposeToTasksMappings(), new CloudQueueResolver());

        public static Task ProcessFileAsync(
            [QueueTrigger(Files.Shared.Constants.FilesQueueName)] ProcessFileMessage processFile,
            IBinder binder,
            TextWriter logger,
            CancellationToken cancellationToken)
        {
            return FileProcessor.ProcessFileAsync(processFile, binder, logger, cancellationToken);
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
