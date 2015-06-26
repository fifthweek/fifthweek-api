namespace Fifthweek.WebJobs.Payments
{
    using System.Threading;

    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;

    [AutoConstructor]
    public partial class PaymentProcessingLeaseFactory : IPaymentProcessingLeaseFactory
    {
        private readonly ITimestampCreator timestampCreator;
        private readonly ICloudStorageAccount cloudStorageAccount;

        public IPaymentProcessingLease Create(CancellationToken cancellationToken)
        {
            return new PaymentProcessingLease(this.timestampCreator, this.cloudStorageAccount, cancellationToken);
        }
    }
}