namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task UpdateLastSignInDateAndAccessTokenDateAsync(Username username, DateTime timestamp);

        Task UpdateLastAccessTokenDateAsync(Username username, DateTime timestamp);
    }
}