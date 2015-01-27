namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;

    public class RequesterSecurity : IRequesterSecurity
    {
        public Task<Shared.Membership.UserId> AuthenticateAsync(Requester requester)
        {
            requester.AssertNotNull("requester");

            if (requester.UserId == null)
            {
                throw new UnauthorizedException();
            }

            return Task.FromResult(requester.UserId);
        }

        public Task<Shared.Membership.UserId> AuthenticateAsAsync(Requester requester, Shared.Membership.UserId userId)
        {
            requester.AssertNotNull("requester");
            userId.AssertNotNull("userId");

            // This handles requester being Unauthorized as requester.UserId will be null.
            if (!userId.Equals(requester.UserId))
            {
                throw new UnauthorizedException();
            }

            return Task.FromResult(requester.UserId);
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

        public Task AssertInRoleAsync(Requester requester, string role)
        {
            return this.AssertTrueAsync(this.IsInRoleAsync(requester, role));
        }

        public Task AssertInAnyRoleAsync(Requester requester, params string[] roles)
        {
            return this.AssertTrueAsync(this.IsInAnyRoleAsync(requester, roles));
        }

        public Task AssertInAnyRoleAsync(Requester requester, IEnumerable<string> roles)
        {
            return this.AssertTrueAsync(this.IsInAnyRoleAsync(requester, roles));
        }

        public Task AssertInAllRolesAsync(Requester requester, params string[] roles)
        {
            return this.AssertTrueAsync(this.IsInAllRolesAsync(requester, roles));
        }

        public Task AssertInAllRolesAsync(Requester requester, IEnumerable<string> roles)
        {
            return this.AssertTrueAsync(this.IsInAllRolesAsync(requester, roles));
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

        private async Task AssertTrueAsync(Task<bool> task)
        {
            if (!await task)
            {
                throw new UnauthorizedException();
            }
        }
    }
}