namespace Fifthweek.Api.Identity.Queries
{
    using Fifthweek.Api.Core;

    public class GetUsernameAvailabilityQuery : IQuery<bool>
    {
        public GetUsernameAvailabilityQuery(string username)
        {
            this.Username = username;
        }

        public string Username { get; private set; }

        protected bool Equals(GetUsernameAvailabilityQuery other)
        {
            return string.Equals(this.Username, other.Username);
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