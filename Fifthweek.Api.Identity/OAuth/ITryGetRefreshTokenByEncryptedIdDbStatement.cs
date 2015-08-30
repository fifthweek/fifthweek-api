namespace Fifthweek.Api.Identity.OAuth
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence;

    public interface ITryGetRefreshTokenByEncryptedIdDbStatement
    {
        Task<RefreshToken> ExecuteAsync(EncryptedRefreshTokenId encryptedId);
    }
}