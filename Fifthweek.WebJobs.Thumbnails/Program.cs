namespace Fifthweek.WebJobs.Thumbnails
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.WebJobs.Shared;
    using Fifthweek.WebJobs.Thumbnails.Shared;

    using ImageMagick;

    using Microsoft.Azure.WebJobs;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public class Program
    {
        private static readonly IThumbnailProcessor ThumbnailProcessor = new ThumbnailProcessor(new ImageService());

        public static Task CreateThumbnailSetAsync(
            [QueueTrigger(Constants.ThumbnailsQueueName)] CreateThumbnailSetMessage message,
            [Blob("{ContainerName}/{InputBlobName}")] CloudBlockBlob input,
            CloudStorageAccount cloudStorageAccount,
            TextWriter logger,
            CancellationToken cancellationToken)
        {
            return ThumbnailProcessor.CreateThumbnailSetAsync(
                message,
                new FifthweekCloudBlockBlob(input),
                new FifthweekCloudStorageAccount(cloudStorageAccount),
                new WebJobLogger(logger),
                cancellationToken);
        }

        public static Task ProcessPoisonMessage(
            [QueueTrigger(Constants.ThumbnailsQueueName + "-poison")] CreateThumbnailSetMessage message,
            CloudStorageAccount cloudStorageAccount,
            TextWriter logger,
            CancellationToken cancellationToken)
        {
            logger.WriteLine("Failed to resize image from path {0}/{1}", message.ContainerName, message.InputBlobName);

            return ThumbnailProcessor.CreatePoisonThumbnailSetAsync(
                message,
                new FifthweekCloudStorageAccount(cloudStorageAccount),
                new WebJobLogger(logger), 
                cancellationToken);
        }

        public static void Main(string[] args)
        {
            // This is just a sanity check to ensure we run as 64 bit when processing images.
            Console.WriteLine("Thumbnails WebJob running as {0} bit process.", Environment.Is64BitProcess ? "64" : "32");
            Trace.TraceInformation("Thumbnails WebJob running as {0} bit process.", Environment.Is64BitProcess ? "64" : "32");

            var config = new JobHostConfiguration();
            config.DashboardConnectionString = config.StorageConnectionString = AzureConfiguration.GetStorageConnectionString();
            config.Queues.BatchSize = 8;
            config.Queues.MaxDequeueCount = 3;
            config.Queues.MaxPollingInterval = TimeSpan.FromSeconds(2);
            var host = new JobHost(config);
            host.RunAndBlock();
        }
    }
}
