namespace Fifthweek.Api.Subscriptions.Commands
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;

    [AutoEqualityMembers, AutoConstructor]
    public partial class CreateSubscriptionCommand
    {
        public UserId Requester { get; private set; }
        
        public SubscriptionId NewSubscriptionId { get; private set; }

        public SubscriptionName SubscriptionName { get; private set; }

        public Tagline Tagline { get; private set; }

        public ChannelPriceInUsCentsPerWeek BasePrice { get; private set; }
    }
}