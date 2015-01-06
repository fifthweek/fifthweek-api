using System;

namespace Fifthweek.Api.Identity.Membership.Queries
{
    using Fifthweek.Api.Core;

    public class IsPasswordResetTokenValidQuery : IQuery<bool>
    {
        public IsPasswordResetTokenValidQuery(UserId userId, string token)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (token == null)
            {
                throw new ArgumentNullException("token");
            }

            this.UserId = userId;
            this.Token = token;
        }

        public UserId UserId { get; private set; }

        public string Token { get; private set; }

        protected bool Equals(IsPasswordResetTokenValidQuery other)
        {
            return Equals(this.UserId, other.UserId) && string.Equals(this.Token, other.Token);
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
            return Equals((IsPasswordResetTokenValidQuery) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.UserId != null ? this.UserId.GetHashCode() : 0)*397) ^ (this.Token != null ? this.Token.GetHashCode() : 0);
            }
        }
    }
}