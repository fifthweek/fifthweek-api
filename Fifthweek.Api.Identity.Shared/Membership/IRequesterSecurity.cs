namespace Fifthweek.Api.Identity.Shared.Membership
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRequesterSecurity
    {
        Task<Shared.Membership.UserId> AuthenticateAsync(Requester requester);

        Task<Shared.Membership.UserId> AuthenticateAsAsync(Requester requester, Shared.Membership.UserId userId);

        Task<bool> IsInRoleAsync(Requester requester, string role);

        Task<bool> IsInAnyRoleAsync(Requester requester, params string[] roles);

        Task<bool> IsInAnyRoleAsync(Requester requester, IEnumerable<string> roles);

        Task<bool> IsInAllRolesAsync(Requester requester, params string[] roles);

        Task<bool> IsInAllRolesAsync(Requester requester, IEnumerable<string> roles);

        Task AssertInRoleAsync(Requester requester, string role);

        Task AssertInAnyRoleAsync(Requester requester, params string[] roles);

        Task AssertInAnyRoleAsync(Requester requester, IEnumerable<string> roles);

        Task AssertInAllRolesAsync(Requester requester, params string[] roles);

        Task AssertInAllRolesAsync(Requester requester, IEnumerable<string> roles);
    }
}