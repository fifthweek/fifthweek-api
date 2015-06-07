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

    using Fifthweek.Azure;
    using Fifthweek.WebJobs.Shared;

    using Microsoft.Azure.WebJobs;

    class Program
    {
        private const string ErrorIdentifier = "Snapshots WebJob";

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
