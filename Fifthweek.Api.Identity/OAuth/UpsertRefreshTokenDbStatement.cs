namespace Fifthweek.Api.Identity.OAuth
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class UpsertRefreshTokenDbStatement : IUpsertRefreshTokenDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(RefreshToken refreshToken)
        {
            refreshToken.AssertNotNull("refreshToken");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.UpsertAsync(
                    refreshToken,
                    RefreshToken.Fields.Username |
                    RefreshToken.Fields.ClientId,
                    RefreshToken.Fields.HashedId |
                    RefreshToken.Fields.ProtectedTicket |
                    RefreshToken.Fields.IssuedDate |
                    RefreshToken.Fields.ExpiresDate);
            }
        }
    }
}