namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Web;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;

    public class UserContext : IUserContext
    {
        public Requester GetRequester()
        {
            var identity = this.TryGetIdentity();
            if (identity != null && identity.IsAuthenticated)
            {
                var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || userIdClaim.Value == null)
                {
                    throw new InvalidOperationException("The authenticated user does not have a UserId present in their authentication token.");
                }

                var userId = new UserId(Guid.Parse(userIdClaim.Value));
                var roles = identity.FindAll(ClaimTypes.Role).Select(v => v.Value).ToList();
             
                // This isn't currently used.
                var username = identity.Name;

                return Requester.Authenticated(userId, roles);
            }

            return Requester.Unauthenticated;
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