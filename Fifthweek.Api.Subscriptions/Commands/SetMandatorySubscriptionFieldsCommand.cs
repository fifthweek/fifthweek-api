using Fifthweek.Api.Core;
using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Subscriptions.Commands
{
    [AutoEqualityMembers, AutoConstructor]
    public partial class SetMandatorySubscriptionFieldsCommand
    {
        public UserId Requester { get; private set; }
        
        public SubscriptionId SubscriptionId { get; private set; }

        public SubscriptionName SubscriptionName { get; private set; }

        public Tagline Tagline { get; private set; }

        public ChannelPriceInUsCentsPerWeek BasePrice { get; private set; }
    }
}