using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Fifthweek.Api.Core;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Persistence;
using Fifthweek.Api.Persistence.Tests.Shared;
using Fifthweek.Api.Subscriptions.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Fifthweek.Api.Subscriptions.Tests.Commands
{
    [TestClass]
    public class SetMandatorySubscriptionFieldsCommandHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());
        private static readonly SubscriptionName SubscriptionName = SubscriptionName.Parse("Lawrence");
        private static readonly Tagline Tagline = Tagline.Parse("Web Comics and More");
        private static readonly ChannelPriceInUsCentsPerWeek BasePrice = ChannelPriceInUsCentsPerWeek.Parse(10);
        private static readonly SetMandatorySubscriptionFieldsCommand Command = new SetMandatorySubscriptionFieldsCommand(UserId, SubscriptionId, SubscriptionName, Tagline, BasePrice);
        private Mock<ISubscriptionSecurity> subscriptionSecurity;
        private SetMandatorySubscriptionFieldsCommandHandler target;

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            this.subscriptionSecurity = new Mock<ISubscriptionSecurity>();
            this.target = new SetMandatorySubscriptionFieldsCommandHandler(this.NewDbContext(), this.subscriptionSecurity.Object);
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public async Task WhenNotAllowedToUpdate_ItShouldReportAnError()
        {
            this.subscriptionSecurity.Setup(_ => _.IsUpdateAllowedAsync(UserId, SubscriptionId)).ReturnsAsync(false);

            await this.SnapshotDatabaseAsync();

            try
            {
                await this.target.HandleAsync(Command);
                Assert.Fail("Expected recoverable exception");
            }
            catch (RecoverableException)
            {
            }

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenReRun_ItShouldHaveNoEffect()
        {
            this.subscriptionSecurity.Setup(_ => _.IsUpdateAllowedAsync(UserId, SubscriptionId)).ReturnsAsync(true);
            await this.CreateSubscriptionAsync(UserId, SubscriptionId);
            await this.target.HandleAsync(Command);

            await this.SnapshotDatabaseAsync();

            await this.target.HandleAsync(Command);

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task ItShouldUpdateSubscription()
        {
            this.subscriptionSecurity.Setup(_ => _.IsUpdateAllowedAsync(UserId, SubscriptionId)).ReturnsAsync(true);
            var subscription = await this.CreateSubscriptionAsync(UserId, SubscriptionId);

            await this.SnapshotDatabaseAsync();

            await this.target.HandleAsync(Command);

            subscription.Tagline = Tagline.Value;
            subscription.Name = SubscriptionName.Value;

            await this.AssertDatabaseAsync(new ExpectedSideEffects
            {
                Update = subscription, 
                ExcludedFromTest = entity => entity is Channel
            });
        }

        [TestMethod]
        public async Task ItShouldCreateTheDefaultChannel()
        {
            this.subscriptionSecurity.Setup(_ => _.IsUpdateAllowedAsync(UserId, SubscriptionId)).ReturnsAsync(true);
            await this.CreateSubscriptionAsync(UserId, SubscriptionId);

            await this.SnapshotDatabaseAsync();

            await this.target.HandleAsync(Command);

            var expectedChannel = new Channel(
                SubscriptionId.Value, // The default channel uses the subscription ID.
                SubscriptionId.Value,
                null,
                BasePrice.Value,
                DateTime.MinValue);

            await this.AssertDatabaseAsync(new ExpectedSideEffects
            {
                Insert = new WildcardEntity<Channel>(expectedChannel)
                {
                    AreEqual = actualChannel =>
                    {
                        expectedChannel.CreationDate = actualChannel.CreationDate; // Take wildcard properties from actual value.
                        return Equals(expectedChannel, actualChannel);
                    }
                },
                ExcludedFromTest = entity => entity is Subscription
            });
        }

        private async Task<Subscription> CreateSubscriptionAsync(UserId newUserId, SubscriptionId newSubscriptionId)
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

            using (var dbContext = this.NewDbContext())
            {
                // Re-fetch from database to solve clipping (e.g. DateTime).
                return await dbContext.Subscriptions.FirstAsync(_ => _.Id == subscription.Id);
            }
        }
    }
}