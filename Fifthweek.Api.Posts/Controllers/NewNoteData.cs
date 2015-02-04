namespace Fifthweek.Api.Posts.Controllers
{
    using System;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class NewNoteData
    {
        public NewNoteData()
        {
        }

        public ChannelId ChannelId { get; set; }

        [Parsed(typeof(ValidNote))]
        public string Note { get; set; }

        [Optional]
        public DateTime? ScheduledPostTime { get; set; }
    }
}