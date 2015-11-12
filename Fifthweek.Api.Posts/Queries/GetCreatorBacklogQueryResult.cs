namespace Fifthweek.Api.Posts.Queries
{
    using System;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetCreatorBacklogQueryResult
    {
        public PostId PostId { get; private set; }

        public ChannelId ChannelId { get; private set; }

        [Optional]
        public QueueId QueueId { get; private set; }

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

        public int VideoCount { get; private set; }

        public DateTime LiveDate { get; private set; }
    }
}