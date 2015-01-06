namespace Fifthweek.Api.Subscriptions.Controllers
{
    public class MandatorySubscriptionData
    {
        public string SubscriptionId { get; set; }

        public string SubscriptionName { get; set; }

        public string Tagline { get; set; }

        public int BasePrice { get; set; }

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
                int hashCode = (this.SubscriptionId != null ? this.SubscriptionId.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (this.SubscriptionName != null ? this.SubscriptionName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (this.Tagline != null ? this.Tagline.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ this.BasePrice;
                return hashCode;
            }
        }
    }
}