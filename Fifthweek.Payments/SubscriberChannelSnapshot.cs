namespace Fifthweek.Payments
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class SubscriberChannelSnapshot : ISnapshot
    {
        public DateTime Timestamp { get; private set; }

        public Guid SubscriberId { get; private set; }

        public IReadOnlyList<SubscriberChannelSnapshotItem> SubscribedChannels { get; private set; }

        public static SubscriberChannelSnapshot Default(DateTime timestamp, Guid subscriberId)
        {
            return new SubscriberChannelSnapshot(timestamp, subscriberId, new List<SubscriberChannelSnapshotItem>());
        }
    }
}