namespace Fifthweek.Api.Payments
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Payments;

    public static class Extensions
    {
        public static async Task<UserType> GetUserTypeAsync(this IRequesterSecurity requesterSecurity, Requester requester)
        {
            var isTestUser = await requesterSecurity.IsInRoleAsync(requester, FifthweekRole.TestUser);
            var stripeMode = isTestUser ? UserType.TestUser : UserType.StandardUser;
            return stripeMode;
        }
    }
}