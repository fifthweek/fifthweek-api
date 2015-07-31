namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class RequesterContext : IRequesterContext
    {
        public const string ImpersonateHeaderKey = "impersonate-user";

        private readonly IRequestContext requestContext;

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

                var userId = new UserId(userIdClaim.Value.DecodeGuid());
                var roles = identity.FindAll(ClaimTypes.Role).Select(v => v.Value).ToList();
             
                // This isn't currently used.
                var username = identity.Name;

                var impersonatedUserId = this.TryGetImpersonatedUserId();

                return Requester.Authenticated(userId, impersonatedUserId, roles);
            }

            return Requester.Unauthenticated;
        }

        private ClaimsIdentity TryGetIdentity()
        {
            var user = this.requestContext.Context.Principal;
            if (user != null)
            {
                return (ClaimsIdentity)user.Identity;
            }

            return null;
        }

        private UserId TryGetImpersonatedUserId()
        {
            IEnumerable<string> impersonateHeaderValues;
            if (this.requestContext.Request != null
                && this.requestContext.Request.Headers != null
                && this.requestContext.Request.Headers.TryGetValues(ImpersonateHeaderKey, out impersonateHeaderValues))
            {
                var userIdString = impersonateHeaderValues.FirstOrDefault();
                if (userIdString != null)
                {
                    var userId = new UserId(userIdString.DecodeGuid());
                    return userId;
                }
            }

            return null;
        }
    }
}