namespace Fifthweek.Api.Identity.OAuth
{
    public interface IRefreshTokenIdEncryptionService
    {
        EncryptedRefreshTokenId EncryptRefreshTokenId(RefreshTokenId refreshTokenId);

        RefreshTokenId DecryptRefreshTokenId(EncryptedRefreshTokenId encryptedRefreshTokenId);
    }
}