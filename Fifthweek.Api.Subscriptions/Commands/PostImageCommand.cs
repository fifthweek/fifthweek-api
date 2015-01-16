namespace Fifthweek.Api.Subscriptions.Commands
{
    using System;

    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class PostImageCommand
    {
        public UserId Requester { get; private set; }

        public CollectionId CollectionId { get; private set; }

        public PostId NewPostId { get; private set; }

        public FileId ImageFileId { get; private set; }

        [Optional]
        public DateTime? ScheduledPostDate { get; private set; }

        public bool IsQueued { get; private set; }
    }
}