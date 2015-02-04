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

    [AutoConstructor]
    public partial class ReplaceWeeklyReleaseTimesDbStatement : IReplaceWeeklyReleaseTimesDbStatement
    {
        private static readonly string DeleteWeeklyReleaseTimesSql = string.Format(
            @"
            DELETE FROM {0}
            WHERE {1} = @CollectionId",
            WeeklyReleaseTime.Table,
            WeeklyReleaseTime.Fields.CollectionId);

        private readonly IFifthweekDbContext databaseContext;

        public async Task ExecuteAsync(CollectionId collectionId, WeeklyReleaseSchedule weeklyReleaseSchedule)
        {
            collectionId.AssertNotNull("collectionId");
            weeklyReleaseSchedule.AssertNotNull("weeklyReleaseSchedule");

            var newWeeklyReleaseTimes = weeklyReleaseSchedule.Value.Select(
                _ => new WeeklyReleaseTime(collectionId.Value, null, _.Value));

            var deletionParameters = new
            {
                CollectionId = collectionId.Value
            };

            // Transaction required on the following, as database must always contain at least one weekly release time per 
            // collection. The absence of weekly release times would cause a breaking inconsistency.
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await this.databaseContext.Database.Connection.ExecuteAsync(DeleteWeeklyReleaseTimesSql, deletionParameters);
                await this.databaseContext.Database.Connection.InsertAsync(newWeeklyReleaseTimes);

                transaction.Complete();
            }
        }
    }
}