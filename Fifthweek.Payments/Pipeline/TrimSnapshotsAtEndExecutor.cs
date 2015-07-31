namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Payments.Snapshots;

    public class TrimSnapshotsAtEndExecutor : ITrimSnapshotsAtEndExecutor
    {
        public IReadOnlyList<ISnapshot> Execute(
            DateTime endTimeExclusive,
            IReadOnlyList<ISnapshot> snapshots)
        {
            if (snapshots.Count == 0 || snapshots.Last().Timestamp < endTimeExclusive)
            {
                return snapshots;
            }

            return snapshots.Where(v => v.Timestamp < endTimeExclusive).ToList();
        }
    }
}