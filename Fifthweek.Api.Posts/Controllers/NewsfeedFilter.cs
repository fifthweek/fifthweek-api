namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers]
    public partial class NewsfeedFilter
    {
        [Optional]
        public string CreatorId { get; set; }

        [Optional]
        public string ChannelId { get; set; }

        [Optional]
        public DateTime? Origin { get; set; }

        public bool SearchForwards { get; set; }

        [Parsed(typeof(NonNegativeInt))]
        public int StartIndex { get; set; }

        [Parsed(typeof(PositiveInt))]
        public int Count { get; set; }
    }
}