namespace Dexter.Api.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Dexter.Api.Entities;

    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> TryGetRefreshTokenAsync(string username, string clientId);

        Task AddRefreshTokenAsync(RefreshToken refreshToken);

        Task<RefreshToken> TryGetRefreshTokenAsync(string hashedTokenId);

        Task RemoveRefreshTokenAsync(RefreshToken refreshToken);

        Task<List<RefreshToken>> GetAllRefreshTokensAsync();
    }
}