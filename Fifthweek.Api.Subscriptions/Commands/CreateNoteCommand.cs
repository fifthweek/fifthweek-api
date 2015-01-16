namespace Fifthweek.Api.Subscriptions.Commands
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CreateNoteCommand
    {
        public UserId Requester { get; private set; }

        public ChannelId ChannelId { get; private set; }

        public PostId NewPostId { get; private set; }

        public ValidNote Note { get; private set; }

        [Optional]
        public DateTime? ScheduledPostDate { get; private set; }
    }
}