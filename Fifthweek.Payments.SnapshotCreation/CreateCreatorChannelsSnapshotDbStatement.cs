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
    public partial class CreateCreatorChannelsSnapshotDbStatement : ICreateCreatorChannelsSnapshotDbStatement
    {
        private readonly IGuidCreator guidCreator;
        private readonly ISnapshotTimestampCreator timestampCreator;
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        private static readonly string Sql = string.Format(
            @"INSERT INTO {0} VALUES (@RecordId, @Timestamp, @CreatorId)
              INSERT INTO {1} SELECT @RecordId, {2}, {3} FROM {4} WHERE {5} IN (SELECT {6}.{7} FROM {8} INNER JOIN {6} ON {8}.{9} = {6}.{10} WHERE {8}.{9}=@CreatorId)",
            CreatorChannelsSnapshot.Table,
            CreatorChannelsSnapshotItem.Table,
            Channel.Fields.Id,
            Channel.Fields.PriceInUsCentsPerWeek,
            Channel.Table,
            Channel.Fields.BlogId,
            Blog.Table,
            Blog.Fields.Id,
            FifthweekUser.Table,
            FifthweekUser.Fields.Id,
            Blog.Fields.CreatorId);

        public async Task ExecuteAsync(UserId creatorId)
        {
            creatorId.AssertNotNull("creatorId");
            var timestamp = this.timestampCreator.Create();
            var recordId = this.guidCreator.CreateSqlSequential();

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(Sql, new { RecordId = recordId, Timestamp = timestamp, CreatorId = creatorId });
            }
        }
    }
}