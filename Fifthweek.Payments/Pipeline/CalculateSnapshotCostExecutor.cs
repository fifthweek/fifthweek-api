namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Pipeline;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Snapshots;

    public class CalculateSnapshotCostExecutor : ICalculateSnapshotCostExecutor
    {
        public int Execute(MergedSnapshot snapshot, IReadOnlyList<CreatorPost> creatorPosts)
        {
            if (snapshot.SubscriberChannels.SubscribedChannels.Count == 0)
            {
                return 0;
            }

            if (snapshot.Subscriber.Email != null && snapshot.CreatorFreeAccessUsers.FreeAccessUserEmails.Contains(snapshot.Subscriber.Email))
            {
                return 0;
            }

            var creatorSubscriptions 
                = from s in snapshot.SubscriberChannels.SubscribedChannels
                  let c = snapshot.CreatorChannels.CreatorChannels.FirstOrDefault(v => v.ChannelId.Equals(s.ChannelId))
                  where c != null
                  select new
                  {
                      Subscription = s,
                      Channel = c
                  };

            var subscriptionsWithPosts = creatorSubscriptions.Where(
                    v => this.CreatorPostedInBillingWeek(
                        v.Subscription.SubscriptionStartDate,
                        v.Subscription.ChannelId,
                        creatorPosts));

            var acceptedSubscriptions = subscriptionsWithPosts.Where(v => v.Subscription.AcceptedPrice >= v.Channel.Price);

            var totalCost = acceptedSubscriptions.Aggregate(0, (a, s) => a + s.Channel.Price);

            return totalCost;
        }

        private bool CreatorPostedInBillingWeek(
            DateTime subscriptionStartDateInclusive,
            ChannelId channelId, 
            IReadOnlyList<CreatorPost> creatorPosts)
        {
            var endDateExclusive = subscriptionStartDateInclusive.AddDays(7);
            return creatorPosts.Any(
                v => v.ChannelId.Equals(channelId) 
                    && v.LiveDate >= subscriptionStartDateInclusive 
                    && v.LiveDate < endDateExclusive);
        }
    }
}