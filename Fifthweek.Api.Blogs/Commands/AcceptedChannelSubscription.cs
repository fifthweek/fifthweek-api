namespace Fifthweek.Api.Blogs.Commands
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class AcceptedChannelSubscription
    {
        public ChannelId ChannelId { get; private set; }

        public ValidAcceptedChannelPrice AcceptedPrice { get; private set; }
    }
}