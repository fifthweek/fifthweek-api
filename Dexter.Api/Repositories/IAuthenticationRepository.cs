namespace Dexter.Api.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Dexter.Api.Entities;
    using Dexter.Api.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public interface IAuthenticationRepository
    {
        Task<IdentityResult> RegisterUser(InternalRegistrationData userModel);

        Task<IdentityUser> FindUser(string username, string password);

        Client FindClient(string clientId);

        Task<bool> AddRefreshToken(RefreshToken token);

        Task<bool> RemoveRefreshToken(string refreshTokenId);

        Task<bool> RemoveRefreshToken(RefreshToken refreshToken);

        Task<RefreshToken> FindRefreshToken(string refreshTokenId);

        List<RefreshToken> GetAllRefreshTokens();

        Task<IdentityUser> FindAsync(SignInData signInData);

        Task<IdentityResult> CreateAsync(IdentityUser user);

        Task<IdentityResult> AddUserSignInDataAsync(string userId, SignInData signInData);

        void Dispose();
    }
}