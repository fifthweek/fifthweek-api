namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class NewPostData
    {
        public NewPostData()
        {
        }

        public ChannelId ChannelId { get; set; }

        [Optional]
        public FileId PreviewImageId { get; set; }

        [Optional]
        [Parsed(typeof(ValidPreviewText))]
        public string PreviewText { get; set; }

        [Optional]
        [Parsed(typeof(ValidComment))]
        public string Content { get; set; }

        [Optional]
        public DateTime? ScheduledPostTime { get; set; }

        [Optional]
        public QueueId QueueId { get; set; }

        public int PreviewWordCount { get; set; }

        public int WordCount { get; set; }

        public int ImageCount { get; set; }

        public int FileCount { get; set; }

        [Optional]
        public List<FileId> FileIds { get; set; }
    }
}