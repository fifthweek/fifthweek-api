namespace Fifthweek.Payments
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class MergedSnapshot
    {
        public MergedSnapshot(CreatorSnapshot creator, CreatorGuestListSnapshot creatorGuestList, SubscriberSnapshot subscriber)
        {
            this.Timestamp = this.GetMaximumTimestamp(creator.Timestamp, creatorGuestList.Timestamp, subscriber.Timestamp);
            this.Creator = creator;
            this.CreatorGuestList = creatorGuestList;
            this.Subscriber = subscriber;
        }

        public DateTime Timestamp { get; private set; }

        public CreatorSnapshot Creator { get; private set; }

        public CreatorGuestListSnapshot CreatorGuestList { get; private set; }

        public SubscriberSnapshot Subscriber { get; private set; }

        private DateTime GetMaximumTimestamp(DateTime first, DateTime second, DateTime third)
        {
            return new DateTime(Math.Max(first.Ticks, Math.Max(second.Ticks, third.Ticks)));
        }
    }
}