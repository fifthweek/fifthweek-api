namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task UpdateLastSignInDateAndAccessTokenDateAsync(NormalizedUsername username, DateTime timestamp);

        Task UpdateLastAccessTokenDateAsync(NormalizedUsername username, DateTime timestamp);
    }
}