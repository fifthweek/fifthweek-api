using System;
using System.Threading.Tasks;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Persistence.Tests.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Subscriptions.Tests
{
    [TestClass]
    public class SubscriptionSecurityTests : PersistenceTestsBase
    {
        [TestMethod]
        public async Task WhenAtLeastOneSubscriptionMatchesSubscriptionAndCreator_ItShouldAllowUpdate()
        {
            await this.CreateSubscriptionAsync(this.userId, this.subscriptionId);
            await this.TakeSnapshotAsync();

            var result = await this.target.IsUpdateAllowedAsync(this.userId, this.subscriptionId);

            Assert.IsTrue(result);
            await this.AssertNoSideEffectsAsync();
        }

        [TestMethod]
        public async Task WhenNoSubscriptionsExist_ItShouldForbidUpdate()
        {
            await this.TakeSnapshotAsync();

            var result = await this.target.IsUpdateAllowedAsync(this.userId, this.subscriptionId);

            Assert.IsFalse(result);
            await this.AssertNoSideEffectsAsync();
        }

        [TestMethod]
        public async Task WhenNoSubscriptionsMatchSubscriptionOrCreator_ItShouldForbidUpdate()
        {
            await this.CreateSubscriptionAsync(new UserId(Guid.NewGuid()), new SubscriptionId(Guid.NewGuid()));
            await this.TakeSnapshotAsync();

            var result = await this.target.IsUpdateAllowedAsync(this.userId, this.subscriptionId);

            Assert.IsFalse(result);
            await this.AssertNoSideEffectsAsync();
        }

        [TestMethod]
        public async Task WhenNoSubscriptionsMatchSubscription_ItShouldForbidUpdate()
        {
            await this.CreateSubscriptionAsync(this.userId, new SubscriptionId(Guid.NewGuid()));
            await this.TakeSnapshotAsync();

            var result = await this.target.IsUpdateAllowedAsync(this.userId, this.subscriptionId);

            Assert.IsFalse(result);
            await this.AssertNoSideEffectsAsync();
        }

        [TestMethod]
        public async Task WhenNoSubscriptionsMatchCreator_ItShouldForbidUpdate()
        {
            await this.CreateSubscriptionAsync(new UserId(Guid.NewGuid()), this.subscriptionId);
            await this.TakeSnapshotAsync();

            var result = await this.target.IsUpdateAllowedAsync(this.userId, this.subscriptionId);

            Assert.IsFalse(result);
            await this.AssertNoSideEffectsAsync();
        }

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            this.target = new SubscriptionSecurity(this.NewDbContext());
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        private async Task CreateSubscriptionAsync(UserId newUserId, SubscriptionId newSubscriptionId)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId.Value;

            var subscription = SubscriptionTests.UniqueEntity(random);
            subscription.Id = newSubscriptionId.Value;
            subscription.Creator = creator;
            subscription.CreatorId = creator.Id;

            using (var dbContext = this.NewDbContext())
            {
                dbContext.Subscriptions.Add(subscription);
                await dbContext.SaveChangesAsync();    
            }
        }

        private readonly UserId userId = new UserId(Guid.NewGuid());
        private readonly SubscriptionId subscriptionId = new SubscriptionId(Guid.NewGuid());
        private SubscriptionSecurity target;
    }
}