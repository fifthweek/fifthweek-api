namespace Fifthweek.Api.Identity.OAuth
{
    public class HashedRefreshTokenId
    {
        public HashedRefreshTokenId(string value)
        {
            this.Value = value;
        }

        public string Value { get; private set; }

        public static HashedRefreshTokenId FromRefreshToken(string refreshToken)
        {
            var hashedTokenId = Helper.GetHash(refreshToken);
            return new HashedRefreshTokenId(hashedTokenId);
        }

        public static HashedRefreshTokenId FromRefreshTokenId(RefreshTokenId refreshTokenId)
        {
            var hashedTokenId = Helper.GetHash(refreshTokenId.Value);
            return new HashedRefreshTokenId(hashedTokenId);
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}