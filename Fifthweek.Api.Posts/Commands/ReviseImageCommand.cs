namespace Fifthweek.Api.Posts.Commands
{
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class ReviseImageCommand
    {
        public Requester Requester { get; private set; }

        public PostId PostId { get; private set; }

        public CollectionId CollectionId { get; private set; }

        public FileId ImageFileId { get; private set; }

        [Optional]
        public ValidComment Comment { get; private set; }
    }
}