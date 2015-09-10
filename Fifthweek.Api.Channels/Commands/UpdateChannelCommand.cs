namespace Fifthweek.Api.Channels.Commands
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class UpdateChannelCommand
    {
        public Requester Requester { get; private set; }

        public ChannelId ChannelId { get; private set; }

        public ValidChannelName Name { get; private set; }

        public ValidChannelPrice Price { get; private set; }

        public bool IsVisibleToNonSubscribers { get; private set; }
    }
}