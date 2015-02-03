namespace Fifthweek.Api.Posts.Commands
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class ReviseNoteCommand
    {
        public Requester Requester { get; private set; }

        public PostId PostId { get; private set; }

        public ChannelId ChannelId { get; private set; }

        public ValidNote Note { get; private set; }
    }
}