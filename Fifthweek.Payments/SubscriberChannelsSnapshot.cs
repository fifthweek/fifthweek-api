namespace Fifthweek.Payments
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class SubscriberChannelsSnapshot : ISnapshot
    {
        public DateTime Timestamp { get; private set; }

        public Guid SubscriberId { get; private set; }

        public IReadOnlyList<SubscriberChannelsSnapshotItem> SubscribedChannels { get; private set; }

        public static SubscriberChannelsSnapshot Default(DateTime timestamp, Guid subscriberId)
        {
            return new SubscriberChannelsSnapshot(timestamp, subscriberId, new List<SubscriberChannelsSnapshotItem>());
        }
    }
}