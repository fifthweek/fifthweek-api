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
    public class GetAllCreatorsDbStatementTests : PersistenceTestsBase
    {
        private GetAllCreatorsDbStatement target;

        [TestInitialize]
        public void Test()
        {
            this.target = new GetAllCreatorsDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task ItShouldReturnAllCreatorIdsWithSnapshots()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetAllCreatorsDbStatement(testDatabase);

                var userIds = await this.CreateDataAsync(testDatabase, 10);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync();

                Assert.AreEqual(10 + TestDatabaseSeed.CreatorChannelSnapshots, result.Count);

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
                var creatorChannelsSnapshots = new List<CreatorChannelsSnapshot>();
                var creatorChannelsSnapshotItems = new List<CreatorChannelsSnapshotItem>();

                for (int i = 0; i < creatorChannelsSnapshotCount; i++)
                {
                    creatorChannelsSnapshots.Add(new CreatorChannelsSnapshot(Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid()));
                    creatorChannelsSnapshotItems.Add(new CreatorChannelsSnapshotItem(creatorChannelsSnapshots.Last().Id, null, Guid.NewGuid(), 100));
                }

                await connection.InsertAsync(creatorChannelsSnapshots, false);
                await connection.InsertAsync(creatorChannelsSnapshotItems, false);
                return creatorChannelsSnapshots.Select(v => new UserId(v.CreatorId)).ToList();
            }
        }
    }
}