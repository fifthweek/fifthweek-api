namespace Dexter.Api.QueryHandlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Dexter.Api.Entities;
    using Dexter.Api.Queries;
    using Dexter.Api.Repositories;

    public class GetAllRefreshTokensQueryHandler : IQueryHandler<GetAllRefreshTokensQuery, List<RefreshToken>>
    {
        private readonly IRefreshTokenRepository refreshTokenRepository;

        public GetAllRefreshTokensQueryHandler(IRefreshTokenRepository refreshTokenRepository)
        {
            this.refreshTokenRepository = refreshTokenRepository;
        }

        public Task<List<RefreshToken>> HandleAsync(GetAllRefreshTokensQuery query)
        {
            return this.refreshTokenRepository.GetAllRefreshTokensAsync();
        }
    }
}