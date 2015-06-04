namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;

    public interface ITrimSnapshotsExecutor
    {
        IReadOnlyList<MergedSnapshot> Execute(
            DateTime startTimeInclusive,
            IReadOnlyList<MergedSnapshot> snapshots);
    }
}