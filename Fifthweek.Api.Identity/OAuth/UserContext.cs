namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Web;

    using Fifthweek.Api.Identity.Membership;

    public class UserContext : IUserContext
    {
        public bool IsAuthenticated
        {
            get
            {
                var identity = this.TryGetIdentity();
                return identity != null && identity.IsAuthenticated;
            }
        }

        public string TryGetUsername()
        {
            var identity = this.TryGetIdentity();
            if (identity != null && identity.IsAuthenticated)
            {
                return identity.Name;
            }

            return null;
        }

        public UserId TryGetUserId()
        {
            var identity = this.TryGetIdentity();
            if (identity != null && identity.IsAuthenticated)
            {
                var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
                if (claim == null || claim.Value == null)
                {
                    throw new InvalidOperationException("The authenticated user does not have a UserId present in their authentication token.");
                }

                return new UserId(Guid.Parse(claim.Value));
            }

            return null;
        }

        public bool IsUserInRole(string role)
        {
            var identity = this.TryGetIdentity();

            if (identity != null && identity.IsAuthenticated)
            {
                return identity.FindAll(ClaimTypes.Role).Any(v => v.Value == role);
            }

            return false;
        }

        private ClaimsIdentity TryGetIdentity()
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                return (ClaimsIdentity)HttpContext.Current.User.Identity;
            }

            return null;
        }
    }
}