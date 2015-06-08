namespace Fifthweek.Payments.SnapshotCreation
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class CreateCreatorFreeAccessUsersSnapshotDbStatement : ICreateCreatorFreeAccessUsersSnapshotDbStatement
    {
        private readonly IGuidCreator guidCreator;
        private readonly ISnapshotTimestampCreator timestampCreator;
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        private static readonly string Sql = string.Format(
            @"INSERT INTO {0} VALUES (@RecordId, @Timestamp, @CreatorId)
              INSERT INTO {1} SELECT @RecordId, {2} FROM {3} WHERE {4} IN (SELECT {5}.{6} FROM {7} INNER JOIN {5} ON {7}.{8} = {5}.{9} WHERE {7}.{8}=@CreatorId)",
            CreatorFreeAccessUsersSnapshot.Table,
            CreatorFreeAccessUsersSnapshotItem.Table,
            FreeAccessUser.Fields.Email,
            FreeAccessUser.Table,
            FreeAccessUser.Fields.BlogId,
            Blog.Table,
            Blog.Fields.Id,
            FifthweekUser.Table,
            FifthweekUser.Fields.Id,
            Blog.Fields.CreatorId);

        public async Task ExecuteAsync(UserId subscriberId)
        {
            subscriberId.AssertNotNull("subscriberId");
            var timestamp = this.timestampCreator.Create();
            var recordId = this.guidCreator.CreateSqlSequential();

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(Sql, new { RecordId = recordId, Timestamp = timestamp, SubscriberId = subscriberId });
            }
        }
    }
}