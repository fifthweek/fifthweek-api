namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class TryGetRefreshTokenQueryHandler : IQueryHandler<TryGetRefreshTokenQuery, RefreshToken>
    {
        private readonly ITryGetRefreshTokenDbStatement tryGetRefreshToken;

        public Task<RefreshToken> HandleAsync(TryGetRefreshTokenQuery query)
        {
            query.AssertNotNull("query");

            return this.tryGetRefreshToken.ExecuteAsync(query.HashedRefreshTokenId);
        }
    }
}