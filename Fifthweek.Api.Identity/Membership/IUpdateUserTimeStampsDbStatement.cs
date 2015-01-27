namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IUpdateUserTimeStampsDbStatement
    {
        Task UpdateSignInAndAccessTokenAsync(UserId userId, DateTime timestamp);

        Task UpdateAccessTokenAsync(UserId userId, DateTime timestamp);
    }
}