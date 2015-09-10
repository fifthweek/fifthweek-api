namespace Fifthweek.Api.Posts.Controllers
{
    using System;

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
        public FileId FileId { get; set; }

        [Optional]
        public FileId ImageId { get; set; }

        [Optional]
        [Parsed(typeof(ValidComment))]
        public string Comment { get; set; }

        [Optional]
        public DateTime? ScheduledPostTime { get; set; }

        [Optional]
        public QueueId QueueId { get; set; }
    }
}