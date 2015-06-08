namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class VerifySnapshotsExecutor : IVerifySnapshotsExecutor
    {
        public void Execute(
            DateTime startTimeInclusive,
            DateTime endTimeExclusive,
            Guid subscriberId, 
            Guid creatorId, 
            IReadOnlyList<ISnapshot> snapshots)
        {
            foreach (var snapshot in snapshots)
            {
                if (snapshot.Timestamp >= endTimeExclusive)
                {
                    throw new InvalidOperationException("Unexpected snapshot with timestamp greater than or equal to end time.");
                }

                if (snapshot.Timestamp.Kind != DateTimeKind.Utc)
                {
                    throw new InvalidOperationException("Timestamps must be UTC.");
                }

                var creatorChannelSnapshot = snapshot as CreatorChannelSnapshot;
                if (creatorChannelSnapshot != null)
                {
                    if (creatorChannelSnapshot.CreatorId != creatorId)
                    {
                        throw new InvalidOperationException("Unexpected creator id: " + creatorChannelSnapshot.CreatorId);
                    }
                }
                else
                {
                    var creatorGuestListSnapshot = snapshot as CreatorGuestListSnapshot;
                    if (creatorGuestListSnapshot != null)
                    {
                        if (creatorGuestListSnapshot.CreatorId != creatorId)
                        {
                            throw new InvalidOperationException(
                                "Unexpected creator id: " + creatorGuestListSnapshot.CreatorId);
                        }
                    }
                    else
                    {
                        var subscriberChannelSnapshot = snapshot as SubscriberChannelSnapshot;
                        if (subscriberChannelSnapshot != null)
                        {
                            if (subscriberChannelSnapshot.SubscribedChannels.Any(v => v.SubscriptionStartDate.Kind != DateTimeKind.Utc))
                            {
                                throw new InvalidOperationException("Subscription start dates must be UTC.");
                            }

                            if (subscriberChannelSnapshot.SubscriberId != subscriberId)
                            {
                                throw new InvalidOperationException("Unexpected subscriber id: " + subscriberChannelSnapshot.SubscriberId);
                            }
                        }
                        else
                        {
                            var subscriberSnapshot = snapshot as SubscriberSnapshot;
                            if (subscriberSnapshot != null)
                            {
                                if (subscriberSnapshot.SubscriberId != subscriberId)
                                {
                                    throw new InvalidOperationException("Unexpected subscriber id: " + subscriberSnapshot.SubscriberId);
                                }
                            }
                            else
                            {
                                throw new InvalidOperationException("Unknown snapshot type: " + snapshot.GetType().Name);
                            }
                        }
                    }
                }
            }

            if (snapshots.Count > 1)
            {
                var lastTimestamp = snapshots[0].Timestamp;
                foreach (var snapshot in snapshots)
                {
                    if (snapshot.Timestamp < lastTimestamp)
                    {
                        throw new InvalidOperationException("Snapshots were not in order.");
                    }

                    lastTimestamp = snapshot.Timestamp;
                }
            }
        }
    }
}