namespace Fifthweek.Api.Identity.Membership.Queries
{
    using Fifthweek.Api.Core;

    public class IsUsernameAvailableQuery : IQuery<bool>
    {
        public IsUsernameAvailableQuery(NormalizedUsername username)
        {
            this.Username = username;
        }

        public NormalizedUsername Username { get; private set; }

        protected bool Equals(IsUsernameAvailableQuery other)
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
            return this.Equals((IsUsernameAvailableQuery) obj);
        }

        public override int GetHashCode()
        {
            return (this.Username != null ? this.Username.GetHashCode() : 0);
        }
    }
}