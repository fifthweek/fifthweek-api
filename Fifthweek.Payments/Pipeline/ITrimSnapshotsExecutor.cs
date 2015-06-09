namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Payments.Snapshots;

    public interface ITrimSnapshotsExecutor
    {
        IReadOnlyList<MergedSnapshot> Execute(
            DateTime startTimeInclusive,
            IReadOnlyList<MergedSnapshot> snapshots);
    }
}