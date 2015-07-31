namespace Fifthweek.Api.Identity.Shared.Membership
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Shared;

    public class Requester
    {
        public static readonly Requester Unauthenticated = new Requester();

        private readonly UserId userId;

        private readonly HashSet<string> roles;

        private readonly Requester impersonatingRequester;

        private Requester(UserId userId, Requester impersonatingRequester, IEnumerable<string> roles)
        {
            userId.AssertNotNull("userId");

            this.userId = userId;
            this.impersonatingRequester = impersonatingRequester;
            this.roles = new HashSet<string>(roles ?? Enumerable.Empty<string>());
        }

        private Requester()
        {
            this.roles = new HashSet<string>();
        }

        internal UserId UserId
        {
            get
            {
                return this.userId;
            }
        }

        internal Requester ImpersonatingRequester
        {
            get
            {
                return this.impersonatingRequester;
            }
        }

        internal IEnumerable<string> Roles 
        {
            get
            {
                return this.roles.ToList();
            }
        }

        public static Requester Authenticated(UserId userId, params string[] roles)
        {
            return Authenticated(userId, null, (IEnumerable<string>)roles);
        }

        public static Requester Authenticated(UserId userId, IEnumerable<string> roles)
        {
            return Authenticated(userId, null, roles);
        }

        public static Requester Authenticated(UserId userId, Requester impersonatingRequester, params string[] roles)
        {
            return Authenticated(userId, impersonatingRequester, (IEnumerable<string>)roles);
        }

        public static Requester Authenticated(UserId userId, Requester impersonatingRequester, IEnumerable<string> roles)
        {
            return new Requester(userId, impersonatingRequester, roles);
        }

        public override string ToString()
        {
            return this.userId == null 
                ? "Requester(Unauthenticated)"
                : string.Format("Requester({0}, {1}, [{2}])", this.userId, this.impersonatingRequester, string.Join(", ", this.roles));
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
                hashCode = (hashCode * 397) ^ (this.impersonatingRequester != null ? this.impersonatingRequester.GetHashCode() : 0);
                
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

            if (!object.Equals(this.impersonatingRequester, other.impersonatingRequester))
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