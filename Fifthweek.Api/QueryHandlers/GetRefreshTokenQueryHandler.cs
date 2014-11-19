namespace Fifthweek.Api.QueryHandlers
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Queries;
    using Fifthweek.Api.Repositories;

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