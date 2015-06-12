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
    public class GetCreatorChannelsSnapshotsDbStatementTests : PersistenceTestsBase
    {
        private const int Days = 10;
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;

        private static readonly UserId CreatorId1 = UserId.Random();
        private static readonly UserId CreatorId2 = UserId.Random();

        private GetCreatorChannelsSnapshotsDbStatement target;

        [TestInitialize]
        public void Test()
        {
            this.target = new GetCreatorChannelsSnapshotsDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task ItShouldReturnResultsForCreator1()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorChannelsSnapshotsDbStatement(testDatabase);

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
                    Assert.AreEqual(100 + i, result[i].CreatorChannels[0].Price);
                    Assert.AreEqual(100 + i, result[i].CreatorChannels[1].Price);
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnAllResultsForCreator2()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorChannelsSnapshotsDbStatement(testDatabase);

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
                    Assert.AreEqual(200 + i, result[i].CreatorChannels[0].Price);
                    Assert.AreEqual(200 + i, result[i].CreatorChannels[1].Price);
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnSubsetOfResultsForCreator1()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorChannelsSnapshotsDbStatement(testDatabase);

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
                    Assert.AreEqual(100 + i + 1, result[i].CreatorChannels[0].Price);
                    Assert.AreEqual(100 + i + 1, result[i].CreatorChannels[1].Price);
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnSubsetOfResultsForCreator2()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorChannelsSnapshotsDbStatement(testDatabase);

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
                    Assert.AreEqual(200 + i + 2, result[i].CreatorChannels[0].Price);
                    Assert.AreEqual(200 + i + 2, result[i].CreatorChannels[1].Price);
                }

                return ExpectedSideEffects.None;
            });
        }

        private async Task<IReadOnlyList<UserId>> CreateDataAsync(TestDatabaseContext testDatabase)
        {
            using (var connection = testDatabase.CreateConnection())
            {
                var creatorChannelsSnapshots = new List<CreatorChannelsSnapshot>();
                var creatorChannelsSnapshotItems = new List<CreatorChannelsSnapshotItem>();

                for (int i = 0; i < Days; i++)
                {
                    creatorChannelsSnapshots.Add(new CreatorChannelsSnapshot(Guid.NewGuid(), Now.AddDays(i), CreatorId1.Value));
                    creatorChannelsSnapshotItems.Add(new CreatorChannelsSnapshotItem(creatorChannelsSnapshots.Last().Id, null, Guid.NewGuid(), 100 + i));
                    creatorChannelsSnapshotItems.Add(new CreatorChannelsSnapshotItem(creatorChannelsSnapshots.Last().Id, null, Guid.NewGuid(), 100 + i));
                    
                    creatorChannelsSnapshots.Add(new CreatorChannelsSnapshot(Guid.NewGuid(), Now.AddDays(i).AddHours(12), CreatorId2.Value));
                    creatorChannelsSnapshotItems.Add(new CreatorChannelsSnapshotItem(creatorChannelsSnapshots.Last().Id, null, Guid.NewGuid(), 200 + i));
                    creatorChannelsSnapshotItems.Add(new CreatorChannelsSnapshotItem(creatorChannelsSnapshots.Last().Id, null, Guid.NewGuid(), 200 + i));
                }

                await connection.InsertAsync(creatorChannelsSnapshots, false);
                await connection.InsertAsync(creatorChannelsSnapshotItems, false);
                return creatorChannelsSnapshots.Select(v => new UserId(v.CreatorId)).ToList();
            }
        }
    }
}