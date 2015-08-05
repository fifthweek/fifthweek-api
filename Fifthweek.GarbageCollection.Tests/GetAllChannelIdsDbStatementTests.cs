namespace Fifthweek.GarbageCollection.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetAllChannelIdsDbStatementTests : PersistenceTestsBase
    {
        private static readonly IReadOnlyList<ChannelId> ChannelIds 
            = Enumerable.Repeat(0, 10).Select(v => ChannelId.Random()).ToList();

        private GetAllChannelIdsDbStatement target;

        [TestMethod]
        public async Task WhenDeletingFile_ItShouldRemoveTheRequestedPostFromTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                target = new GetAllChannelIdsDbStatement(testDatabase);
                var initialChannelIds = await this.target.ExecuteAsync();
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var finalChannelIds = await this.target.ExecuteAsync();

                Assert.AreEqual(ChannelIds.Count, finalChannelIds.Count - initialChannelIds.Count);

                CollectionAssert.AreEquivalent(
                    initialChannelIds.Concat(ChannelIds).ToList(),
                    finalChannelIds.ToList());

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestChannelsAsync(Guid.NewGuid(), Guid.NewGuid(), ChannelIds.Select(v => v.Value).ToArray());
            }
        }
    }
}