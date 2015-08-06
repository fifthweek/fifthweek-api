namespace Fifthweek.Azure
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Shared;

    public interface IBlobLease : IKeepAliveHandler
    {
        bool GetIsAcquired();

        Task<bool> TryAcquireLeaseAsync();

        Task AcquireLeaseAsync();

        Task RenewLeaseAsync();

        Task ReleaseLeaseAsync();

        Task UpdateTimestampsAsync();

        Task<TimeSpan> GetTimeSinceLastLeaseAsync();
    }
}