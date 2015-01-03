namespace Fifthweek.Api.Identity.Membership.Queries
{
    using Fifthweek.Api.Core;

    public class GetUsernameAvailabilityQuery : IQuery<bool>
    {
        public GetUsernameAvailabilityQuery(NormalizedUsername username)
        {
            this.Username = username;
        }

        public NormalizedUsername Username { get; private set; }

        protected bool Equals(GetUsernameAvailabilityQuery other)
        {
            return object.Equals(this.Username, other.Username);
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
            return this.Equals((GetUsernameAvailabilityQuery) obj);
        }

        public override int GetHashCode()
        {
            return (this.Username != null ? this.Username.GetHashCode() : 0);
        }
    }
}