namespace Fifthweek.Api.Posts.Commands
{
    using System;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class PostImageCommand
    {
        public Requester Requester { get; private set; }

        public PostId NewPostId { get; private set; }

        public CollectionId CollectionId { get; private set; }

        public FileId ImageFileId { get; private set; }

        [Optional]
        public ValidComment Comment { get; private set; }

        [Optional]
        public DateTime? ScheduledPostDate { get; private set; }

        public bool IsQueued { get; private set; }
    }
}