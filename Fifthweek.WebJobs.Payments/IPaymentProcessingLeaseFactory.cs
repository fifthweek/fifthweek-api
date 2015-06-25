namespace Fifthweek.WebJobs.Payments
{
    using System.Threading;

    using Fifthweek.WebJobs.Shared;

    public interface IPaymentProcessingLeaseFactory
    {
        IPaymentProcessingLease Create(CancellationToken cancellationToken);
    }
}