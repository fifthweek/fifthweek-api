namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    public class RequesterSecurity : IRequesterSecurity
    {
        public async Task<UserId> AuthenticateAsync(Requester requester)
        {
            requester.AssertNotNull("requester");

            if (requester.UserId == null)
            {
                throw new UnauthenticatedException();
            }
            
            return requester.UserId;
        }

        public async Task<UserId> AuthenticateAsAsync(Requester requester, UserId userId)
        {
            requester.AssertNotNull("requester");
            userId.AssertNotNull("userId");

            var authenticatedUserId = await this.AuthenticateAsync(requester);

            if (!userId.Equals(authenticatedUserId))
            {
                throw new UnauthorizedException("User '{0}' is could not be authenticated as '{1}'.", requester.UserId, userId);
            }

            return authenticatedUserId;
        }

        public Task<bool> IsInRoleAsync(Requester requester, string role)
        {
            requester.AssertNotNull("requester");
            role.AssertNotNull("role");
            return Task.FromResult(requester.IsInRole(role));
        }

        public Task<bool> IsInAnyRoleAsync(Requester requester, params string[] roles)
        {
            return this.IsInAnyRoleAsync(requester, (IEnumerable<string>)roles);
        }

        public Task<bool> IsInAnyRoleAsync(Requester requester, IEnumerable<string> roles)
        {
            requester.AssertNotNull("requester");
            roles.AssertNotNull("roles");
            roles = this.CheckRoles(roles);
            return Task.FromResult(roles.Where(v => v != null).Any(requester.IsInRole));
        }

        public Task<bool> IsInAllRolesAsync(Requester requester, params string[] roles)
        {
            return this.IsInAllRolesAsync(requester, (IEnumerable<string>)roles);
        }

        public Task<bool> IsInAllRolesAsync(Requester requester, IEnumerable<string> roles)
        {
            requester.AssertNotNull("requester");
            roles.AssertNotNull("roles");
            roles = this.CheckRoles(roles);
            return Task.FromResult(roles.Where(v => v != null).All(requester.IsInRole));
        }

        public async Task AssertInRoleAsync(Requester requester, string role)
        {
            await this.AuthenticateAsync(requester);
            if (!await this.IsInRoleAsync(requester, role))
            {
                throw new UnauthorizedException("User '{0}' is not in role '{1}'.", requester.UserId, role);
            }
        }

        public Task AssertInAnyRoleAsync(Requester requester, params string[] roles)
        {
            return this.AssertInAnyRoleAsync(requester, (IEnumerable<string>)roles);
        }

        public async Task AssertInAnyRoleAsync(Requester requester, IEnumerable<string> roles)
        {
            await this.AuthenticateAsync(requester);
            if (!await this.IsInAnyRoleAsync(requester, roles))
            {
                throw new UnauthorizedException("User '{0}' is not in any role from '{1}'.", requester.UserId, string.Join(",", roles));
            }
        }

        public Task AssertInAllRolesAsync(Requester requester, params string[] roles)
        {
            return this.AssertInAllRolesAsync(requester, (IEnumerable<string>)roles);
        }

        public async Task AssertInAllRolesAsync(Requester requester, IEnumerable<string> roles)
        {
            await this.AuthenticateAsync(requester);
            if (!await this.IsInAllRolesAsync(requester, roles))
            {
                throw new UnauthorizedException("User '{0}' is not in all roles from '{1}'.", requester.UserId, string.Join(",", roles));
            }
        }

        private IEnumerable<string> CheckRoles(IEnumerable<string> roles)
        {
            var result = roles.Where(v => v != null).ToList();
            if (result.Count == 0)
            {
                throw new ArgumentException("An empty set of roles is not valid for this operation as the result is ambiguous.", "roles");
            }

            return result;
        }
    }
}