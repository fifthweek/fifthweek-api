namespace Fifthweek.Payments.Snapshots
{
    using System;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class SubscriberChannelsSnapshotItem
    {
        public ChannelId ChannelId { get; private set; }

        public int AcceptedPrice { get; private set; }

        public DateTime SubscriptionStartDate { get; private set; }
    }
}