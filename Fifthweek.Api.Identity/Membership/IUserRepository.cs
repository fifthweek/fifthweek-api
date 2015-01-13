namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task UpdateLastSignInDateAndAccessTokenDateAsync(ValidUsername username, DateTime timestamp);

        Task UpdateLastAccessTokenDateAsync(ValidUsername username, DateTime timestamp);
    }
}