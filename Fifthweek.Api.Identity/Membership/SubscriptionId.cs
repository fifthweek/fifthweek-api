using System;

namespace Fifthweek.Api.Identity.Membership
{
    public class SubscriptionId
    {
        protected SubscriptionId()
        {
        }

        public Guid Value { get; protected set; }

        protected bool Equals(SubscriptionId other)
        {
            return object.Equals(this.Value, other.Value);
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
            return Equals((SubscriptionId)obj);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public static SubscriptionId Parse(Guid value)
        {
            return new SubscriptionId
            {
                Value = value
            };
        }
    }
}