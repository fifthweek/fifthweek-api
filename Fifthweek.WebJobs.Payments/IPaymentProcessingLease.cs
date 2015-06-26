namespace Fifthweek.WebJobs.Payments
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Payments.Shared;

    public interface IPaymentProcessingLease : IKeepAliveHandler
    {
        bool GetIsAcquired();

        Task<bool> TryAcquireLeaseAsync();

        Task AcquireLeaseAsync();

        Task RenewLeaseAsync();

        Task ReleaseLeaseAsync();

        Task UpdateTimestampsAsync();
    }
}