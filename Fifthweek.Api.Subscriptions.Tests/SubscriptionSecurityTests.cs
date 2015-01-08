using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Persistence;
using Fifthweek.Api.Persistence.Tests.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Fifthweek.Api.Subscriptions.Tests
{
    [TestClass]
    public class SubscriptionSecurityTests
    {
        [TestMethod]
        public async Task WhenAtLeastOneSubscriptionMatchesSubscriptionAndCreator_ItShouldReturnTrue()
        {
            this.WithEntities(new[]
            {
                new Subscription
                {
                    Id = this.subscriptionId.Value,
                    CreatorId = this.userId.Value
                }
            });

            var result = await this.target.CanUpdateAsync(this.userId, this.subscriptionId);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task WhenNoSubscriptionsExist_ItShouldReturnFalse()
        {
            this.WithEntities(Enumerable.Empty<Subscription>());

            var result = await this.target.CanUpdateAsync(this.userId, this.subscriptionId);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task WhenNoSubscriptionsMatchSubscriptionOrCreator_ItShouldReturnFalse()
        {
            this.WithEntities(new[]
            {
                new Subscription
                {
                    Id = new Guid(),
                    CreatorId = new Guid()
                }
            });

            var result = await this.target.CanUpdateAsync(this.userId, this.subscriptionId);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task WhenNoSubscriptionsMatchSubscription_ItShouldReturnFalse()
        {
            this.WithEntities(new[]
            {
                new Subscription
                {
                    Id = new Guid(),
                    CreatorId = this.userId.Value
                }
            });

            var result = await this.target.CanUpdateAsync(this.userId, this.subscriptionId);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task WhenNoSubscriptionsMatchCreator_ItShouldReturnFalse()
        {
            this.WithEntities(new[]
            {
                new Subscription
                {
                    Id = this.subscriptionId.Value,
                    CreatorId = new Guid()
                }
            });

            var result = await this.target.CanUpdateAsync(this.userId, this.subscriptionId);

            Assert.IsFalse(result);
        }

        [TestInitialize]
        public void Initialize()
        {
            this.subscriptions = new Mock<IDbSet<Subscription>>();
            this.fifthweekDbContext = new Mock<IFifthweekDbContext>();
            this.target = new SubscriptionSecurity(this.fifthweekDbContext.Object);
        }

        private void WithEntities(IEnumerable<Subscription> entities)
        {
            TestDbAsync.Populate(entities, this.subscriptions);
            this.fifthweekDbContext.SetupGet(_ => _.Subscriptions).Returns(this.subscriptions.Object);
        }

        private UserId userId = new UserId(Guid.NewGuid());
        private SubscriptionId subscriptionId = new SubscriptionId(Guid.NewGuid());
        private Mock<IDbSet<Subscription>> subscriptions;
        private Mock<IFifthweekDbContext> fifthweekDbContext;
        private SubscriptionSecurity target;
    }
}