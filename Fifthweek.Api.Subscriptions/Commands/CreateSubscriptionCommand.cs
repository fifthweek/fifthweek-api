namespace Fifthweek.Api.Subscriptions.Commands
{
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class CreateSubscriptionCommand
    {
        public Requester Requester { get; private set; }
        
        public SubscriptionId NewSubscriptionId { get; private set; }

        public ValidSubscriptionName SubscriptionName { get; private set; }

        public ValidTagline Tagline { get; private set; }

        public ValidChannelPriceInUsCentsPerWeek BasePrice { get; private set; }
    }
}