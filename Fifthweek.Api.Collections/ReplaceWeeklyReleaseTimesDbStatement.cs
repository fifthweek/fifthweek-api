namespace Fifthweek.Api.Collections
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Transactions;

    using Dapper;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class ReplaceWeeklyReleaseTimesDbStatement : IReplaceWeeklyReleaseTimesDbStatement
    {
        private static readonly string DeleteWeeklyReleaseTimesSql = string.Format(
            @"
            DELETE FROM {0}
            WHERE {1} = @CollectionId",
            WeeklyReleaseTime.Table,
            WeeklyReleaseTime.Fields.CollectionId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(CollectionId collectionId, WeeklyReleaseSchedule weeklyReleaseSchedule)
        {
            collectionId.AssertNotNull("collectionId");
            weeklyReleaseSchedule.AssertNotNull("weeklyReleaseSchedule");

            var newWeeklyReleaseTimes = weeklyReleaseSchedule.Value.Select(
                _ => new WeeklyReleaseTime(collectionId.Value, null, (byte)_.Value));

            var deletionParameters = new
            {
                CollectionId = collectionId.Value
            };

            // Transaction required on the following, as database must always contain at least one weekly release time per 
            // collection. The absence of weekly release times would cause a breaking inconsistency.
            using (var transaction = TransactionScopeBuilder.CreateAsync())
            {
                using (var connection = this.connectionFactory.CreateConnection())
                {
                    await connection.ExecuteAsync(DeleteWeeklyReleaseTimesSql, deletionParameters);
                    await connection.InsertAsync(newWeeklyReleaseTimes);
                }

                transaction.Complete();
            }
        }
    }
}