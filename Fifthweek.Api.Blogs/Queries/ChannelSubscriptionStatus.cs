namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class ChannelSubscriptionStatus
    {
        public ChannelId ChannelId { get; private set; }

        public string Name { get; private set; }

        public int AcceptedPrice { get; private set; }

        public int PriceInUsCentsPerWeek { get; private set; }

        public bool IsDefault { get; private set; }

        public DateTime PriceLastSetDate { get; private set; }

        public DateTime SubscriptionStartDate { get; private set; }

        public IReadOnlyList<CollectionSubscriptionStatus> Collections { get; private set; }
    }
}