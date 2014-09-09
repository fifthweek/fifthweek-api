namespace Dexter.Api.Queries
{
    using Dexter.Api.Entities;
    using Dexter.Api.Models;

    public class GetRefreshTokenQuery : IQuery<RefreshToken>
    {
        public GetRefreshTokenQuery(RefreshTokenId refreshTokenId)
        {
            this.RefreshTokenId = refreshTokenId;
        }

        public RefreshTokenId RefreshTokenId { get; private set; }
    }
}