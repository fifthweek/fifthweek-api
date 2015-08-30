namespace Fifthweek.Api.Identity.OAuth
{
    using System;

    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class RefreshTokenIdEncryptionService : IRefreshTokenIdEncryptionService
    {
        private readonly IAesEncryptionService encryptionService;

        public EncryptedRefreshTokenId EncryptRefreshTokenId(RefreshTokenId refreshTokenId)
        {
            refreshTokenId.AssertNotNull("refreshTokenId");

            // Note we are encoding a single random block that will never be generated again, so having a random IV does not
            // improve security for us. Having a known IV does allow us to look up the refresh token by encrypted ID however.
            var encryptedBytes = this.encryptionService.Encrypt(refreshTokenId.Value.DecodeGuid().ToByteArray(), true);
            var encryptedString = Convert.ToBase64String(encryptedBytes);
            return new EncryptedRefreshTokenId(encryptedString);
        }

        public RefreshTokenId DecryptRefreshTokenId(EncryptedRefreshTokenId encryptedRefreshTokenId)
        {
            encryptedRefreshTokenId.AssertNotNull("encryptedRefreshTokenId");

            var encryptedBytes = Convert.FromBase64String(encryptedRefreshTokenId.Value);
            var decryptedBytes = this.encryptionService.Decrypt(encryptedBytes, true);
            return new RefreshTokenId(new Guid(decryptedBytes).EncodeGuid());
        }
    }
}