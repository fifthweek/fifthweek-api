namespace Fifthweek.Api.Channels.Commands
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class DeleteChannelCommand
    {
        public Requester Requester { get; private set; }

        public ChannelId ChannelId { get; private set; }
    }
}