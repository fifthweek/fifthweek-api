namespace Fifthweek.Api.Identity.OAuth
{
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class TryGetRefreshTokenDbStatement : ITryGetRefreshTokenDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<RefreshToken> ExecuteAsync(HashedRefreshTokenId hashedTokenId)
        {
            hashedTokenId.AssertNotNull("hashedTokenId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var results = await connection.QueryAsync<RefreshToken>(
                    string.Format(@"SELECT * FROM {0} WHERE {1}=@{1}", RefreshToken.Table, RefreshToken.Fields.HashedId),
                    new { HashedId = hashedTokenId.Value });
                return results.SingleOrDefault();
            }
        }
    }
}