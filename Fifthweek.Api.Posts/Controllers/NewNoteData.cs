namespace Fifthweek.Api.Posts.Controllers
{
    using System;

    using Fifthweek.Api.Channels;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class NewNoteData
    {
        public ChannelId ChannelId { get; set; }

        [Parsed(typeof(ValidNote))]
        public string Note { get; set; }

        [Optional]
        public DateTime? ScheduledPostDate { get; set; }
    }
}