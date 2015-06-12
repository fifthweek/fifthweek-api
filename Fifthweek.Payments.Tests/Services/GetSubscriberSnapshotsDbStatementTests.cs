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
    public class GetSubscriberSnapshotsDbStatementTests : PersistenceTestsBase
    {
        private const int Days = 10;
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;

        private static readonly UserId SubscriberId1 = UserId.Random();
        private static readonly UserId SubscriberId2 = UserId.Random();

        private GetSubscriberSnapshotsDbStatement target;

        [TestInitialize]
        public void Test()
        {
            this.target = new GetSubscriberSnapshotsDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task ItShouldReturnResultsForSubscriber1()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetSubscriberSnapshotsDbStatement(testDatabase);

                var userIds = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(SubscriberId1, Now, Now.AddDays(Days));

                Assert.AreEqual(Days, result.Count);

                result = result.OrderBy(v => v.Timestamp).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    Assert.AreEqual(SubscriberId1, result[i].SubscriberId);
                    Assert.AreEqual(Now.AddDays(i), result[i].Timestamp);
                    Assert.IsTrue(result[i].Timestamp.Kind == DateTimeKind.Utc);
                    Assert.AreEqual(i + "@test.com", result[i].Email);
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnAllResultsForSubscriber2()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetSubscriberSnapshotsDbStatement(testDatabase);

                var userIds = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(SubscriberId2, Now, Now.AddDays(Days));

                Assert.AreEqual(Days, result.Count);

                result = result.OrderBy(v => v.Timestamp).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    Assert.AreEqual(SubscriberId2, result[i].SubscriberId);
                    Assert.AreEqual(Now.AddDays(i).AddHours(12), result[i].Timestamp);
                    Assert.IsTrue(result[i].Timestamp.Kind == DateTimeKind.Utc);
                    Assert.AreEqual(i + "@test2.com", result[i].Email);
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnSubsetOfResultsForSubscriber1()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetSubscriberSnapshotsDbStatement(testDatabase);

                var userIds = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                // Two fewer results at the end, and one fewer at the beginning as we select first 
                // result in front of timestamp.
                var result = await this.target.ExecuteAsync(SubscriberId1, Now.AddHours(18).AddDays(1), Now.AddDays(Days - 2));

                Assert.AreEqual(Days - 3, result.Count);

                result = result.OrderBy(v => v.Timestamp).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    Assert.AreEqual(SubscriberId1, result[i].SubscriberId);
                    Assert.AreEqual(Now.AddDays(i + 1), result[i].Timestamp);
                    Assert.IsTrue(result[i].Timestamp.Kind == DateTimeKind.Utc);
                    Assert.AreEqual((i + 1) + "@test.com", result[i].Email);
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnSubsetOfResultsForSubscriber2()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetSubscriberSnapshotsDbStatement(testDatabase);

                var userIds = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                // Three fewer results at the end, and two fewer at the beginning as we select first 
                // result in front of timestamp.
                var result = await this.target.ExecuteAsync(SubscriberId2, Now.AddHours(18).AddDays(2), Now.AddDays(Days - 3));

                Assert.AreEqual(Days - 5, result.Count);

                result = result.OrderBy(v => v.Timestamp).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    Assert.AreEqual(SubscriberId2, result[i].SubscriberId);
                    Assert.AreEqual(Now.AddDays(i + 2).AddHours(12), result[i].Timestamp);
                    Assert.IsTrue(result[i].Timestamp.Kind == DateTimeKind.Utc);
                    Assert.AreEqual((i + 2) + "@test2.com", result[i].Email);
                }

                return ExpectedSideEffects.None;
            });
        }

        private async Task<IReadOnlyList<UserId>> CreateDataAsync(TestDatabaseContext testDatabase)
        {
            using (var connection = testDatabase.CreateConnection())
            {
                var creatorChannelsSnapshots = new List<SubscriberSnapshot>();

                for (int i = 0; i < Days; i++)
                {
                    creatorChannelsSnapshots.Add(new SubscriberSnapshot(Now.AddDays(i), SubscriberId1.Value, i + "@test.com"));

                    creatorChannelsSnapshots.Add(new SubscriberSnapshot(Now.AddDays(i).AddHours(12), SubscriberId2.Value, i + "@test2.com"));
                }

                await connection.InsertAsync(creatorChannelsSnapshots, false);
                return creatorChannelsSnapshots.Select(v => new UserId(v.SubscriberId)).ToList();
            }
        }
    }
}