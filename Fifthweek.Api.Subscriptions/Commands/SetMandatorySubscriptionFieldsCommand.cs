using System;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Subscriptions.Commands
{
    [AutoEqualityMembers, AutoConstructor]
    public partial class SetMandatorySubscriptionFieldsCommand
    {
        public SubscriptionId SubscriptionId { get; private set; }

        public SubscriptionName SubscriptionName { get; private set; }

        public Tagline Tagline { get; private set; }

        public ChannelPriceInUsCentsPerWeek BasePrice { get; private set; }
    }
}