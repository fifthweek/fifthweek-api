namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetCollectionWeeklyReleaseTimesDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly IReadOnlyList<WeeklyReleaseTime> SortedReleaseTimes = WeeklyReleaseTimeTests.GenerateSortedWeeklyReleaseTimes(CollectionId.Value, 10); 
        private GetCollectionWeeklyReleaseTimesDbStatement target;

        [TestMethod]
        public async Task WhenNoReleaseTimesExist_ItShouldThrowException()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCollectionWeeklyReleaseTimesDbStatement(testDatabase.NewContext());
                await testDatabase.TakeSnapshotAsync();

                await ExpectedException.AssertExceptionAsync<Exception>(() =>
                {
                    return this.target.ExecuteAsync(CollectionId);
                });

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenReleaseTimesExist_ItShouldReleaseTimes()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCollectionWeeklyReleaseTimesDbStatement(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var actual = await this.target.ExecuteAsync(CollectionId);
                var actualList = actual.ToList();
                var expectedList = SortedReleaseTimes.ToList();

                CollectionAssert.AllItemsAreNotNull(actualList);
                CollectionAssert.AreEquivalent(actualList, expectedList);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenReleaseTimesExist_ItShouldReleaseTimesSorted()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCollectionWeeklyReleaseTimesDbStatement(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var actual = await this.target.ExecuteAsync(CollectionId);
                var actualList = actual.ToList();
                var expectedList = SortedReleaseTimes.ToList();

                CollectionAssert.AllItemsAreNotNull(actualList);
                CollectionAssert.AreEqual(actualList, expectedList);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                await databaseContext.CreateTestCollectionAsync(UserId.Value, ChannelId.Value, CollectionId.Value);
                await databaseContext.Database.Connection.InsertAsync(SortedReleaseTimes);
            }
        }
    }
}