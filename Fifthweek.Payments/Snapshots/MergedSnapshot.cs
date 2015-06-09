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
            SubscriberSnapshot subscriber)
        {
            this.CreatorChannels = creatorChannels;
            this.CreatorFreeAccessUsers = creatorFreeAccessUsers;
            this.SubscriberChannels = subscriberChannels;
            this.Subscriber = subscriber;
            this.Timestamp = this.GetMaximumTimestamp();
        }

        public DateTime Timestamp { get; private set; }

        public CreatorChannelsSnapshot CreatorChannels { get; private set; }

        public CreatorFreeAccessUsersSnapshot CreatorFreeAccessUsers { get; private set; }

        public SubscriberChannelsSnapshot SubscriberChannels { get; private set; }

        public SubscriberSnapshot Subscriber { get; private set; }

        private DateTime GetMaximumTimestamp()
        {
            var timestamps = new[] 
            { 
                this.CreatorChannels.Timestamp, 
                this.CreatorFreeAccessUsers.Timestamp,
                this.SubscriberChannels.Timestamp,
                this.Subscriber.Timestamp,
            };

            return timestamps.Max();
        }
    }
}