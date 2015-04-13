namespace Fifthweek.Api.Blogs.Queries
{
    using System;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class ChannelSubscriptionStatus
    {
        public ChannelId ChannelId { get; private set; }

        public string ChannelName { get; private set; }

        public int AcceptedPrice { get; private set; }

        public int CurrentPrice { get; private set; }

        public DateTime PriceLastSetDate { get; private set; }

        public DateTime SubscriptionStartDate { get; private set; }
    }
}