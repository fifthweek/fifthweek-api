namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class RequesterContext : IRequesterContext
    {
        public const string ImpersonateHeaderKey = "impersonate-user";

        private readonly IRequestContext requestContext;
        private readonly IImpersonateIfRequired impersonateIfRequired;

        public async Task<Requester> GetRequesterAsync()
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

                var requester = Requester.Authenticated(userId, null, roles);
                
                var impersonatedUserId = this.TryGetImpersonatedUserId();
                if (impersonatedUserId != null)
                {
                    var impersonatedRequester = await this.impersonateIfRequired.ExecuteAsync(requester, impersonatedUserId);
                    requester = impersonatedRequester ?? requester;
                }

                return requester;
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