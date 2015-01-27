namespace Fifthweek.Api.Identity.Shared.Membership
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Api.Core;

    public class Requester
    {
        public static readonly Requester Unauthenticated = new Requester();

        private readonly Shared.Membership.UserId userId;

        private readonly HashSet<string> roles;

        private Requester(Shared.Membership.UserId userId, IEnumerable<string> roles)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            this.userId = userId;
            this.roles = new HashSet<string>(roles ?? Enumerable.Empty<string>());
        }

        private Requester()
        {
            this.roles = new HashSet<string>();
        }

        internal Shared.Membership.UserId UserId
        {
            get
            {
                return this.userId;
            }
        }

        internal IEnumerable<string> Roles 
        {
            get
            {
                return this.roles.ToList();
            }
        }

        public static Requester Authenticated(Shared.Membership.UserId userId, params string[] roles)
        {
            return Authenticated(userId, (IEnumerable<string>)roles);
        }

        public static Requester Authenticated(Shared.Membership.UserId userId, IEnumerable<string> roles)
        {
            userId.AssertNotNull("userId");
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            return new Requester(userId, roles);
        }

        public override string ToString()
        {
            return this.userId == null 
                ? "Requester(Unauthenticated)" 
                : string.Format("Requester({0}, [{1}])", this.userId, string.Join(", ", this.roles));
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

            return this.Equals((Requester)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                
                hashCode = (hashCode * 397) ^ (this.userId != null ? this.userId.GetHashCode() : 0);
                
                var sortedRoles = new List<string>(this.roles);
                sortedRoles.Sort();
                foreach (var item in sortedRoles)
                {
                    hashCode = (hashCode * 397) ^ (item != null ? item.GetHashCode() : 0);
                }

                return hashCode;
            }
        }

        internal bool IsInRole(string role)
        {
            role.AssertNotNull("role");
            return this.roles.Contains(role);
        }

        protected bool Equals(Requester other)
        {
            if (!object.Equals(this.userId, other.userId))
            {
                return false;
            }

            if (this.roles.Count != other.roles.Count)
            {
                return false;
            }

            return this.roles.All(item => other.roles.Contains(item));
        }
    }
}