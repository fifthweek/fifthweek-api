namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetCreatorFreeAccessUsersSnapshotsDbStatement
    {
        Task<IReadOnlyList<Snapshots.CreatorFreeAccessUsersSnapshot>> ExecuteAsync(
            UserId creatorId,
            DateTime startTimestampInclusive,
            DateTime endTimestampExclusive);
    }
}