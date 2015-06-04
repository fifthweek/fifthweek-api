namespace Fifthweek.Payments
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class CreatorSnapshot : ISnapshot
    {
        public DateTime Timestamp { get; private set; }

        public Guid CreatorId { get; private set; }

        public IReadOnlyList<CreatorChannelSnapshot> CreatorChannels { get; private set; }

        public static CreatorSnapshot Default(DateTime timestamp, Guid creatorId)
        {
            return new CreatorSnapshot(timestamp, creatorId, new List<CreatorChannelSnapshot>());
        }
    }
}
