namespace Fifthweek.WebJobs.Thumbnails
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.WebJobs.Thumbnails.Shared;

    using Microsoft.Azure.WebJobs;

    public class Program
    {
        private static readonly IThumbnailProcessor ThumbnailProcessor = new ThumbnailProcessor(new ImageService());

        public static Task CreateThumbnailAsync(
            [QueueTrigger(Constants.ThumbnailsQueueName)] CreateThumbnailMessage thumbnail,
            [Blob("{ContainerName}/{InputBlobName}", FileAccess.Read)] Stream input,
            [Blob("{ContainerName}/{OutputBlobName}", FileAccess.Write)] Stream output,
            TextWriter logger,
            CancellationToken cancellationToken)
        {
            return ThumbnailProcessor.CreateThumbnailAsync(
                thumbnail, 
                input, 
                output, 
                logger, 
                cancellationToken);
        }

        public static void Main(string[] args)
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
