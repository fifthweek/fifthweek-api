namespace Fifthweek.WebJobs.Thumbnails
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.Logging;
    using Fifthweek.WebJobs.Shared;
    using Fifthweek.WebJobs.Thumbnails.Shared;

    using Microsoft.Azure.WebJobs;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public class Program
    {
        private const string ErrorIdentifier = "Thumbnails WebJob";

        private static readonly IThumbnailProcessor ThumbnailProcessor = new ThumbnailProcessor(new ImageService());

        public static async Task CreateThumbnailSetAsync(
            [QueueTrigger(Constants.ThumbnailsQueueName)] CreateThumbnailsMessage message,
            [Blob("{ContainerName}/{InputBlobName}")] CloudBlockBlob input,
            CloudStorageAccount cloudStorageAccount,
            TextWriter webJobsLogger,
            CancellationToken cancellationToken)
        {
            var logger = CreateLogger(webJobsLogger);
            try
            {
                await ThumbnailProcessor.CreateThumbnailSetAsync(
                        message,
                        new FifthweekCloudBlockBlob(input),
                        new FifthweekCloudStorageAccount(cloudStorageAccount),
                        logger,
                        cancellationToken);
            }
            catch (Exception t)
            {
                logger.Error(t);
                throw;
            }
        }

        public static async Task ProcessPoisonMessage(
            [QueueTrigger(Constants.ThumbnailsQueueName + "-poison")] CreateThumbnailsMessage message,
            CloudStorageAccount cloudStorageAccount,
            TextWriter webJobsLogger,
            CancellationToken cancellationToken)
        {
            var logger = CreateLogger(webJobsLogger);
            try
            {
                logger.Warn(
                    "Failed to resize image from blob {0}/{1}",
                    message.ContainerName,
                    message.InputBlobName);

                await ThumbnailProcessor.CreatePoisonThumbnailSetAsync(
                    message,
                    new FifthweekCloudStorageAccount(cloudStorageAccount),
                    logger,
                    cancellationToken);
            }
            catch (Exception t)
            {
                logger.Error(t);
                throw;
            }
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

        private static ILogger CreateLogger(TextWriter webJobsLogger)
        {
            return new WebJobLogger(webJobsLogger, ErrorIdentifier);
        }
    }
}
