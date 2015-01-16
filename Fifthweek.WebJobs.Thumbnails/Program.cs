namespace Fifthweek.WebJobs.Thumbnails
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.WebJobs.Thumbnails.Shared;

    using ImageMagick;

    using Microsoft.Azure.WebJobs;
    using Microsoft.WindowsAzure.Storage.Blob;

    public class Program
    {
        private static readonly IThumbnailProcessor ThumbnailProcessor = new ThumbnailProcessor(new ImageService());

        public static Task CreateThumbnailAsync(
            [QueueTrigger(Constants.ThumbnailsQueueName)] CreateThumbnailMessage message,
            [Blob("{ContainerName}/{InputBlobName}", FileAccess.Read)] Stream input,
            [Blob("{ContainerName}/{OutputBlobName}", FileAccess.Write)] CloudBlockBlob output,
            TextWriter logger,
            CancellationToken cancellationToken)
        {
            return ThumbnailProcessor.CreateThumbnailAsync(
                message, 
                input, 
                output, 
                logger, 
                cancellationToken);
        }

        public static Task ProcessPoisonMessage(
            [QueueTrigger(Constants.ThumbnailsQueueName + "-poison")] CreateThumbnailMessage message,
            [Blob("{ContainerName}/{OutputBlobName}", FileAccess.Write)] CloudBlockBlob output,
            TextWriter logger,
            CancellationToken cancellationToken)
        {
            logger.WriteLine("Failed to resize image to {0}x{1} from path {2}/{3}", message.Width, message.Height, message.ContainerName, message.InputBlobName);

            return ThumbnailProcessor.CreatePoisonThumbnailAsync(
                message,
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
