namespace Fifthweek.Api.Identity.OAuth
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence;

    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> TryGetRefreshTokenAsync(string hashedTokenId);

        Task AddRefreshTokenAsync(RefreshToken refreshToken);

        Task RemoveRefreshTokens(string username, string clientId);

        Task RemoveRefreshToken(string hashedTokenId);
    }
}