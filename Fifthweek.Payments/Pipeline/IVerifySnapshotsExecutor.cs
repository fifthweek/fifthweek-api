namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;

    public interface IVerifySnapshotsExecutor
    {
        void Execute(
            DateTime startTimeInclusive,
            DateTime endTimeExclusive, 
            Guid subscriberId,
            Guid creatorId, 
            IReadOnlyList<ISnapshot> snapshots);
    }
}