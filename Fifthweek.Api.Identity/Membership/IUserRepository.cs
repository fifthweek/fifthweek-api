namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IUserRepository
    {
        Task UpdateLastSignInDateAndAccessTokenDateAsync(ValidUsername username, DateTime timestamp);

        Task UpdateLastAccessTokenDateAsync(ValidUsername username, DateTime timestamp);
    }
}