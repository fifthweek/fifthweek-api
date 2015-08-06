namespace Fifthweek.WebJobs.GarbageCollection
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Azure;
    using Fifthweek.GarbageCollection;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.GarbageCollection.Shared;
    using Fifthweek.WebJobs.Shared;

    using Microsoft.Azure.WebJobs;

    using Constants = Fifthweek.GarbageCollection.Shared.Constants;

    public class Program
    {
        private const string ErrorIdentifier = "Garbage Collection WebJob";

        private static readonly GarbageCollectionProcessor GarbageCollectionProcessor =
            new GarbageCollectionProcessor(
                new RunGarbageCollection(
                    new TimestampCreator(),
                    new DeleteTestUserAccountsDbStatement(new FifthweekDbConnectionFactory()),
                    new GetFilesEligibleForGarbageCollectionDbStatement(new FifthweekDbConnectionFactory()),
                    new DeleteBlobsForFile(new BlobLocationGenerator(), new FifthweekCloudStorageAccount()),
                    new DeleteFileDbStatement(new FifthweekDbConnectionFactory()),
                    new DeleteOrphanedBlobContainers(
                        new GetAllChannelIdsDbStatement(new FifthweekDbConnectionFactory()),
                        new FifthweekCloudStorageAccount())),
                new BlobLeaseFactory(
                    new TimestampCreator(),
                    new FifthweekCloudStorageAccount()));

        public static Task RunGarbageCollectionAsync(
            [QueueTrigger(Constants.GarbageCollectionQueueName)] RunGarbageCollectionMessage message,
            TextWriter webJobsLogger,
            int dequeueCount,
            CancellationToken cancellationToken)
        {
            return GarbageCollectionProcessor.RunGarbageCollectionAsync(
                message,
                CreateLogger(webJobsLogger),
                cancellationToken);
        }

        public static Task ProcessGarbageCollectionPoisonMessage(
            [QueueTrigger(Constants.GarbageCollectionQueueName + "-poison")] string message,
            TextWriter webJobsLogger,
            int dequeueCount,
            CancellationToken cancellationToken)
        {
            return GarbageCollectionProcessor.HandlePoisonMessageAsync(
                message,
                CreateLogger(webJobsLogger),
                cancellationToken);
        }

        public static void Main(string[] args)
        {
            DataDirectory.ConfigureDataDirectory();

            var config = new JobHostConfiguration();
            config.DashboardConnectionString = config.StorageConnectionString = AzureConfiguration.GetStorageConnectionString();
            config.Queues.BatchSize = 8;
            config.Queues.MaxDequeueCount = 3;
            config.Queues.MaxPollingInterval = TimeSpan.FromSeconds(60);
            var host = new JobHost(config);
            host.RunAndBlock();
        }

        private static ILogger CreateLogger(TextWriter webJobsLogger)
        {
            return new WebJobLogger(webJobsLogger, ErrorIdentifier);
        }
    }
}
