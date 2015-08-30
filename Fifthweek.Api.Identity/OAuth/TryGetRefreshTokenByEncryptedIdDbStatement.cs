namespace Fifthweek.Api.Identity.OAuth
{
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class TryGetRefreshTokenByEncryptedIdDbStatement : ITryGetRefreshTokenByEncryptedIdDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        private static readonly string Sql = string.Format(
            @"SELECT * FROM {0} WHERE {1}=@EncryptedId",
            RefreshToken.Table,
            RefreshToken.Fields.EncryptedId);

        public async Task<RefreshToken> ExecuteAsync(EncryptedRefreshTokenId encryptedId)
        {
            encryptedId.AssertNotNull("encryptedId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var results = await connection.QueryAsync<RefreshToken>(
                    Sql,
                    new { EncryptedId = encryptedId.Value });
                return results.SingleOrDefault();
            }
        }
    }
}