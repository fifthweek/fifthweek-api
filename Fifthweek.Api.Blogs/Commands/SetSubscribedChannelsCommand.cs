namespace Fifthweek.Api.Blogs.Commands
{
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;

    public class SetSubscribedChannelsCommand
    {
        public Requester Requester { get; private set; }

        public BlogId BlogId { get; private set; }

        public class ChannelSubscriptionDetails
        {
            public ChannelId ChannelId { get; private set; }

            public int AgreedPrice { get; private set; }
        }
    }
}