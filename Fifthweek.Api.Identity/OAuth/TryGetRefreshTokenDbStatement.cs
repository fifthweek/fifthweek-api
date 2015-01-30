namespace Fifthweek.Api.Identity.OAuth
{
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class TryGetRefreshTokenDbStatement : ITryGetRefreshTokenDbStatement
    {
        private readonly IFifthweekDbContext fifthweekDbContext;

        public async Task<RefreshToken> ExecuteAsync(HashedRefreshTokenId hashedTokenId)
        {
            hashedTokenId.AssertNotNull("hashedTokenId");

            var results = await this.fifthweekDbContext.Database.Connection.QueryAsync<RefreshToken>(
                string.Format(@"SELECT * FROM {0} WHERE {1}=@{1}", RefreshToken.Table, RefreshToken.Fields.HashedId),
                new { HashedId = hashedTokenId.Value });
            return results.SingleOrDefault();
        }
    }
}