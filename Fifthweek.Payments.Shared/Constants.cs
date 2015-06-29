using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifthweek.Payments.Shared
{
    public class Constants
    {
        public const string RequestSnapshotQueueName = "snapshot-requests";
        public const string RequestProcessPaymentsQueueName = "process-payments-requests";
        public const string ProcessPaymentsLeaseObjectName = "process-payments-lease-object";
        public const string LastProcessPaymentsStartTimestampMetadataKey = "lastProcessPaymentsStartTimestamp";
        public const string LastProcessPaymentsEndTimestampMetadataKey = "lastProcessPaymentsEndTimestamp";
        public const string LastProcessPaymentsRenewCountMetadataKey = "lastProcessPaymentsRenewCount";

        public const string PaymentProcessingDataContainerName = "payment-processing-data";

        public static readonly TimeSpan PaymentProcessingDefaultMessageDelay = TimeSpan.FromMinutes(30);
    }
}
