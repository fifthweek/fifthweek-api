﻿namespace Fifthweek.Api.Identity.OAuth
{
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class TryGetRefreshTokenDbStatement : ITryGetRefreshTokenDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        private static readonly string Sql = string.Format(
            @"SELECT * FROM {0} WHERE {1}=@ClientId AND {2}=@Username",
            RefreshToken.Table,
            RefreshToken.Fields.ClientId,
            RefreshToken.Fields.Username);

        public async Task<RefreshToken> ExecuteAsync(ClientId clientId, Username username)
        {
            clientId.AssertNotNull("clientId");
            username.AssertNotNull("username");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var results = await connection.QueryAsync<RefreshToken>(
                    Sql,
                    new { ClientId = clientId.Value, Username = username.Value });
                return results.SingleOrDefault();
            }
        }
    }
}