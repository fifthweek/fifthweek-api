namespace Fifthweek.Api.Collections.Queries
{
    using System.Collections.Generic;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class ChannelsAndCollections
    {
        public IReadOnlyList<Channel> Channels { get; private set; }

        [AutoConstructor]
        public partial class Channel
        {
            public ChannelId ChannelId { get; private set; }

            public string Name { get; private set; }

            public IReadOnlyList<Collection> Collections { get; private set; }
        }

        [AutoConstructor]
        public partial class Collection
        {
            public CollectionId CollectionId { get; private set; }

            public string Name { get; private set; }
        }
    }
}