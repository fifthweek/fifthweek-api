namespace Fifthweek.Payments
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class CreatorChannelsSnapshot : ISnapshot
    {
        public DateTime Timestamp { get; private set; }

        public Guid CreatorId { get; private set; }

        public IReadOnlyList<CreatorChannelsSnapshotItem> CreatorChannels { get; private set; }

        public static CreatorChannelsSnapshot Default(DateTime timestamp, Guid creatorId)
        {
            return new CreatorChannelsSnapshot(timestamp, creatorId, new List<CreatorChannelsSnapshotItem>());
        }
    }
}
