namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetPostQueryResult
    {
        public FullPost Post { get; private set; }

        public IReadOnlyList<File> Files { get; private set; }

        [AutoConstructor, AutoEqualityMembers]
        public partial class FullPost
        {
            public UserId CreatorId { get; private set; }

            public GetPreviewNewsfeedQueryResult.PreviewPostCreator Creator { get; private set; }

            public PostId PostId { get; private set; }

            public BlogId BlogId { get; private set; }
            
            public GetPreviewNewsfeedQueryResult.PreviewPostBlog Blog { get; private set; }

            public ChannelId ChannelId { get; private set; }
           
            public GetPreviewNewsfeedQueryResult.PreviewPostChannel Channel { get; private set; }

            public Comment Content { get; private set; }

            public int PreviewWordCount { get; private set; }

            public int WordCount { get; private set; }

            public int ImageCount { get; private set; }

            public int FileCount { get; private set; }

            public int VideoCount { get; private set; }

            public DateTime LiveDate { get; private set; }

            public int LikesCount { get; private set; }

            public int CommentsCount { get; private set; }

            public bool HasLiked { get; private set; }

            public bool IsPreview { get; private set; }

            public bool IsFreePost { get; private set; }
        }

        [AutoConstructor, AutoEqualityMembers]
        public partial class File
        {
            public FileInformation Information { get; private set; }

            public FileSourceInformation Source { get; private set; }
            
            [Optional]
            public BlobSharedAccessInformation AccessInformation { get; private set; }
        }
    }
}