namespace Fifthweek.Api.Posts.Commands
{
    using System;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class ReviseFileCommand
    {
        public Requester Requester { get; private set; }

        public PostId PostId { get; private set; }

        public CollectionId CollectionId { get; private set; }

        public FileId FileId { get; private set; }

        [Optional]
        public ValidComment Comment { get; private set; }

        [Optional]
        public DateTime? ScheduledPostDate { get; private set; }

        public bool IsQueued { get; private set; }
    }
}