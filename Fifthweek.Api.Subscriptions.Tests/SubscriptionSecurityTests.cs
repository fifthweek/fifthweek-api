using System;
using System.Threading.Tasks;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Persistence;
using Fifthweek.Api.Persistence.Tests.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Subscriptions.Tests
{
    [TestClass]
    public class SubscriptionSecurityTests
    {
        [TestMethod]
        public async Task WhenAtLeastOneSubscriptionMatchesSubscriptionAndCreator_ItShouldAllowUpdate()
        {
            await this.CreateSubscriptionAsync(this.userId, this.subscriptionId);

            var result = await this.target.IsUpdateAllowedAsync(this.userId, this.subscriptionId);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task WhenNoSubscriptionsExist_ItShouldForbidUpdate()
        {
            var result = await this.target.IsUpdateAllowedAsync(this.userId, this.subscriptionId);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task WhenNoSubscriptionsMatchSubscriptionOrCreator_ItShouldForbidUpdate()
        {
            await this.CreateSubscriptionAsync(new UserId(Guid.NewGuid()), new SubscriptionId(Guid.NewGuid()));

            var result = await this.target.IsUpdateAllowedAsync(this.userId, this.subscriptionId);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task WhenNoSubscriptionsMatchSubscription_ItShouldForbidUpdate()
        {
            await this.CreateSubscriptionAsync(this.userId, new SubscriptionId(Guid.NewGuid()));

            var result = await this.target.IsUpdateAllowedAsync(this.userId, this.subscriptionId);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task WhenNoSubscriptionsMatchCreator_ItShouldForbidUpdate()
        {
            await this.CreateSubscriptionAsync(new UserId(Guid.NewGuid()), this.subscriptionId);

            var result = await this.target.IsUpdateAllowedAsync(this.userId, this.subscriptionId);

            Assert.IsFalse(result);
        }

        [TestInitialize]
        public void Initialize()
        {
            this.temporaryDatabase = TemporaryDatabase.CreateNew();
            this.fifthweekDbContext = this.temporaryDatabase.NewDbContext();
            this.target = new SubscriptionSecurity(this.fifthweekDbContext);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.temporaryDatabase.Dispose();
        }

        private Task CreateSubscriptionAsync(UserId newUserId, SubscriptionId newSubscriptionId)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId.Value;

            var subscription = SubscriptionTests.UniqueEntity(random);
            subscription.Id = newSubscriptionId.Value;
            subscription.Creator = creator;
            subscription.CreatorId = creator.Id;

            this.fifthweekDbContext.Subscriptions.Add(subscription);
            return this.fifthweekDbContext.SaveChangesAsync();
        }

        private TemporaryDatabase temporaryDatabase;
        private readonly UserId userId = new UserId(Guid.NewGuid());
        private readonly SubscriptionId subscriptionId = new SubscriptionId(Guid.NewGuid());
        private IFifthweekDbContext fifthweekDbContext;
        private SubscriptionSecurity target;
    }
}