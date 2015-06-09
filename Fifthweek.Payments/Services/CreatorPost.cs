namespace Fifthweek.Payments.Services
{
    using System;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CreatorPost
    {
        public ChannelId ChannelId { get; set; }

        public DateTime LiveDate { get; set; }
    }
}