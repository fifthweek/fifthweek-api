using System;
using System.Web.Http.ModelBinding;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Subscriptions.Controllers
{
    public class MandatorySubscriptionData
    {
        public Guid SubscriptionId { get; set; }

        public string SubscriptionName { get; set; }

        public string Tagline { get; set; }

        public int BasePrice { get; set; }

        public SubscriptionId SubscriptionIdObject { get; set; }

        public SubscriptionName SubscriptionNameObject { get; set; }

        public Tagline TaglineObject { get; set; }

        public ChannelPriceInUsCentsPerWeek BasePriceObject { get; set; }

        public void Parse()
        {
            var modelState = new ModelStateDictionary();

            this.SubscriptionIdObject = Subscriptions.SubscriptionId.Parse(this.SubscriptionId);
            this.SubscriptionNameObject = this.SubscriptionName.AsSubscriptionName("SubscriptionName", modelState);
            this.TaglineObject = this.Tagline.AsTagline("Tagline", modelState);
            this.BasePriceObject = this.BasePrice.AsChannelPriceInUsCentsPerWeek("BasePrice", modelState);

            if (!modelState.IsValid)
            {
                throw new ModelValidationException(modelState);
            }
        }

        protected bool Equals(MandatorySubscriptionData other)
        {
            return string.Equals(this.SubscriptionId, other.SubscriptionId) && 
                string.Equals(this.SubscriptionName, other.SubscriptionName) && 
                string.Equals(this.Tagline, other.Tagline) && 
                this.BasePrice == other.BasePrice;
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
            return Equals((MandatorySubscriptionData) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.SubscriptionId.GetHashCode();
                hashCode = (hashCode*397) ^ (this.SubscriptionName != null ? this.SubscriptionName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (this.Tagline != null ? this.Tagline.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ this.BasePrice;
                return hashCode;
            }
        }
    }
}