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
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private ChannelOwnership target;

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            this.target = new ChannelOwnership(this.NewDbContext());
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public async Task WhenCheckingChannelOwnership_ItShouldPassIfAtLeastOneChannelMatchesChannelAndCreator()
        {
            await this.CreateChannelAsync(UserId, ChannelId);
            await this.SnapshotDatabaseAsync();

            var result = await this.target.IsOwnerAsync(UserId, ChannelId);

            Assert.IsTrue(result);
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenCheckingChannelOwnership_ItShouldFailIfNoChannelsExist()
        {
            await this.SnapshotDatabaseAsync();

            var result = await this.target.IsOwnerAsync(UserId, ChannelId);

            Assert.IsFalse(result);
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenCheckingChannelOwnership_ItShouldFailIfNoChannelsMatchChannelOrCreator()
        {
            await this.CreateChannelAsync(new UserId(Guid.NewGuid()), new ChannelId(Guid.NewGuid()));
            await this.SnapshotDatabaseAsync();

            var result = await this.target.IsOwnerAsync(UserId, ChannelId);

            Assert.IsFalse(result);
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenCheckingChannelOwnership_ItShouldFailIfNoChannelsMatchChannel()
        {
            await this.CreateChannelAsync(UserId, new ChannelId(Guid.NewGuid()));
            await this.SnapshotDatabaseAsync();

            var result = await this.target.IsOwnerAsync(UserId, ChannelId);

            Assert.IsFalse(result);
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenCheckingChannelOwnership_ItShouldFailIfNoChannelsMatchCreator()
        {
            await this.CreateChannelAsync(new UserId(Guid.NewGuid()), ChannelId);
            await this.SnapshotDatabaseAsync();

            var result = await this.target.IsOwnerAsync(UserId, ChannelId);

            Assert.IsFalse(result);
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        private async Task CreateChannelAsync(UserId newUserId, ChannelId newChannelId)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId.Value;

            var subscription = SubscriptionTests.UniqueEntity(random);
            subscription.Creator = creator;
            subscription.CreatorId = creator.Id;

            var channel = ChannelTests.UniqueEntity(random);
            channel.Id = newChannelId.Value;
            channel.Subscription = subscription;
            channel.SubscriptionId = subscription.Id;

            using (var dbContext = this.NewDbContext())
            {
                dbContext.Channels.Add(channel);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}