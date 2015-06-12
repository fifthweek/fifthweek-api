namespace Fifthweek.Payments.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.Services;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetAllSubscribedUsersDbStatementTests : PersistenceTestsBase
    {
        private const int Channel1OnlySubscribers = 5;
        private const int Channel2OnlySubscribers = 4;
        private const int BothChannelsSubscribers = 3;
        private static readonly ChannelId ChannelId1 = ChannelId.Random();
        private static readonly ChannelId ChannelId2 = ChannelId.Random();

        private GetAllSubscribedUsersDbStatement target;

        [TestInitialize]
        public void Test()
        {
            this.target = new GetAllSubscribedUsersDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task WhenBothChannelIdsPassedIn_ItShouldReturnSubscribersToEitherChannel()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetAllSubscribedUsersDbStatement(testDatabase);

                var userIds = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(new List<ChannelId> { ChannelId1, ChannelId2, ChannelId.Random() });

                Assert.AreEqual(Channel1OnlySubscribers + Channel2OnlySubscribers + BothChannelsSubscribers, result.Count);

                foreach (var item in userIds)
                {
                    Assert.IsTrue(result.Contains(item));
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenOneChannelIdsPassedIn_ItShouldReturnSubscribersToChannel()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetAllSubscribedUsersDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(new List<ChannelId> { ChannelId1, ChannelId.Random() });

                Assert.AreEqual(Channel1OnlySubscribers + BothChannelsSubscribers, result.Count);

                return ExpectedSideEffects.None;
            });
        }

        private async Task<IReadOnlyList<UserId>> CreateDataAsync(TestDatabaseContext testDatabase)
        {
            using (var connection = testDatabase.CreateConnection())
            {
                var subscriberChannelsSnapshots = new List<SubscriberChannelsSnapshot>();
                var subscriberChannelsSnapshotItems = new List<SubscriberChannelsSnapshotItem>();

                for (int i = 0; i < Channel1OnlySubscribers; i++)
                {
                    subscriberChannelsSnapshots.Add(new SubscriberChannelsSnapshot(Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid()));
                    subscriberChannelsSnapshotItems.Add(new SubscriberChannelsSnapshotItem(subscriberChannelsSnapshots.Last().Id, null, ChannelId1.Value, 100, DateTime.UtcNow));
                }

                for (int i = 0; i < Channel2OnlySubscribers; i++)
                {
                    subscriberChannelsSnapshots.Add(new SubscriberChannelsSnapshot(Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid()));
                    subscriberChannelsSnapshotItems.Add(new SubscriberChannelsSnapshotItem(subscriberChannelsSnapshots.Last().Id, null, ChannelId2.Value, 100, DateTime.UtcNow));
                }

                for (int i = 0; i < BothChannelsSubscribers; i++)
                {
                    subscriberChannelsSnapshots.Add(new SubscriberChannelsSnapshot(Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid()));
                    subscriberChannelsSnapshotItems.Add(new SubscriberChannelsSnapshotItem(subscriberChannelsSnapshots.Last().Id, null, ChannelId1.Value, 100, DateTime.UtcNow));
                    subscriberChannelsSnapshotItems.Add(new SubscriberChannelsSnapshotItem(subscriberChannelsSnapshots.Last().Id, null, ChannelId2.Value, 100, DateTime.UtcNow));
                }

                await connection.InsertAsync(subscriberChannelsSnapshots, false);
                await connection.InsertAsync(subscriberChannelsSnapshotItems, false);

                return subscriberChannelsSnapshots.Select(v => new UserId(v.SubscriberId)).ToList();
            }
        }
    }
}