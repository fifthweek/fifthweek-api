using System;

namespace Fifthweek.Api.Identity.Membership
{
    public class UserId
    {
        protected UserId()
        {
        }

        public Guid Value { get; protected set; }

        protected bool Equals(UserId other)
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
            return Equals((UserId)obj);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public static UserId Parse(Guid value)
        {
            return new UserId
            {
                Value = value
            };
        }
    }
}