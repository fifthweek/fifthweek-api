namespace Fifthweek.Payments
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class CreatorChannelSnapshot
    {
        public Guid ChannelId { get; private set; }

        public int Price { get; private set; }
    }
}