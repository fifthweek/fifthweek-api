namespace Fifthweek.Payments
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class SubscriberSnapshot : ISnapshot
    {
        public DateTime Timestamp { get; private set; }

        public Guid SubscriberId { get; private set; }

        [Optional]
        public string Email { get; private set; }

        public IReadOnlyList<SubscriberChannelSnapshot> SubscribedChannels { get; private set; }
        
        public static SubscriberSnapshot Default(DateTime timestamp, Guid subscriberId)
        {
            return new SubscriberSnapshot(timestamp, subscriberId, null, new List<SubscriberChannelSnapshot>());
        }
    }
}