namespace Fifthweek.Payments
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class CreatorChannelSnapshot : ISnapshot
    {
        public DateTime Timestamp { get; private set; }

        public Guid CreatorId { get; private set; }

        public IReadOnlyList<CreatorChannelSnapshotItem> CreatorChannels { get; private set; }

        public static CreatorChannelSnapshot Default(DateTime timestamp, Guid creatorId)
        {
            return new CreatorChannelSnapshot(timestamp, creatorId, new List<CreatorChannelSnapshotItem>());
        }
    }
}
