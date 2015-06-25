namespace Fifthweek.Payments.Tests.Services
{
    using System;
    using System.Collections.Generic;
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
    public class GetAllSubscribersDbStatementTests : PersistenceTestsBase
    {
        private GetAllSubscribersDbStatement target;

        [TestInitialize]
        public void Test()
        {
            this.target = new GetAllSubscribersDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task ItShouldReturnAllSubscriberIdsWithSnapshots()
        {
            await this.DatabaseTestAsync(async testDatabase =>
                {
                    this.target = new GetAllSubscribersDbStatement(testDatabase);

                    var userIds = await this.CreateDataAsync(testDatabase, 10);
                    await testDatabase.TakeSnapshotAsync();

                    var result = await this.target.ExecuteAsync();

                    Assert.AreEqual(10 + TestDatabaseSeed.SubscriberChannelSnapshots, result.Count);

                    foreach (var item in userIds)
                    {
                        Assert.IsTrue(result.Contains(item));
                    }

                    return ExpectedSideEffects.None;
                });
        }

        private async Task<IReadOnlyList<UserId>> CreateDataAsync(TestDatabaseContext testDatabase, int creatorChannelsSnapshotCount)
        {
            using (var connection = testDatabase.CreateConnection())
            {
                var creatorChannelsSnapshots = new List<SubscriberChannelsSnapshot>();
                var creatorChannelsSnapshotItems = new List<SubscriberChannelsSnapshotItem>();

                for (int i = 0; i < creatorChannelsSnapshotCount; i++)
                {
                    creatorChannelsSnapshots.Add(new SubscriberChannelsSnapshot(Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid()));
                    creatorChannelsSnapshotItems.Add(new SubscriberChannelsSnapshotItem(creatorChannelsSnapshots.Last().Id, null, Guid.NewGuid(), 100, DateTime.UtcNow));
                }

                await connection.InsertAsync(creatorChannelsSnapshots, false);
                await connection.InsertAsync(creatorChannelsSnapshotItems, false);
                return creatorChannelsSnapshots.Select(v => new UserId(v.SubscriberId)).ToList();
            }
        }
    }
}