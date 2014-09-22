namespace Dexter.Api.Queries
{
    using Dexter.Api.Entities;
    using Dexter.Api.Models;

    public class GetRefreshTokenQuery : IQuery<RefreshToken>
    {
        public GetRefreshTokenQuery(HashedRefreshTokenId hashedRefreshTokenId)
        {
            this.HashedRefreshTokenId = hashedRefreshTokenId;
        }

        public HashedRefreshTokenId HashedRefreshTokenId { get; private set; }
    }
}