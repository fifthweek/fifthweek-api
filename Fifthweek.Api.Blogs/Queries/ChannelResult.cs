namespace Fifthweek.Api.Blogs.Queries
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class ChannelResult
    {
        public ChannelId ChannelId { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public int PriceInUsCentsPerWeek { get; private set; }

        public bool IsDefault { get; private set; }

        public bool IsVisibleToNonSubscribers { get; private set; }
    }
}