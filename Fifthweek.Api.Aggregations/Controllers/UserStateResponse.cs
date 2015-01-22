namespace Fifthweek.Api.Aggregations.Controllers
{
    using System.Collections.Generic;

    using Fifthweek.Api.Subscriptions.Controllers;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class UserStateResponse
    {
        [Optional]
        public CreatorStatusResponse CreatorStatus { get; private set; }

        [Optional]
        public ChannelsAndCollectionsResponse CreatedChannelsAndCollections { get; private set; }

        [AutoConstructor]
        public partial class ChannelsAndCollectionsResponse
        {
            public IReadOnlyList<ChannelResponse> Channels { get; private set; }

            [AutoConstructor]
            public partial class ChannelResponse
            {
                public string ChannelId { get; private set; }

                public string Name { get; private set; }

                public IReadOnlyList<CollectionResponse> Collections { get; private set; }
            }

            [AutoConstructor]
            public partial class CollectionResponse
            {
                public string CollectionId { get; private set; }

                public string Name { get; private set; }
            }
        }

        [AutoConstructor, AutoEqualityMembers]
        public partial class CreatorStatusResponse
        {
            [Optional]
            public string SubscriptionId { get; private set; }

            public bool MustWriteFirstPost { get; private set; }
        }
    }
}