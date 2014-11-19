namespace Fifthweek.Api.Queries
{
    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Models;

    public class GetRefreshTokenQuery : IQuery<RefreshToken>
    {
        public GetRefreshTokenQuery(HashedRefreshTokenId hashedRefreshTokenId)
        {
            this.HashedRefreshTokenId = hashedRefreshTokenId;
        }

        public HashedRefreshTokenId HashedRefreshTokenId { get; private set; }
    }
}