namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Payments.Snapshots;

    public class TrimSnapshotsAtStartExecutor : ITrimSnapshotsAtStartExecutor
    {
        public IReadOnlyList<MergedSnapshot> Execute(
            DateTime startTimeInclusive,
            IReadOnlyList<MergedSnapshot> snapshots)
        {
            if (snapshots.Count == 0 || snapshots[0].Timestamp == startTimeInclusive)
            {
                return snapshots;
            }

            if (snapshots[0].Timestamp > startTimeInclusive)
            {
                throw new InvalidOperationException("Initial merged snapshot is unexpectedly ahead of start time.");
            }

            // If any snapshot is located at the start time, ignore everything before.
            for (int i = 0; i < snapshots.Count; i++)
            {
                if (snapshots[i].Timestamp == startTimeInclusive)
                {
                    return snapshots.Skip(i).ToList();
                }
            }

            // We have timestamps either side of the start time, so find the first one which is
            // earlier, bump it to the start time, and ignore any before.
            for (int i = snapshots.Count - 1; i >= 0; i--)
            {
                var snapshot = snapshots[i];
                if (snapshot.Timestamp < startTimeInclusive)
                {
                    var result = snapshots.Skip(i).ToList();
                    result[0] = new MergedSnapshot(
                        startTimeInclusive,
                        snapshot.CreatorChannels,
                        snapshot.CreatorFreeAccessUsers,
                        snapshot.SubscriberChannels,
                        snapshot.Subscriber,
                        snapshot.CalculatedAccountBalance);

                    return result;
                }
            }

            throw new InvalidOperationException("Unexpectedly did not find required timestamps for trimming.");
        }
    }
}