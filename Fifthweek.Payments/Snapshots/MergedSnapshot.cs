namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class MergedSnapshot
    {
        public MergedSnapshot(
            CreatorChannelsSnapshot creatorChannels, 
            CreatorFreeAccessUsersSnapshot creatorFreeAccessUsers, 
            SubscriberChannelsSnapshot subscriberChannels,
            SubscriberSnapshot subscriber,
            CalculatedAccountBalanceSnapshot calculatedAccountBalance)
        {
            this.CreatorChannels = creatorChannels;
            this.CreatorFreeAccessUsers = creatorFreeAccessUsers;
            this.SubscriberChannels = subscriberChannels;
            this.Subscriber = subscriber;
            this.CalculatedAccountBalance = calculatedAccountBalance;
            this.Timestamp = this.GetMaximumTimestamp();
        }

        public DateTime Timestamp { get; private set; }

        public CreatorChannelsSnapshot CreatorChannels { get; private set; }

        public CreatorFreeAccessUsersSnapshot CreatorFreeAccessUsers { get; private set; }

        public SubscriberChannelsSnapshot SubscriberChannels { get; private set; }

        public SubscriberSnapshot Subscriber { get; private set; }

        public CalculatedAccountBalanceSnapshot CalculatedAccountBalance { get; private set; }

        private DateTime GetMaximumTimestamp()
        {
            var timestamps = new[] 
            { 
                this.CreatorChannels.Timestamp, 
                this.CreatorFreeAccessUsers.Timestamp,
                this.SubscriberChannels.Timestamp,
                this.Subscriber.Timestamp,
                this.CalculatedAccountBalance.Timestamp,
            };

            return timestamps.Max();
        }
    }
}