﻿namespace Fifthweek.Api.Posts.Queries
{
    using System;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoCopy]
    public partial class NewsfeedPost
    {
        public UserId CreatorId { get; private set; }

        public PostId PostId { get; private set; }

        public BlogId BlogId { get; private set; }

        public ChannelId ChannelId { get; private set; }

        [Optional]
        public PreviewText PreviewText { get; private set; }

        [Optional]
        public FileId ImageId { get; private set; }

        public int PreviewWordCount { get; private set; }

        public int WordCount { get; private set; }

        public int ImageCount { get; private set; }

        public int FileCount { get; private set; }

        public int VideoCount { get; private set; }

        public DateTime LiveDate { get; private set; }

        [Optional]
        public string ImageName { get; private set; }

        [Optional]
        public string ImageExtension { get; private set; }

        [Optional]
        public long? ImageSize { get; private set; }

        [Optional]
        public int? ImageRenderWidth { get; private set; }

        [Optional]
        public int? ImageRenderHeight { get; private set; }

        public int LikesCount { get; private set; }

        public int CommentsCount { get; private set; }

        public bool HasLikedPost { get; private set; }

        public DateTime CreationDate { get; private set; }
    }
}