namespace Fifthweek.Api.Identity
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;

    public class GetRefreshTokenQueryHandler : IQueryHandler<GetRefreshTokenQuery, RefreshToken>
    {
        private readonly IRefreshTokenRepository refreshTokenRepository;

        public GetRefreshTokenQueryHandler(IRefreshTokenRepository refreshTokenRepository)
        {
            this.refreshTokenRepository = refreshTokenRepository;
        }

        public Task<RefreshToken> HandleAsync(GetRefreshTokenQuery query)
        {
            return this.refreshTokenRepository.TryGetRefreshTokenAsync(query.HashedRefreshTokenId.Value);
        }
    }
}