namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Snapshots;

    public class RollForwardSubscriptionsExecutor : IRollForwardSubscriptionsExecutor
    {
        /// <summary>
        /// This method "rolls forward" subscriptions, which means if the user unsubscribed we
        /// make sure the subscription is billed until the end of their billing period by extending
        /// the subscription length here.
        /// </summary>
        public IReadOnlyList<MergedSnapshot> Execute(
            DateTime endTimeExclusive,
            IReadOnlyList<MergedSnapshot> inputSnapshots)
        {
            var snapshots = inputSnapshots.ToList();
            var activeSubscriptions = new Dictionary<ChannelId, ActiveSubscription>();

            for (int i = 0; i < snapshots.Count; i++)
            {
                var snapshot = snapshots[i];

                foreach (var subscription in snapshot.SubscriberChannels.SubscribedChannels)
                {
                    var billingWeekEndDateExclusive =
                        BillingWeekUtilities.CalculateBillingWeekEndDateExclusive(
                            subscription.SubscriptionStartDate,
                            snapshot.Timestamp);
                    ActiveSubscription activeSubscription;
                    if (activeSubscriptions.TryGetValue(subscription.ChannelId, out activeSubscription))
                    {
                        // The following condition is a relaxation to allow people to briefly re-subscribe without
                        // trigging a whole new billing week, and therefore to reduce anticipated complaints.
                        // If they restarted their subscription before the minumum end date of their
                        // previous subscription, then we do not reset the minimum end date yet.
                        // However if they exceed that billing week then the new subscription's billing week will be used.
                        if (snapshot.Timestamp >= activeSubscription.BillingWeekEndDateExclusive)
                        {
                            activeSubscription.BillingWeekEndDateExclusive = billingWeekEndDateExclusive;
                        }
                    }
                    else
                    {
                        activeSubscription = new ActiveSubscription(billingWeekEndDateExclusive, subscription);
                        activeSubscriptions.Add(subscription.ChannelId, activeSubscription);
                    }
                }

                // Copy the list so we can modify activeSubscriptions within the loop.
                foreach (var activeSubscription in activeSubscriptions.Values.ToList())
                {
                    var activeChannelId = activeSubscription.Subscription.ChannelId;
                    if (snapshot.SubscriberChannels.SubscribedChannels.All(v => !v.ChannelId.Equals(activeChannelId)))
                    {
                        // The subscriber has unsubscribed from this channel.
                        var billingWeekFinalSnapshotTime = this.GetBillingWeekFinalSnapshotTime(activeSubscription.BillingWeekEndDateExclusive);
                        
                        if (snapshot.Timestamp == billingWeekFinalSnapshotTime)
                        {
                            // We are at the end of the billing week, so there is nothing to adjust.
                            activeSubscriptions.Remove(activeSubscription.Subscription.ChannelId);
                        }
                        else if (snapshot.Timestamp < billingWeekFinalSnapshotTime)
                        {
                            if (snapshot.CreatorChannels.CreatorChannels.All(v => !v.ChannelId.Equals(activeChannelId)))
                            {
                                // We are within the billing week, but the channel is no longer published.
                                // So we stop billing at this point.
                                activeSubscriptions.Remove(activeSubscription.Subscription.ChannelId);
                            }
                            else
                            {
                                // We are still within their billing week and the creator still publishes the channel.
                                // We must extend their subscription to this snapshot.
                                var newSnapshot = new MergedSnapshot(
                                    snapshot.Timestamp,
                                    snapshot.CreatorChannels,
                                    snapshot.CreatorFreeAccessUsers,
                                    new SubscriberChannelsSnapshot(
                                        snapshot.SubscriberChannels.Timestamp,
                                        snapshot.SubscriberChannels.SubscriberId,
                                        snapshot.SubscriberChannels.SubscribedChannels.Concat(new[] { activeSubscription.Subscription }).ToList()),
                                    snapshot.Subscriber,
                                    snapshot.CalculatedAccountBalance);

                                snapshots[i] = newSnapshot;

                                if ((i == snapshots.Count - 1 && activeSubscription.BillingWeekEndDateExclusive < endTimeExclusive)
                                    || (i < snapshots.Count - 1 && snapshots[i + 1].Timestamp > billingWeekFinalSnapshotTime))
                                {
                                    // Either this is the last snapshot and the billing week ends before the end date of 
                                    // our search range, or the next snapshot will be in the next billing week.
                                    // Either way, we need to insert a snapshot to end the biling at the correct time.
                                    var endSnapshot = new MergedSnapshot(
                                        billingWeekFinalSnapshotTime,
                                        snapshot.CreatorChannels,
                                        snapshot.CreatorFreeAccessUsers,
                                        snapshot.SubscriberChannels,
                                        snapshot.Subscriber,
                                        snapshot.CalculatedAccountBalance);

                                    snapshots.Insert(i + 1, endSnapshot);
                                }

                                snapshot = snapshots[i];
                            }
                        }
                    }
                }
            }

            return snapshots;
        }

        private DateTime GetBillingWeekFinalSnapshotTime(DateTime billingWeekEndDateExclusive)
        {
            return billingWeekEndDateExclusive.AddTicks(-1);
        }

        private class ActiveSubscription
        {
            public ActiveSubscription(DateTime billingWeekEndDateExclusive, SubscriberChannelsSnapshotItem subscription)
            {
                this.BillingWeekEndDateExclusive = billingWeekEndDateExclusive;
                this.Subscription = subscription;
            }

            public SubscriberChannelsSnapshotItem Subscription { get; private set; }

            public DateTime BillingWeekEndDateExclusive { get; set; }
        }
    }
}