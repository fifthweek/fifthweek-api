namespace Fifthweek.Payments.Snapshots
{
    using System;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class CreatorChannelsSnapshotItem
    {
        public ChannelId ChannelId { get; private set; }

        public int Price { get; private set; }
    }
}