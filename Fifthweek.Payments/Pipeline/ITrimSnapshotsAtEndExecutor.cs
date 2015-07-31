namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Payments.Snapshots;

    public interface ITrimSnapshotsAtEndExecutor
    {
        IReadOnlyList<ISnapshot> Execute(
            DateTime endTimeExclusive,
            IReadOnlyList<ISnapshot> snapshots);
    }
}