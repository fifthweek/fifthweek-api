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
    public class GetSubscriberChannelsSnapshotsDbStatementTests : PersistenceTestsBase
    {
        private const int Days = 10;
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;

        private static readonly UserId SubscriberId1 = UserId.Random();
        private static readonly UserId SubscriberId2 = UserId.Random();

        private GetSubscriberChannelsSnapshotsDbStatement target;

        [TestInitialize]
        public void Test()
        {
            this.target = new GetSubscriberChannelsSnapshotsDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task ItShouldReturnResultsForSubscriber1()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetSubscriberChannelsSnapshotsDbStatement(testDatabase);

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
                    Assert.AreEqual(100 + i, result[i].SubscribedChannels[0].AcceptedPrice);
                    Assert.AreEqual(100 + i, result[i].SubscribedChannels[1].AcceptedPrice);
                    Assert.IsTrue(result[i].SubscribedChannels[0].SubscriptionStartDate.Kind == DateTimeKind.Utc);
                    Assert.IsTrue(result[i].SubscribedChannels[1].SubscriptionStartDate.Kind == DateTimeKind.Utc);
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnAllResultsForSubscriber2()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetSubscriberChannelsSnapshotsDbStatement(testDatabase);

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
                    Assert.AreEqual(200 + i, result[i].SubscribedChannels[0].AcceptedPrice);
                    Assert.AreEqual(200 + i, result[i].SubscribedChannels[1].AcceptedPrice);
                    Assert.IsTrue(result[i].SubscribedChannels[0].SubscriptionStartDate.Kind == DateTimeKind.Utc);
                    Assert.IsTrue(result[i].SubscribedChannels[1].SubscriptionStartDate.Kind == DateTimeKind.Utc);
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnSubsetOfResultsForSubscriber1()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetSubscriberChannelsSnapshotsDbStatement(testDatabase);

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
                    Assert.AreEqual(100 + i + 1, result[i].SubscribedChannels[0].AcceptedPrice);
                    Assert.AreEqual(100 + i + 1, result[i].SubscribedChannels[1].AcceptedPrice);
                    Assert.IsTrue(result[i].SubscribedChannels[0].SubscriptionStartDate.Kind == DateTimeKind.Utc);
                    Assert.IsTrue(result[i].SubscribedChannels[1].SubscriptionStartDate.Kind == DateTimeKind.Utc);
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnSubsetOfResultsForSubscriber2()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetSubscriberChannelsSnapshotsDbStatement(testDatabase);

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
                    Assert.AreEqual(200 + i + 2, result[i].SubscribedChannels[0].AcceptedPrice);
                    Assert.AreEqual(200 + i + 2, result[i].SubscribedChannels[1].AcceptedPrice);
                    Assert.IsTrue(result[i].SubscribedChannels[0].SubscriptionStartDate.Kind == DateTimeKind.Utc);
                    Assert.IsTrue(result[i].SubscribedChannels[1].SubscriptionStartDate.Kind == DateTimeKind.Utc);
                }

                return ExpectedSideEffects.None;
            });
        }

        private async Task<IReadOnlyList<UserId>> CreateDataAsync(TestDatabaseContext testDatabase)
        {
            using (var connection = testDatabase.CreateConnection())
            {
                var creatorChannelsSnapshots = new List<SubscriberChannelsSnapshot>();
                var creatorChannelsSnapshotItems = new List<SubscriberChannelsSnapshotItem>();

                for (int i = 0; i < Days; i++)
                {
                    creatorChannelsSnapshots.Add(new SubscriberChannelsSnapshot(Guid.NewGuid(), Now.AddDays(i), SubscriberId1.Value));
                    creatorChannelsSnapshotItems.Add(new SubscriberChannelsSnapshotItem(creatorChannelsSnapshots.Last().Id, null, Guid.NewGuid(), 100 + i, Now));
                    creatorChannelsSnapshotItems.Add(new SubscriberChannelsSnapshotItem(creatorChannelsSnapshots.Last().Id, null, Guid.NewGuid(), 100 + i, Now));

                    creatorChannelsSnapshots.Add(new SubscriberChannelsSnapshot(Guid.NewGuid(), Now.AddDays(i).AddHours(12), SubscriberId2.Value));
                    creatorChannelsSnapshotItems.Add(new SubscriberChannelsSnapshotItem(creatorChannelsSnapshots.Last().Id, null, Guid.NewGuid(), 200 + i, Now));
                    creatorChannelsSnapshotItems.Add(new SubscriberChannelsSnapshotItem(creatorChannelsSnapshots.Last().Id, null, Guid.NewGuid(), 200 + i, Now));
                }

                await connection.InsertAsync(creatorChannelsSnapshots, false);
                await connection.InsertAsync(creatorChannelsSnapshotItems, false);
                return creatorChannelsSnapshots.Select(v => new UserId(v.SubscriberId)).ToList();
            }
        }
    }
}