namespace Fifthweek.Api.Posts.Controllers
{
    using System;

    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class NewNoteData
    {
        [Constructed(typeof(ChannelId), IsGuidBase64 = true)]
        public string ChannelId { get; set; }

        [Parsed(typeof(ValidNote))]
        public string Note { get; set; }

        [Optional]
        public DateTime? ScheduledPostDate { get; set; }
    }
}