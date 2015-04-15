namespace Fifthweek.Api.Blogs.Commands
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class AcceptChannelSubscriptionPriceChangeCommand
    {
        public Requester Requester { get; private set; }

        public ChannelId ChannelId { get; private set; }

        public ValidAcceptedChannelPriceInUsCentsPerWeek AcceptedPrice { get; private set; }
    }
}