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
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;

    using Microsoft.Azure.WebJobs;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public class Program
    {
        private const string ErrorIdentifier = "Snapshots WebJob";

        private static readonly ISnapshotProcessor SnapshotProcessor = new SnapshotProcessor(
            new CreateSubscriberSnapshotDbStatement(new SnapshotTimestampCreator(), new FifthweekDbConnectionFactory()),
            new CreateSubscriberChannelsSnapshotDbStatement(new GuidCreator(), new SnapshotTimestampCreator(), new FifthweekDbConnectionFactory()),
            new CreateCreatorChannelsSnapshotDbStatement(new GuidCreator(), new SnapshotTimestampCreator(), new FifthweekDbConnectionFactory()),
            new CreateCreatorFreeAccessUsersSnapshotDbStatement(new GuidCreator(), new SnapshotTimestampCreator(), new FifthweekDbConnectionFactory()));

        public static Task CreateSnapshotAsync(
            [QueueTrigger(Payments.Shared.Constants.RequestSnapshotQueueName)] CreateSnapshotMessage message,
            TextWriter webJobsLogger,
            int dequeueCount,
            CancellationToken cancellationToken)
        {
            return SnapshotProcessor.CreateSnapshotAsync(
                message,
                CreateLogger(webJobsLogger),
                cancellationToken);
        }

        public static Task ProcessPoisonMessage(
            [QueueTrigger(Payments.Shared.Constants.RequestSnapshotQueueName + "-poison")] string message,
            TextWriter webJobsLogger,
            int dequeueCount,
            CancellationToken cancellationToken)
        {
            return SnapshotProcessor.HandlePoisonMessageAsync(
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
