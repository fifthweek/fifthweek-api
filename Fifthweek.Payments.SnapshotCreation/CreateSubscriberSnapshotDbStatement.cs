namespace Fifthweek.Payments.SnapshotCreation
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class CreateSubscriberSnapshotDbStatement : ICreateSubscriberSnapshotDbStatement
    {
        private static readonly string Sql = string.Format(
            @"INSERT INTO {0} 
                SELECT t.SubscriberId, t.Timestamp, u.{1}
                FROM 
                    (SELECT @Timestamp AS Timestamp, @SubscriberId AS SubscriberId) AS t
                    LEFT JOIN {2} AS u
                    ON u.{3} = t.SubscriberId",
            SubscriberSnapshot.Table,
            FifthweekUser.Fields.Email,
            FifthweekUser.Table,
            FifthweekUser.Fields.Id);

        private readonly ISnapshotTimestampCreator timestampCreator;
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(UserId subscriberId)
        {
            subscriberId.AssertNotNull("subscriberId");
            var timestamp = this.timestampCreator.Create();

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(Sql, new { Timestamp = timestamp, SubscriberId = subscriberId.Value });
            }
        }
    }
}