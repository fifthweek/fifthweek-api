namespace Fifthweek.Payments
{
    using System.Linq;

    using Fifthweek.Payments.Pipeline;

    public class SnapshotCostCalculator : ICalculateSnapshotCostExecutor
    {
        public int Execute(MergedSnapshot snapshot)
        {
            if (snapshot.Subscriber.SubscribedChannels.Count == 0)
            {
                return 0;
            }

            if (snapshot.Subscriber.Email != null && snapshot.CreatorGuestList.GuestListEmails.Contains(snapshot.Subscriber.Email))
            {
                return 0;
            }

            var creatorSubscriptions 
                = from s in snapshot.Subscriber.SubscribedChannels
                  let c = snapshot.Creator.CreatorChannels.FirstOrDefault(v => v.ChannelId == s.ChannelId)
                  where c != null
                  select new
                  {
                      Subscription = s,
                      Channel = c
                  };

            var acceptedSubscriptions = creatorSubscriptions.Where(v => v.Subscription.AcceptedPrice >= v.Channel.Price);

            var totalCost = acceptedSubscriptions.Aggregate(0, (a, s) => a + s.Channel.Price);

            return totalCost;
        }
    }
}