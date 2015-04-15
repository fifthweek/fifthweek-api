﻿namespace Fifthweek.Api.Blogs.Controllers
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class ChannelSubscriptionDataWithChannelId
    {
        public string ChannelId { get; set; }

        [Parsed(typeof(ValidAcceptedChannelPriceInUsCentsPerWeek))]
        public int AcceptedPrice { get; set; }
    }
}