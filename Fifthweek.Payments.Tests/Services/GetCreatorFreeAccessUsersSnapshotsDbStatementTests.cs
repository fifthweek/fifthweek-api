namespace Fifthweek.Payments.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.Services;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetCreatorFreeAccessUsersSnapshotsDbStatementTests : PersistenceTestsBase
    {
        private const int Days = 10;
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;

        private static readonly UserId CreatorId1 = UserId.Random();
        private static readonly UserId CreatorId2 = UserId.Random();

        private GetCreatorFreeAccessUsersSnapshotsDbStatement target;

        [TestInitialize]
        public void Test()
        {
            this.target = new GetCreatorFreeAccessUsersSnapshotsDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task ItShouldReturnResultsForCreator1()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorFreeAccessUsersSnapshotsDbStatement(testDatabase);

                var userIds = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CreatorId1, Now, Now.AddDays(Days));

                Assert.AreEqual(Days, result.Count);

                result = result.OrderBy(v => v.Timestamp).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    Assert.AreEqual(CreatorId1, result[i].CreatorId);
                    Assert.AreEqual(Now.AddDays(i), result[i].Timestamp);
                    Assert.IsTrue(result[i].Timestamp.Kind == DateTimeKind.Utc);
                    Assert.IsTrue(result[i].FreeAccessUserEmails[0].Contains(i + "@test.com"));
                    Assert.IsTrue(result[i].FreeAccessUserEmails[1].Contains(i + "@test.com"));
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnAllResultsForCreator2()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorFreeAccessUsersSnapshotsDbStatement(testDatabase);

                var userIds = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CreatorId2, Now, Now.AddDays(Days));

                Assert.AreEqual(Days, result.Count);

                result = result.OrderBy(v => v.Timestamp).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    Assert.AreEqual(CreatorId2, result[i].CreatorId);
                    Assert.AreEqual(Now.AddDays(i).AddHours(12), result[i].Timestamp);
                    Assert.IsTrue(result[i].Timestamp.Kind == DateTimeKind.Utc);
                    Assert.IsTrue(result[i].FreeAccessUserEmails[0].Contains(i + "@test2.com"));
                    Assert.IsTrue(result[i].FreeAccessUserEmails[1].Contains(i + "@test2.com"));
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnSubsetOfResultsForCreator1()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorFreeAccessUsersSnapshotsDbStatement(testDatabase);

                var userIds = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                // Two fewer results at the end, and one fewer at the beginning as we select first 
                // result in front of timestamp.
                var result = await this.target.ExecuteAsync(CreatorId1, Now.AddHours(18).AddDays(1), Now.AddDays(Days - 2));

                Assert.AreEqual(Days - 3, result.Count);

                result = result.OrderBy(v => v.Timestamp).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    Assert.AreEqual(CreatorId1, result[i].CreatorId);
                    Assert.AreEqual(Now.AddDays(i + 1), result[i].Timestamp);
                    Assert.IsTrue(result[i].Timestamp.Kind == DateTimeKind.Utc);
                    Assert.IsTrue(result[i].FreeAccessUserEmails[0].Contains((i + 1) + "@test.com"));
                    Assert.IsTrue(result[i].FreeAccessUserEmails[1].Contains((i + 1) + "@test.com"));
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnSubsetOfResultsForCreator2()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorFreeAccessUsersSnapshotsDbStatement(testDatabase);

                var userIds = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                // Three fewer results at the end, and two fewer at the beginning as we select first 
                // result in front of timestamp.
                var result = await this.target.ExecuteAsync(CreatorId2, Now.AddHours(18).AddDays(2), Now.AddDays(Days - 3));

                Assert.AreEqual(Days - 5, result.Count);

                result = result.OrderBy(v => v.Timestamp).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    Assert.AreEqual(CreatorId2, result[i].CreatorId);
                    Assert.AreEqual(Now.AddDays(i + 2).AddHours(12), result[i].Timestamp);
                    Assert.IsTrue(result[i].Timestamp.Kind == DateTimeKind.Utc);
                    Assert.IsTrue(result[i].FreeAccessUserEmails[0].Contains((i + 2) + "@test2.com"));
                    Assert.IsTrue(result[i].FreeAccessUserEmails[1].Contains((i + 2) + "@test2.com"));
                }

                return ExpectedSideEffects.None;
            });
        }

        private async Task<IReadOnlyList<UserId>> CreateDataAsync(TestDatabaseContext testDatabase)
        {
            using (var connection = testDatabase.CreateConnection())
            {
                var creatorFreeAccessUsersSnapshots = new List<CreatorFreeAccessUsersSnapshot>();
                var creatorFreeAccessUsersSnapshotItems = new List<CreatorFreeAccessUsersSnapshotItem>();

                for (int i = 0; i < Days; i++)
                {
                    creatorFreeAccessUsersSnapshots.Add(new CreatorFreeAccessUsersSnapshot(Guid.NewGuid(), Now.AddDays(i), CreatorId1.Value));
                    creatorFreeAccessUsersSnapshotItems.Add(new CreatorFreeAccessUsersSnapshotItem(creatorFreeAccessUsersSnapshots.Last().Id, null, i + "@test.com1"));
                    creatorFreeAccessUsersSnapshotItems.Add(new CreatorFreeAccessUsersSnapshotItem(creatorFreeAccessUsersSnapshots.Last().Id, null, i + "@test.com2"));

                    creatorFreeAccessUsersSnapshots.Add(new CreatorFreeAccessUsersSnapshot(Guid.NewGuid(), Now.AddDays(i).AddHours(12), CreatorId2.Value));
                    creatorFreeAccessUsersSnapshotItems.Add(new CreatorFreeAccessUsersSnapshotItem(creatorFreeAccessUsersSnapshots.Last().Id, null, i + "@test2.com1"));
                    creatorFreeAccessUsersSnapshotItems.Add(new CreatorFreeAccessUsersSnapshotItem(creatorFreeAccessUsersSnapshots.Last().Id, null, i + "@test2.com2"));
                }

                await connection.InsertAsync(creatorFreeAccessUsersSnapshots, false);
                await connection.InsertAsync(creatorFreeAccessUsersSnapshotItems, false);
                return creatorFreeAccessUsersSnapshots.Select(v => new UserId(v.CreatorId)).ToList();
            }
        }
    }
}