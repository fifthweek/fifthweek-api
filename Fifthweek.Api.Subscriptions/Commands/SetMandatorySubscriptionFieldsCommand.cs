using System;

namespace Fifthweek.Api.Subscriptions.Commands
{
    public class SetMandatorySubscriptionFieldsCommand
    {
        public SetMandatorySubscriptionFieldsCommand(SubscriptionId subscriptionId)
        {
            if (subscriptionId == null)
            {
                throw new ArgumentNullException("subscriptionId");
            }

            this.SubscriptionId = subscriptionId;
        }

        public SubscriptionId SubscriptionId { get; private set; }

        public string SubscriptionName { get; private set; }

        public string Tagline { get; private set; }

        public int BaseWeeklyPrice { get; private set; }
    }
}