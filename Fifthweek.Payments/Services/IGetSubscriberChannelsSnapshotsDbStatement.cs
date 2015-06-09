namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetSubscriberChannelsSnapshotsDbStatement
    {
        Task<IReadOnlyList<Snapshots.SubscriberChannelsSnapshot>> ExecuteAsync(
            UserId subscriberId,
            DateTime startTimestampInclusive,
            DateTime endTimestampExclusive);
    }
}