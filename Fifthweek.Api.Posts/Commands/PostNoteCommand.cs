namespace Fifthweek.Api.Posts.Commands
{
    using System;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class PostNoteCommand
    {
        public UserId Requester { get; private set; }

        public ChannelId ChannelId { get; private set; }

        public PostId NewPostId { get; private set; }

        public ValidNote Note { get; private set; }

        [Optional]
        public DateTime? ScheduledPostDate { get; private set; }
    }
}