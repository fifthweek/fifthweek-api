namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MergeSnapshotsExecutor : IMergeSnapshotsExecutor
    {
        public IReadOnlyList<MergedSnapshot> Execute(
            Guid subscriberId,
            Guid creatorId,
            DateTime startTimeInclusive,
            IReadOnlyList<ISnapshot> snapshots)
        {
            if (snapshots.Count == 0)
            {
                return new List<MergedSnapshot>();
            }

            DateTime initialTimestamp = startTimeInclusive;
            var firstTimestamp = snapshots[0].Timestamp;
            if (firstTimestamp < startTimeInclusive)
            {
                initialTimestamp = firstTimestamp;
            }

            var creator = CreatorSnapshot.Default(initialTimestamp, creatorId);
            var creatorGuestList = CreatorGuestListSnapshot.Default(initialTimestamp, creatorId);
            var subscriber = SubscriberSnapshot.Default(initialTimestamp, subscriberId);

            var mergedSnapshots = new List<MergedSnapshot>();

            if (firstTimestamp > initialTimestamp)
            {
                mergedSnapshots.Add(new MergedSnapshot(creator, creatorGuestList, subscriber));
            }

            foreach (var snapshot in snapshots)
            {
                var assigned = this.TryAssign(snapshot, ref creator)
                                || this.TryAssign(snapshot, ref creatorGuestList)
                                || this.TryAssign(snapshot, ref subscriber);

                if (!assigned)
                {
                    throw new InvalidOperationException("Unknown snapshot type: " + snapshot.GetType().Name);
                }

                var newMergedSnapshot = new MergedSnapshot(creator, creatorGuestList, subscriber);

                if (mergedSnapshots.Count > 0 && mergedSnapshots.Last().Timestamp == snapshot.Timestamp)
                {
                    mergedSnapshots.RemoveAt(mergedSnapshots.Count - 1);
                }
                 
                mergedSnapshots.Add(newMergedSnapshot);
            }

            return mergedSnapshots;
        }

        private bool TryAssign<T>(ISnapshot snapshot, ref T target)
            where T : class, ISnapshot
        {
            var cast = snapshot as T;
            if (cast != null)
            {
                target = cast;
                return true;
            }

            return false;
        }

    }
}