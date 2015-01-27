namespace Fifthweek.Api.Posts.Controllers
{
    using System;

    using Fifthweek.Api.Channels;
    using Fifthweek.CodeGeneration;

    using ChannelId = Fifthweek.Api.Channels.Shared.ChannelId;

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