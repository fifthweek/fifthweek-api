namespace Fifthweek.Api.Blogs.Commands
{
    using System;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class SubscribeToChannelCommand
    {
        public Requester Requester { get; private set; }

        public ChannelId ChannelId { get; private set; }

        public ValidAcceptedChannelPrice AcceptedPrice { get; private set; }

        public DateTime Timestamp { get; private set; }
    }
}
