namespace Fifthweek.Api.Posts.Commands
{
    using System.Collections.Generic;

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
        public FileId PreviewImageId { get; private set; }

        [Optional]
        public ValidPreviewText PreviewText { get; private set; }

        [Optional]
        public ValidComment Content { get; private set; }

        public int PreviewWordCount { get; private set; }

        public int WordCount { get; private set; }

        public int ImageCount { get; private set; }

        public int FileCount { get; private set; }

        public int VideoCount { get; private set; }

        public IReadOnlyList<FileId> FileIds { get; private set; }
    }
}