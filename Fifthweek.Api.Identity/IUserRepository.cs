namespace Fifthweek.Api.Identity
{
    using System;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task UpdateLastSignInDateAndAccessTokenDateAsync(string username, DateTime timestamp);

        Task UpdateLastAccessTokenDateAsync(string username, DateTime timestamp);
    }
}