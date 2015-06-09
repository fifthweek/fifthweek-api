namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class SubscriberChannelsSnapshot : ISnapshot
    {
        public DateTime Timestamp { get; private set; }

        public UserId SubscriberId { get; private set; }

        public IReadOnlyList<SubscriberChannelsSnapshotItem> SubscribedChannels { get; private set; }

        public static SubscriberChannelsSnapshot Default(DateTime timestamp, UserId subscriberId)
        {
            return new SubscriberChannelsSnapshot(timestamp, subscriberId, new List<SubscriberChannelsSnapshotItem>());
        }
    }
}