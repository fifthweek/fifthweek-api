namespace Fifthweek.Api.Subscriptions.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ChannelOwnershipTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private ChannelOwnership target;

        public async Task InitializeWithDatabaseAsync()
        {
            
        }

        [TestMethod]
        public async Task WhenCheckingChannelOwnership_ItShouldPassIfAtLeastOneChannelMatchesChannelAndCreator()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new ChannelOwnership(testDatabase.NewContext());
                await this.CreateChannelAsync(UserId, ChannelId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, ChannelId);

                Assert.IsTrue(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingChannelOwnership_ItShouldFailIfNoChannelsExist()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new ChannelOwnership(testDatabase.NewContext());
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, ChannelId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingChannelOwnership_ItShouldFailIfNoChannelsMatchChannelOrCreator()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new ChannelOwnership(testDatabase.NewContext());
                await this.CreateChannelAsync(new UserId(Guid.NewGuid()), new ChannelId(Guid.NewGuid()), testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, ChannelId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingChannelOwnership_ItShouldFailIfNoChannelsMatchChannel()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new ChannelOwnership(testDatabase.NewContext());
                await this.CreateChannelAsync(UserId, new ChannelId(Guid.NewGuid()), testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, ChannelId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingChannelOwnership_ItShouldFailIfNoChannelsMatchCreator()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new ChannelOwnership(testDatabase.NewContext());
                await this.CreateChannelAsync(new UserId(Guid.NewGuid()), ChannelId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, ChannelId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateChannelAsync(UserId newUserId, ChannelId newChannelId, TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                await databaseContext.CreateTestChannelAsync(newUserId.Value, newChannelId.Value);
            }
        }
    }
}