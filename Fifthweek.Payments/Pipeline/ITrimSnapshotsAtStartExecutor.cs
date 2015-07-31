namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Payments.Snapshots;

    public interface ITrimSnapshotsAtStartExecutor
    {
        IReadOnlyList<MergedSnapshot> Execute(
            DateTime startTimeInclusive,
            IReadOnlyList<MergedSnapshot> snapshots);
    }
}