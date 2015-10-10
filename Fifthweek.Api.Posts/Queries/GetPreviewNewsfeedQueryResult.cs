namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetPreviewNewsfeedQueryResult
    {
        public IReadOnlyList<PreviewPost> Posts { get; private set; }

        [AutoConstructor, AutoEqualityMembers]
        public partial class PreviewPost
        {
            public UserId CreatorId { get; private set; }

            public PostId PostId { get; private set; }

            public BlogId BlogId { get; private set; }

            public ChannelId ChannelId { get; private set; }

            [Optional]
            public Comment Comment { get; private set; }

            [Optional]
            public FileInformation File { get; private set; }

            [Optional]
            public FileSourceInformation FileSource { get; private set; }

            [Optional]
            public FileInformation Image { get; private set; }

            [Optional]
            public FileSourceInformation ImageSource { get; private set; }

            [Optional]
            public BlobSharedAccessInformation ImageAccessInformation { get; private set; }

            public DateTime LiveDate { get; private set; }

            public int LikesCount { get; private set; }

            public int CommentsCount { get; private set; }

            public bool HasLiked { get; private set; }
        }
    }
}