namespace Fifthweek.Api.QueryHandlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Queries;
    using Fifthweek.Api.Repositories;

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