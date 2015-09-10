namespace Fifthweek.Api.Blogs.Queries
{
    using System.Collections.Generic;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class ChannelResult
    {
        public ChannelId ChannelId { get; private set; }

        public string Name { get; private set; }

        public int Price { get; private set; }

        public bool IsVisibleToNonSubscribers { get; private set; }
    }
}