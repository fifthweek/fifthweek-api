namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class TryGetRefreshTokenByEncryptedIdQueryHandler : IQueryHandler<TryGetRefreshTokenByEncryptedIdQuery, RefreshToken>
    {
        private readonly ITryGetRefreshTokenByEncryptedIdDbStatement tryGetRefreshToken;

        public Task<RefreshToken> HandleAsync(TryGetRefreshTokenByEncryptedIdQuery query)
        {
            query.AssertNotNull("query");

            return this.tryGetRefreshToken.ExecuteAsync(query.EncryptedId);
        }
    }
}