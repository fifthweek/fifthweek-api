namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task UpdateLastSignInDateAndAccessTokenDateAsync(ValidatedUsername username, DateTime timestamp);

        Task UpdateLastAccessTokenDateAsync(ValidatedUsername username, DateTime timestamp);
    }
}