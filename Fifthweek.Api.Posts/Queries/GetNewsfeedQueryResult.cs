namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetNewsfeedQueryResult
    {
        public IReadOnlyList<Post> Posts { get; private set; }

        public int AccountBalance { get; private set; }

        [AutoConstructor, AutoEqualityMembers]
        public partial class Post
        {
            public UserId CreatorId { get; private set; }

            public PostId PostId { get; private set; }

            public BlogId BlogId { get; private set; }

            public ChannelId ChannelId { get; private set; }


            [Optional]
            public PreviewText PreviewText { get; private set; }

            [Optional]
            public FileInformation Image { get; private set; }

            [Optional]
            public FileSourceInformation ImageSource { get; private set; }

            public int PreviewWordCount { get; private set; }

            public int WordCount { get; private set; }

            public int ImageCount { get; private set; }

            public int FileCount { get; private set; }

            public DateTime LiveDate { get; private set; }

            public int LikesCount { get; private set; }

            public int CommentsCount { get; private set; }

            public bool HasLiked { get; private set; }
        }
    }
}