namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Snapshots;

    public interface IVerifySnapshotsExecutor
    {
        void Execute(
            DateTime startTimeInclusive,
            DateTime endTimeExclusive,
            UserId subscriberId,
            UserId creatorId, 
            IReadOnlyList<ISnapshot> snapshots);
    }
}