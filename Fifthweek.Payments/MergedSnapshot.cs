namespace Fifthweek.Payments
{
    using System;
    using System.Linq;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class MergedSnapshot
    {
        public MergedSnapshot(
            CreatorChannelSnapshot creatorChannels, 
            CreatorGuestListSnapshot creatorGuestList, 
            SubscriberChannelSnapshot subscriberChannels,
            SubscriberSnapshot subscriber)
        {
            this.CreatorChannels = creatorChannels;
            this.CreatorGuestList = creatorGuestList;
            this.SubscriberChannels = subscriberChannels;
            this.Subscriber = subscriber;
            this.Timestamp = this.GetMaximumTimestamp();
        }

        public DateTime Timestamp { get; private set; }

        public CreatorChannelSnapshot CreatorChannels { get; private set; }

        public CreatorGuestListSnapshot CreatorGuestList { get; private set; }

        public SubscriberChannelSnapshot SubscriberChannels { get; private set; }

        public SubscriberSnapshot Subscriber { get; private set; }

        private DateTime GetMaximumTimestamp()
        {
            var timestamps = new[] 
            { 
                this.CreatorChannels.Timestamp, 
                this.CreatorGuestList.Timestamp,
                this.SubscriberChannels.Timestamp,
                this.Subscriber.Timestamp,
            };

            return timestamps.Max();
        }
    }
}