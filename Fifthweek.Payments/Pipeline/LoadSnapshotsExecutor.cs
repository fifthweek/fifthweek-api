namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;

    public class LoadSnapshotsExecutor : ILoadSnapshotsExecutor
    {
        public IReadOnlyList<ISnapshot> Execute(
            Guid subscriberId,
            Guid creatorId,
            DateTime startTimeInclusive,
            DateTime endTimeExclusive)
        {
            // Load all snapshots in range, plus one week before range so we can determine subscription
            // end day, plus one snapshot of each type before range if necessary in case there has been no
            // activity.
            var queryStartTimeInclusive = startTimeInclusive.AddDays(-7);
            var queryEndTimeExclusive = endTimeExclusive;
            return new List<ISnapshot>();
        }
    }
}