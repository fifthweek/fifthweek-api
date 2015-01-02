namespace Fifthweek.Api.Identity
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;

    public class GetRefreshTokenQuery : IQuery<RefreshToken>
    {
        public GetRefreshTokenQuery(HashedRefreshTokenId hashedRefreshTokenId)
        {
            this.HashedRefreshTokenId = hashedRefreshTokenId;
        }

        public HashedRefreshTokenId HashedRefreshTokenId { get; private set; }
    }
}