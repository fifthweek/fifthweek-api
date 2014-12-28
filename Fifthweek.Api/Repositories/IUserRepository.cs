namespace Fifthweek.Api.Repositories
{
    using System;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task UpdateLastSignInDateAndAccessTokenDateAsync(string username, DateTime timestamp);

        Task UpdateLastAccessTokenDateAsync(string username, DateTime timestamp);
    }
}