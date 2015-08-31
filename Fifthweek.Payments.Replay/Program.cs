using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifthweek.Payments.Replay
{
    using System.IO;
    using System.Reflection;

    using Fifthweek.Payments.Pipeline;
    using Fifthweek.Payments.Services;

    using Newtonsoft.Json;

    class Program
    {
        static void Main(string[] args)
        {
            PersistedPaymentProcessingData data;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
                typeof(Program).Namespace + ".payment-processing-data.json"))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    var json = streamReader.ReadToEnd();
                    data = JsonConvert.DeserializeObject<PersistedPaymentProcessingData>(json);
                }
            }

            var processPaymentProcessingData =
                new ProcessPaymentProcessingData(
                    new SubscriberPaymentPipeline(
                        new TrimSnapshotsAtEndExecutor(),
                        new VerifySnapshotsExecutor(),
                        new MergeSnapshotsExecutor(),
                        new RollBackSubscriptionsExecutor(),
                        new RollForwardSubscriptionsExecutor(),
                        new TrimSnapshotsAtStartExecutor(),
                        new AddSnapshotsForBillingEndDatesExecutor(),
                        new CalculateCostPeriodsExecutor(new CalculateSnapshotCostExecutor()),
                        new AggregateCostPeriodsExecutor()));

            var result = processPaymentProcessingData.ExecuteAsync(data.Input).Result;

            if (result.Equals(data.Output))
            {
                Console.WriteLine("Results match.");
            }
            else
            {
                Console.WriteLine("Results differ.");
            }
            
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }
}
