namespace Fifthweek.Api.Posts.Commands
{
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class RevisePostCommand
    {
        public Requester Requester { get; private set; }

        public PostId PostId { get; private set; }

        [Optional]
        public FileId FileId { get; private set; }

        [Optional]
        public FileId ImageId { get; private set; }

        [Optional]
        public ValidComment Comment { get; private set; }
    }
}