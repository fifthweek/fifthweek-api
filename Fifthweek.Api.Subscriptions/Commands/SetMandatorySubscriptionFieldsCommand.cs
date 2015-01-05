using System;

namespace Fifthweek.Api.Subscriptions.Commands
{
    public class SetMandatorySubscriptionFieldsCommand
    {
        public SetMandatorySubscriptionFieldsCommand(SubscriptionId subscriptionId, SubscriptionName subscriptionName, Tagline tagline, WeeklySubscriptionPriceInUSCents basePrice)
        {
            if (subscriptionId == null)
            {
                throw new ArgumentNullException("subscriptionId");
            }

            if (subscriptionName == null)
            {
                throw new ArgumentNullException("subscriptionName");
            }

            if (tagline == null)
            {
                throw new ArgumentNullException("tagline");
            }

            if (basePrice == null)
            {
                throw new ArgumentNullException("basePrice");
            }

            this.SubscriptionId = subscriptionId;
            this.SubscriptionName = subscriptionName;
            this.Tagline = tagline;
            this.BasePrice = basePrice;
        }

        public SubscriptionId SubscriptionId { get; private set; }

        public SubscriptionName SubscriptionName { get; private set; }

        public Tagline Tagline { get; private set; }

        public WeeklySubscriptionPriceInUSCents BasePrice { get; private set; }
    }
}