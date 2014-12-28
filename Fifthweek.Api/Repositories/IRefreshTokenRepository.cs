namespace Fifthweek.Api.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Entities;

    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> TryGetRefreshTokenAsync(string hashedTokenId);

        Task AddRefreshTokenAsync(RefreshToken refreshToken);

        Task RemoveRefreshTokens(string username, string clientId);

        Task RemoveRefreshToken(string hashedTokenId);
    }
}