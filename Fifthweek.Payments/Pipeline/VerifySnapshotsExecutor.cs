namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Snapshots;

    public class VerifySnapshotsExecutor : IVerifySnapshotsExecutor
    {
        public void Execute(
            DateTime startTimeInclusive,
            DateTime endTimeExclusive,
            UserId subscriberId,
            UserId creatorId, 
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

                var creatorChannelSnapshot = snapshot as CreatorChannelsSnapshot;
                if (creatorChannelSnapshot != null)
                {
                    if (!creatorChannelSnapshot.CreatorId.Equals(creatorId))
                    {
                        throw new InvalidOperationException("Unexpected creator id: " + creatorChannelSnapshot.CreatorId);
                    }
                }
                else
                {
                    var creatorFreeAccessUsersSnapshot = snapshot as CreatorFreeAccessUsersSnapshot;
                    if (creatorFreeAccessUsersSnapshot != null)
                    {
                        if (!creatorFreeAccessUsersSnapshot.CreatorId.Equals(creatorId))
                        {
                            throw new InvalidOperationException(
                                "Unexpected creator id: " + creatorFreeAccessUsersSnapshot.CreatorId);
                        }
                    }
                    else
                    {
                        var subscriberChannelSnapshot = snapshot as SubscriberChannelsSnapshot;
                        if (subscriberChannelSnapshot != null)
                        {
                            if (subscriberChannelSnapshot.SubscribedChannels.Any(v => v.SubscriptionStartDate.Kind != DateTimeKind.Utc))
                            {
                                throw new InvalidOperationException("Subscription start dates must be UTC.");
                            }

                            if (!subscriberChannelSnapshot.SubscriberId.Equals(subscriberId))
                            {
                                throw new InvalidOperationException("Unexpected subscriber id: " + subscriberChannelSnapshot.SubscriberId);
                            }
                        }
                        else
                        {
                            var subscriberSnapshot = snapshot as SubscriberSnapshot;
                            if (subscriberSnapshot != null)
                            {
                                if (!subscriberSnapshot.SubscriberId.Equals(subscriberId))
                                {
                                    throw new InvalidOperationException("Unexpected subscriber id: " + subscriberSnapshot.SubscriberId);
                                }
                            }
                            else
                            {
                                var calculatedAccountBalanceSnapshot = snapshot as CalculatedAccountBalanceSnapshot;
                                if (calculatedAccountBalanceSnapshot != null)
                                {
                                    if (!calculatedAccountBalanceSnapshot.UserId.Equals(subscriberId))
                                    {
                                        throw new InvalidOperationException(
                                            "Unexpected subscriber id: " + calculatedAccountBalanceSnapshot.UserId);
                                    }
                                }
                                else
                                {
                                    throw new InvalidOperationException(
                                        "Unknown snapshot type: " + snapshot.GetType().Name);
                                }
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