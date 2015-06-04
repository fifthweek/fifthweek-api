namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;

    public interface IMergeSnapshotsExecutor
    {
        IReadOnlyList<MergedSnapshot> Execute(
            Guid subscriberId,
            Guid creatorId,
            DateTime startTimeInclusive,
            IReadOnlyList<ISnapshot> snapshots);
    }
}