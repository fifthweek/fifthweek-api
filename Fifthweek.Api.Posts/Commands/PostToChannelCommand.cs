namespace Fifthweek.Api.Posts.Commands
{
    using System;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class PostToChannelCommand
    {
        public Requester Requester { get; private set; }

        public PostId NewPostId { get; private set; }

        public ChannelId ChannelId { get; private set; }

        [Optional]
        public FileId FileId { get; private set; }

        [Optional]
        public FileId ImageId { get; private set; }

        [Optional]
        public ValidComment Comment { get; private set; }

        [Optional]
        public DateTime? ScheduledPostTime { get; private set; }

        [Optional]
        public QueueId QueueId { get; private set; }

        public DateTime Timestamp { get; private set; }
    }
}