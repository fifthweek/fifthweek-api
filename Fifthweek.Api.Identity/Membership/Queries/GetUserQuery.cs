using System;

namespace Fifthweek.Api.Identity.Membership.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    public class GetUserQuery : IQuery<FifthweekUser>
    {
        public GetUserQuery(NormalizedUsername username, Password password)
        {
            if (username == null)
            {
                throw new ArgumentNullException("username");
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            this.Username = username;
            this.Password = password;
        }

        public NormalizedUsername Username { get; private set; }

        public Password Password { get; private set; }

        protected bool Equals(GetUserQuery other)
        {
            return Equals(this.Username, other.Username) && Equals(this.Password, other.Password);
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
            return Equals((GetUserQuery) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.Username != null ? this.Username.GetHashCode() : 0)*397) ^ (this.Password != null ? this.Password.GetHashCode() : 0);
            }
        }
    }
}