namespace Fifthweek.GarbageCollection
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class DeleteTestUserAccountsDbStatement : IDeleteTestUserAccountsDbStatement
    {
        private const string SelectIdsSql = @"(SELECT Value FROM @TestUserIds)";

        private static readonly string TestUserAccountsSql = string.Format(
            @"DECLARE @TestUserIds TABLE (Value uniqueidentifier)
            INSERT INTO @TestUserIds SELECT {2} FROM {0} WHERE {1}<@EndTimeExclusive AND {2} IN (SELECT {3} FROM {4} WHERE {5}='{6}')",
            FifthweekUser.Table,
            FifthweekUser.Fields.RegistrationDate,
            FifthweekUser.Fields.Id,
            FifthweekUserRole.Fields.UserId,
            FifthweekUserRole.Table,
            FifthweekUserRole.Fields.RoleId,
            FifthweekRole.TestUserId.ToString());

        private static readonly string UsersSql = CreateDeleteSql(
            FifthweekUser.Table,
            FifthweekUser.Fields.Id.ToString());

        private static readonly string DeleteIncommingSubscriptionsSql = string.Format(
            @"DELETE cs 
                FROM {0} cs 
                INNER JOIN {1} c ON cs.{2}=c.{3}
                INNER JOIN {4} b ON c.{5}=b.{6}
                WHERE b.{7} IN {8};",
            ChannelSubscription.Table,
            Channel.Table,
            ChannelSubscription.Fields.ChannelId,
            Channel.Fields.Id,
            Blog.Table,
            Channel.Fields.BlogId,
            Blog.Fields.Id,
            Blog.Fields.CreatorId,
            SelectIdsSql);

        private static readonly string CalculatedAccountBalancesSql = CreateDeleteSql(
            CalculatedAccountBalance.Table,
            CalculatedAccountBalance.Fields.UserId.ToString());

        private static readonly string SubscriberSnapshotSql = CreateDeleteSql(
            SubscriberSnapshot.Table,
            SubscriberSnapshot.Fields.SubscriberId.ToString());

        private static readonly string SubscriberChannelsSnapshotSql = CreateDeleteSql(
            SubscriberChannelsSnapshot.Table,
            SubscriberChannelsSnapshot.Fields.SubscriberId.ToString());

        private static readonly string CreatorFreeAccessUsersSnapshotSql = CreateDeleteSql(
            CreatorFreeAccessUsersSnapshot.Table,
            CreatorFreeAccessUsersSnapshot.Fields.CreatorId.ToString());

        private static readonly string CreatorChannelsSnapshotSql = CreateDeleteSql(
            CreatorChannelsSnapshot.Table,
            CreatorChannelsSnapshot.Fields.CreatorId.ToString());

        private static readonly string Sql = string.Concat(
            TestUserAccountsSql,
            DeleteIncommingSubscriptionsSql,
            CalculatedAccountBalancesSql,
            SubscriberSnapshotSql,
            SubscriberChannelsSnapshotSql,
            CreatorFreeAccessUsersSnapshotSql,
            CreatorChannelsSnapshotSql,
            UsersSql);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(DateTime endTimeExclusive)
        {
            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(
                    Sql,
                    new 
                    {
                        EndTimeExclusive = endTimeExclusive
                    },
                    commandTimeout: 600);
            }
        }

        private static string CreateDeleteSql(string table, string userIdColumn)
        {
            return string.Format(@"DELETE FROM {0} WHERE {1} IN {2};", table, userIdColumn, SelectIdsSql);
        }
    }
}