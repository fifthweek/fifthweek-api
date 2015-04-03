namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UpdateUserTimeStampsDbStatement : IUpdateUserTimeStampsDbStatement
    {
        private static readonly string UpdateLastSignInDateAndAccessTokenDate
            = string.Format(
                @"UPDATE {0} 
                SET {1}=@Timestamp, {2}=@Timestamp 
                WHERE {3}=@UserId",
                FifthweekUser.Table,
                FifthweekUser.Fields.LastSignInDate,
                FifthweekUser.Fields.LastAccessTokenDate,
                FifthweekUser.Fields.Id);

        private static readonly string UpdateLastAccessTokenDate
            = string.Format(
                @"UPDATE {0} 
                SET {1}=@Timestamp
                WHERE {2}=@UserId",
                FifthweekUser.Table,
                FifthweekUser.Fields.LastAccessTokenDate,
                FifthweekUser.Fields.Id);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task UpdateSignInAndAccessTokenAsync(UserId userId, DateTime timestamp)
        {
            userId.AssertNotNull("userId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(
                    UpdateLastSignInDateAndAccessTokenDate,
                    new { UserId = userId.Value, TimeStamp = timestamp });
            }
        }

        public async Task UpdateAccessTokenAsync(UserId userId, DateTime timestamp)
        {
            userId.AssertNotNull("userId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                 await connection.ExecuteAsync(
                    UpdateLastAccessTokenDate,
                    new { UserId = userId.Value, TimeStamp = timestamp });
            }
        }
    }
}