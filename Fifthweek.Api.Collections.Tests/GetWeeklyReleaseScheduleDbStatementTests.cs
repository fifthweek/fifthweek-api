namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetWeeklyReleaseScheduleDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly IReadOnlyList<WeeklyReleaseTime> SortedReleaseTimes = WeeklyReleaseTimeTests.GenerateSortedWeeklyReleaseTimes(QueueId.Value, 10); 
        private static readonly WeeklyReleaseSchedule WeeklyReleaseSchedule = 
            WeeklyReleaseSchedule.Parse(SortedReleaseTimes.Select(_ => HourOfWeek.Parse(_.HourOfWeek)).ToArray());
        
        private GetWeeklyReleaseScheduleDbStatement target;

        [TestMethod]
        public async Task WhenNoReleaseTimesExist_ItShouldThrowException()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetWeeklyReleaseScheduleDbStatement(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await ExpectedException.AssertExceptionAsync<Exception>(() =>
                {
                    return this.target.ExecuteAsync(QueueId);
                });

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenReleaseTimesExist_ItShouldReturnSchedule()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetWeeklyReleaseScheduleDbStatement(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var actual = await this.target.ExecuteAsync(QueueId);

                Assert.AreEqual(WeeklyReleaseSchedule, actual);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestCollectionAsync(UserId.Value, ChannelId.Value, QueueId.Value);
                await databaseContext.Database.Connection.InsertAsync(SortedReleaseTimes);
            }
        }
    }
}