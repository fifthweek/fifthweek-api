namespace Fifthweek.Api.Subscriptions.Controllers
{
    using System;

    using Fifthweek.Api.Core;

    [AutoEqualityMembers]
    public partial class NewNoteData
    {
        [Optional]
        [Constructed(typeof(ChannelId), IsGuidBase64 = true)]
        public string ChannelId { get; set; }

        [Parsed(typeof(ValidNote))]
        public string Note { get; set; }

        [Optional]
        public DateTime? ScheduledPostDate { get; set; }
    }
}