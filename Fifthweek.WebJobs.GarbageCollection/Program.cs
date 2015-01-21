namespace Fifthweek.WebJobs.GarbageCollection
{
    using System;

    using Fifthweek.Azure;

    using Microsoft.Azure.WebJobs;

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
