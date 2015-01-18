namespace Fifthweek.Api.Identity.Membership
{
    using System;

    using Fifthweek.Api.Core;

    public class Requester
    {
        public static readonly Requester Unauthenticated = new Requester();

        private readonly UserId userId;

        private Requester(UserId userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            this.userId = userId;
        }

        private Requester()
        {
        }

        public bool IsAuthenticated
        {
            get
            {
                return this.userId != null;
            }
        }

        public static Requester Authenticated(UserId userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            return new Requester(userId);
        }

        public void AssertAuthenticated()
        {
            if (!this.IsAuthenticated)
            {
                throw new UnauthorizedException("The user was not authenticated.");
            }
        }

        public void AssertAuthenticated(out UserId userId)
        {
            if (this.IsAuthenticated)
            {
                userId = this.userId;
            }
            else
            {
                throw new UnauthorizedException("The user was not authenticated.");
            }
        }

        public override string ToString()
        {
            return this.userId == null 
                ? "Requester(Unauthenticated)" 
                : string.Format("Requester({0})", this.userId);
        }

        public override int GetHashCode()
        {
            return (this.userId != null ? this.userId.GetHashCode() : 0);
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
            return Equals((Requester)obj);
        }

        protected bool Equals(Requester other)
        {
            return Equals(this.userId, other.userId);
        }
    }
}