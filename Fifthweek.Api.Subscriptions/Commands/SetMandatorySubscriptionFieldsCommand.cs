using System;

namespace Fifthweek.Api.Subscriptions.Commands
{
    public class SetMandatorySubscriptionFieldsCommand
    {
        public SetMandatorySubscriptionFieldsCommand(SubscriptionId subscriptionId, SubscriptionName subscriptionName, Tagline tagline, UsCentsPerWeek basePrice)
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

        public UsCentsPerWeek BasePrice { get; private set; }

        protected bool Equals(SetMandatorySubscriptionFieldsCommand other)
        {
            return Equals(this.SubscriptionId, other.SubscriptionId) && 
                Equals(this.SubscriptionName, other.SubscriptionName) && 
                Equals(this.Tagline, other.Tagline) && 
                Equals(this.BasePrice, other.BasePrice);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((SetMandatorySubscriptionFieldsCommand) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (this.SubscriptionId != null ? this.SubscriptionId.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (this.SubscriptionName != null ? this.SubscriptionName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (this.Tagline != null ? this.Tagline.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (this.BasePrice != null ? this.BasePrice.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}