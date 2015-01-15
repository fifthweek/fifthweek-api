namespace Fifthweek.Webjobs.Files
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Shared;
    using Fifthweek.Webjobs.Files.Shared;
    using Fifthweek.Webjobs.Thumbnails.Shared;

    using Microsoft.Azure.WebJobs;

    class Program
    {
        static void Main(string[] args)
        {
            var config = new JobHostConfiguration();
            config.Queues.BatchSize = 8;
            config.Queues.MaxDequeueCount = 3;
            config.Queues.MaxPollingInterval = TimeSpan.FromSeconds(5);
            var host = new JobHost(config);
            host.RunAndBlock();
        }

        public static Task ProcessFileAsync(
            [QueueTrigger(Files.Shared.Constants.FilesQueueName)] ProcessFileQueueItem processFile,
            [Queue(Thumbnails.Shared.Constants.ThumbnailsQueueName)] IAsyncCollector<ThumbnailQueueItem> thumbnails,
            TextWriter logger,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
