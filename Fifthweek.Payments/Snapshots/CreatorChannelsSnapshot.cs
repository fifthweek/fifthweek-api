namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class CreatorChannelsSnapshot : ISnapshot
    {
        public DateTime Timestamp { get; private set; }

        public UserId CreatorId { get; private set; }

        public IReadOnlyList<CreatorChannelsSnapshotItem> CreatorChannels { get; private set; }

        public static CreatorChannelsSnapshot Default(DateTime timestamp, UserId creatorId)
        {
            return new CreatorChannelsSnapshot(timestamp, creatorId, new List<CreatorChannelsSnapshotItem>());
        }
    }
}
