namespace Fifthweek.Api.Identity.Membership
{
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class ImpersonateIfRequired : IImpersonateIfRequired
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IGetUserRolesDbStatement getUserRoles;

        public async Task<Requester> ExecuteAsync(Requester requester, UserId requestedUserId)
        {
            requester.AssertNotNull("requester");
            requestedUserId.AssertNotNull("requestedUserId");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(requester);
            if (!authenticatedUserId.Equals(requestedUserId))
            {
                var isAdministrator = await this.requesterSecurity.IsInRoleAsync(requester, FifthweekRole.Administrator);
                if (isAdministrator)
                {
                    // We will impersonate the user to get the required information.
                    var requestedUserRoles = await this.getUserRoles.ExecuteAsync(requestedUserId);
                    return Requester.Authenticated(
                        requestedUserId,
                        requester,
                        requestedUserRoles.Roles.Select(v => v.Name));
                }

                // Throw an appropriate exception.
                await this.requesterSecurity.AuthenticateAsAsync(requester, requestedUserId);
            }

            return null;
        }
    }
}