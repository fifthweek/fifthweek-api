namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Snapshots;

    public class MergeSnapshotsExecutor : IMergeSnapshotsExecutor
    {
        public IReadOnlyList<MergedSnapshot> Execute(
            UserId subscriberId,
            UserId creatorId,
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

            var creatorChannels = CreatorChannelsSnapshot.Default(initialTimestamp, creatorId);
            var creatorFreeAccessUsers = CreatorFreeAccessUsersSnapshot.Default(initialTimestamp, creatorId);
            var subscriberChannels = SubscriberChannelsSnapshot.Default(initialTimestamp, subscriberId);
            var subscriber = SubscriberSnapshot.Default(initialTimestamp, subscriberId);
            var calculatedAccountBalance = CalculatedAccountBalanceSnapshot.Default(initialTimestamp, subscriberId, LedgerAccountType.Fifthweek);

            var mergedSnapshots = new List<MergedSnapshot>();

            if (firstTimestamp > initialTimestamp)
            {
                mergedSnapshots.Add(new MergedSnapshot(creatorChannels, creatorFreeAccessUsers, subscriberChannels, subscriber, calculatedAccountBalance));
            }

            foreach (var snapshot in snapshots)
            {
                var assigned = this.TryAssign(snapshot, ref creatorChannels)
                                || this.TryAssign(snapshot, ref creatorFreeAccessUsers)
                                || this.TryAssign(snapshot, ref subscriberChannels)
                                || this.TryAssign(snapshot, ref subscriber)
                                || this.TryAssign(snapshot, ref calculatedAccountBalance);

                if (!assigned)
                {
                    throw new InvalidOperationException("Unknown snapshot type: " + snapshot.GetType().Name);
                }

                var newMergedSnapshot = new MergedSnapshot(creatorChannels, creatorFreeAccessUsers, subscriberChannels, subscriber, calculatedAccountBalance);

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