namespace Fifthweek.Payments.Snapshots
{
    using System;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class SubscriberSnapshot : ISnapshot
    {
        public DateTime Timestamp { get; private set; }

        public UserId SubscriberId { get; private set; }

        [Optional]
        public string Email { get; private set; }

        public static SubscriberSnapshot Default(DateTime timestamp, UserId subscriberId)
        {
            return new SubscriberSnapshot(timestamp, subscriberId, null);
        }
    }
}