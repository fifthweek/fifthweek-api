using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifthweek.WebJobs.Snapshots
{
    using System.Diagnostics;
    using System.IO;
    using System.Threading;

    using Fifthweek.Api.Persistence;
    using Fifthweek.Azure;
    using Fifthweek.Payments;
    using Fifthweek.Payments.Services;
    using Fifthweek.WebJobs.Shared;

    using Microsoft.Azure.WebJobs;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public class Program
    {
        private const string ErrorIdentifier = "Snapshots WebJob";

        private static readonly ISnapshotProcessor SnapshotProcessor = new SnapshotProcessor(
            new FifthweekDbConnectionFactory());

        public static Task CreateThumbnailSetAsync(
            [QueueTrigger(Constants.RequestSnapshotQueueName)] CreateSnapshotMessage message,
            CloudStorageAccount cloudStorageAccount,
            TextWriter webJobsLogger,
            int dequeueCount,
            CancellationToken cancellationToken)
        {
            return SnapshotProcessor.CreateThumbnailSetAsync(
                message,
                new FifthweekCloudStorageAccount(cloudStorageAccount),
                CreateLogger(webJobsLogger),
                cancellationToken);
        }

        public static Task ProcessPoisonMessage(
            [QueueTrigger(Constants.RequestSnapshotQueueName + "-poison")] CreateSnapshotMessage message,
            CloudStorageAccount cloudStorageAccount,
            TextWriter webJobsLogger,
            int dequeueCount,
            CancellationToken cancellationToken)
        {
            return SnapshotProcessor.CreatePoisonThumbnailSetAsync(
                message,
                new FifthweekCloudStorageAccount(cloudStorageAccount),
                CreateLogger(webJobsLogger),
                cancellationToken);
        }


        public static void Main(string[] args)
        {
            DataDirectory.ConfigureDataDirectory();

            var config = new JobHostConfiguration();
            config.DashboardConnectionString = config.StorageConnectionString = AzureConfiguration.GetStorageConnectionString();
            config.Queues.BatchSize = 8;
            config.Queues.MaxDequeueCount = 10;
            config.Queues.MaxPollingInterval = TimeSpan.FromSeconds(10);
            var host = new JobHost(config);
            host.RunAndBlock();
        }

        private static ILogger CreateLogger(TextWriter webJobsLogger)
        {
            return new WebJobLogger(webJobsLogger, ErrorIdentifier);
        }
    }
}
