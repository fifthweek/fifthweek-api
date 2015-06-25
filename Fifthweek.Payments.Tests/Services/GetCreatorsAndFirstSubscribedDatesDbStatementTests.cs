namespace Fifthweek.Payments.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
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
    public class GetCreatorsAndFirstSubscribedDatesDbStatementTests : PersistenceTestsBase
    {
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;

        private static readonly UserId SubscriberId = UserId.Random();
        private static readonly UserId CreatorId1 = UserId.Random();
        private static readonly UserId CreatorId2 = UserId.Random();
        private static readonly ChannelId ChannelId1 = ChannelId.Random();
        private static readonly ChannelId ChannelId2 = ChannelId.Random();
        private static readonly ChannelId ChannelId3 = ChannelId.Random();

        private GetCreatorsAndFirstSubscribedDatesDbStatement target;

        [TestInitialize]
        public void Test()
        {
            this.target = new GetCreatorsAndFirstSubscribedDatesDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task WhenSubscriberHasNeverSubscribed_ItShouldReturnAllSubscriberCreators()
        {
            await this.DatabaseTestAsync(async testDatabase =>
                {
                    this.target = new GetCreatorsAndFirstSubscribedDatesDbStatement(testDatabase);

                    await testDatabase.TakeSnapshotAsync();

                    var result = await this.target.ExecuteAsync(SubscriberId);

                    Assert.AreEqual(0, result.Count);

                    return ExpectedSideEffects.None;
                });
        }

        [TestMethod]
        public async Task ItShouldReturnAllSubscriberCreatorsWithFirstSubscribedDates()
        {
            await this.DatabaseTestAsync(async testDatabase =>
                {
                    this.target = new GetCreatorsAndFirstSubscribedDatesDbStatement(testDatabase);

                    await this.CreateDataAsync(testDatabase);
                    await testDatabase.TakeSnapshotAsync();

                    var result = await this.target.ExecuteAsync(SubscriberId);

                    Assert.AreEqual(2, result.Count);

                    Assert.IsTrue(result.Contains(new CreatorIdAndFirstSubscribedDate(CreatorId1, Now.AddDays(-1))));
                    Assert.IsTrue(result.Contains(new CreatorIdAndFirstSubscribedDate(CreatorId2, Now.AddDays(1))));

                    return ExpectedSideEffects.None;
                });
        }

        private async Task CreateDataAsync(TestDatabaseContext testDatabase)
        {
            using (var connection = testDatabase.CreateConnection())
            {
                var subscriberChannelsSnapshots = new List<SubscriberChannelsSnapshot>();
                var subscriberChannelsSnapshotItems = new List<SubscriberChannelsSnapshotItem>();

                subscriberChannelsSnapshots.Add(new SubscriberChannelsSnapshot(Guid.NewGuid(), Now, SubscriberId.Value));
                subscriberChannelsSnapshotItems.Add(new SubscriberChannelsSnapshotItem(subscriberChannelsSnapshots.Last().Id, null, ChannelId1.Value, 100, Now));
                subscriberChannelsSnapshotItems.Add(new SubscriberChannelsSnapshotItem(subscriberChannelsSnapshots.Last().Id, null, ChannelId2.Value, 100, Now));

                subscriberChannelsSnapshots.Add(new SubscriberChannelsSnapshot(Guid.NewGuid(), Now.AddDays(-1), SubscriberId.Value));
                subscriberChannelsSnapshotItems.Add(new SubscriberChannelsSnapshotItem(subscriberChannelsSnapshots.Last().Id, null, ChannelId1.Value, 100, Now));

                subscriberChannelsSnapshots.Add(new SubscriberChannelsSnapshot(Guid.NewGuid(), Now.AddDays(1), SubscriberId.Value));
                subscriberChannelsSnapshotItems.Add(new SubscriberChannelsSnapshotItem(subscriberChannelsSnapshots.Last().Id, null, ChannelId3.Value, 100, DateTime.UtcNow));

                await connection.InsertAsync(subscriberChannelsSnapshots, false);
                await connection.InsertAsync(subscriberChannelsSnapshotItems, false);

                var creatorChannelsSnapshots = new List<CreatorChannelsSnapshot>();
                var creatorChannelsSnapshotItems = new List<CreatorChannelsSnapshotItem>();

                creatorChannelsSnapshots.Add(new CreatorChannelsSnapshot(Guid.NewGuid(), DateTime.UtcNow, CreatorId1.Value));
                creatorChannelsSnapshotItems.Add(new CreatorChannelsSnapshotItem(creatorChannelsSnapshots.Last().Id, null, ChannelId1.Value, 100));

                creatorChannelsSnapshots.Add(new CreatorChannelsSnapshot(Guid.NewGuid(), DateTime.UtcNow, CreatorId1.Value));
                creatorChannelsSnapshotItems.Add(new CreatorChannelsSnapshotItem(creatorChannelsSnapshots.Last().Id, null, ChannelId2.Value, 100));
                creatorChannelsSnapshots.Add(new CreatorChannelsSnapshot(Guid.NewGuid(), DateTime.UtcNow, CreatorId1.Value));
                creatorChannelsSnapshotItems.Add(new CreatorChannelsSnapshotItem(creatorChannelsSnapshots.Last().Id, null, ChannelId2.Value, 100));

                creatorChannelsSnapshots.Add(new CreatorChannelsSnapshot(Guid.NewGuid(), DateTime.UtcNow, CreatorId2.Value));
                creatorChannelsSnapshotItems.Add(new CreatorChannelsSnapshotItem(creatorChannelsSnapshots.Last().Id, null, ChannelId3.Value, 100));

                await connection.InsertAsync(creatorChannelsSnapshots, false);
                await connection.InsertAsync(creatorChannelsSnapshotItems, false);
            }
        }
    }
}