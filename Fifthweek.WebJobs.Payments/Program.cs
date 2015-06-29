using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifthweek.WebJobs.Payments
{
    using System.IO;
    using System.Threading;

    using Fifthweek.Api.Persistence;
    using Fifthweek.Azure;
    using Fifthweek.Payments.Pipeline;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;

    using Microsoft.Azure.WebJobs;

    using Constants = Fifthweek.Payments.Shared.Constants;

    public class Program
    {
        private const string ErrorIdentifier = "Payments WebJob";

        private static readonly IPaymentProcessor PaymentProcessor = new PaymentProcessor(
            new ProcessAllPayments(
                new TimestampCreator(),
                new GetAllSubscribersDbStatement(new FifthweekDbConnectionFactory()),
                new ProcessPaymentsForSubscriber(
                    new GetCreatorsAndFirstSubscribedDatesDbStatement(new FifthweekDbConnectionFactory()),
                    new ProcessPaymentsBetweenSubscriberAndCreator(
                        new GetPaymentProcessingData(
                            new GetCreatorChannelsSnapshotsDbStatement(new FifthweekDbConnectionFactory()),
                            new GetCreatorFreeAccessUsersSnapshotsDbStatement(new FifthweekDbConnectionFactory()),
                            new GetCreatorPostsDbStatement(new FifthweekDbConnectionFactory()),
                            new GetSubscriberChannelsSnapshotsDbStatement(new FifthweekDbConnectionFactory()),
                            new GetSubscriberSnapshotsDbStatement(new FifthweekDbConnectionFactory()),
                            new GetCalculatedAccountBalancesDbStatement(new FifthweekDbConnectionFactory()),
                            new GetCreatorPercentageOverrideDbStatement(new FifthweekDbConnectionFactory())),
                        new ProcessPaymentProcessingData(
                            new SubscriberPaymentPipeline(
                                new VerifySnapshotsExecutor(),
                                new MergeSnapshotsExecutor(),
                                new RollBackSubscriptionsExecutor(),
                                new RollForwardSubscriptionsExecutor(),
                                new TrimSnapshotsExecutor(),
                                new AddSnapshotsForBillingEndDatesExecutor(),
                                new CalculateCostPeriodsExecutor(
                                    new CalculateSnapshotCostExecutor()),
                                new AggregateCostPeriodsExecutor())),
                        new PersistPaymentProcessingResults(
                            new GuidCreator(),
                            new PersistPaymentProcessingDataStatement(new FifthweekCloudStorageAccount()),
                            new PersistCommittedAndUncommittedRecordsDbStatement(new FifthweekDbConnectionFactory()))),
                    new GetLatestCommittedLedgerDateDbStatement(new FifthweekDbConnectionFactory())),
                new UpdateAccountBalancesDbStatement(new FifthweekDbConnectionFactory())),
            new PaymentProcessingLeaseFactory(new TimestampCreator(), new FifthweekCloudStorageAccount()),
            new RequestProcessPaymentsService(new QueueService(new FifthweekCloudStorageAccount())));

        public static Task ProcessPaymentsAsync(
            [QueueTrigger(Constants.RequestProcessPaymentsQueueName)] ProcessPaymentsMessage message,
            TextWriter webJobsLogger,
            int dequeueCount,
            CancellationToken cancellationToken)
        {
            return PaymentProcessor.ProcessPaymentsAsync(
                message,
                CreateLogger(webJobsLogger),
                cancellationToken);
        }

        public static Task ProcessPoisonMessage(
            [QueueTrigger(Constants.RequestProcessPaymentsQueueName + "-poison")] string message,
            TextWriter webJobsLogger,
            int dequeueCount,
            CancellationToken cancellationToken)
        {
            return PaymentProcessor.HandlePoisonMessageAsync(
                message,
                CreateLogger(webJobsLogger),
                cancellationToken);
        }

        public static void Main(string[] args)
        {
            DataDirectory.ConfigureDataDirectory();

            var config = new JobHostConfiguration();
            config.DashboardConnectionString = config.StorageConnectionString = AzureConfiguration.GetStorageConnectionString();
            config.Queues.BatchSize = 1;
            config.Queues.MaxDequeueCount = 3;
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
