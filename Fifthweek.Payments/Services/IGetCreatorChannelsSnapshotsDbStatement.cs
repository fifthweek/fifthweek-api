namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetCreatorChannelsSnapshotsDbStatement
    {
        Task<IReadOnlyList<Snapshots.CreatorChannelsSnapshot>> ExecuteAsync(
            UserId creatorId,
            DateTime startTimestampInclusive,
            DateTime endTimestampExclusive);
    }
}