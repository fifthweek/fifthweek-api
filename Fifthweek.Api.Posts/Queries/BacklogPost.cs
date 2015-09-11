namespace Fifthweek.Api.Posts.Queries
{
    using System;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoCopy]
    public partial class BacklogPost
    {
        public PostId PostId { get; private set; }

        public ChannelId ChannelId { get; private set; }

        [Optional]
        public QueueId QueueId { get; private set; }

        [Optional]
        public Comment Comment { get; private set; }

        [Optional]
        public FileId FileId { get; private set; }

        [Optional]
        public FileId ImageId { get; private set; }

        public DateTime LiveDate { get; private set; }

        [Optional]
        public string FileName { get; private set; }

        [Optional]
        public string FileExtension { get; private set; }

        [Optional]
        public long? FileSize { get; private set; }

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

        public DateTime CreationDate { get; private set; }
    }
}