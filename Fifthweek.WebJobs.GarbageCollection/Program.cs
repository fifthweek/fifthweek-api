namespace Fifthweek.WebJobs.GarbageCollection
{
    using System;

    using Fifthweek.Azure;

    using Microsoft.Azure.WebJobs;

    /// <summary>
    /// Delete from UncommittedSubscriptionPayment where amount=0.
    /// Delete from CalculatedAccountBalance where single item with amount=0.
    /// Delete all snapshots associated with testing.fifthweek.com accounts.
    /// Delete all CalculatedAccountBalance associated with testing.fifthweek.com accounts.
    /// Delete all file blobs associated with testing.fifthweek.com accounts.
    /// Delete all accounts associated with testing.fifthweek.com accounts.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new JobHostConfiguration();
            config.DashboardConnectionString = config.StorageConnectionString = AzureConfiguration.GetStorageConnectionString();
            config.Queues.BatchSize = 8;
            config.Queues.MaxDequeueCount = 3;
            config.Queues.MaxPollingInterval = TimeSpan.FromSeconds(60);
            var host = new JobHost(config);
            host.RunAndBlock();
        }
    }
}
