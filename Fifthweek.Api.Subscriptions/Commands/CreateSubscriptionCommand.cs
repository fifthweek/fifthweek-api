namespace Fifthweek.Api.Subscriptions.Commands
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers, AutoConstructor]
    public partial class CreateSubscriptionCommand
    {
        public UserId Requester { get; private set; }
        
        public SubscriptionId NewSubscriptionId { get; private set; }

        public ValidSubscriptionName SubscriptionName { get; private set; }

        public ValidTagline Tagline { get; private set; }

        public ChannelPriceInUsCentsPerWeek BasePrice { get; private set; }
    }
}