namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.ServiceModel.Channels;
    using System.Web;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class RequesterContext : IRequesterContext
    {
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

                return Requester.Authenticated(userId, roles);
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
    }
}