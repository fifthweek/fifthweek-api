using System;
using System.Threading.Tasks;
using Fifthweek.Api.Core;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Persistence.Tests.Shared;
using Fifthweek.Api.Subscriptions.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Fifthweek.Api.Subscriptions.Tests.Commands
{
    [TestClass]
    public class SetMandatorySubscriptionFieldsCommandHandlerTests : PersistenceTestsBase
    {
        [TestMethod]
        public async Task WhenNotAllowedToUpdate_ItShouldHaveNoEffectAndThrowException()
        {
            await this.TakeSnapshotAsync();

            this.subscriptionSecurity.Setup(_ => _.IsUpdateAllowedAsync(UserId, SubscriptionId)).ReturnsAsync(false);

            try
            {
                await this.target.HandleAsync(Command);
                Assert.Fail("Expected recoverable exception");
            }
            catch (RecoverableException)
            {
            }

            await this.AssertNoSideEffectsAsync();
        }

        [TestMethod]
        public async Task WhenReRun_ItShouldHaveNoEffect()
        {
            await this.CreateSubscriptionAsync(UserId, SubscriptionId);

            this.subscriptionSecurity.Setup(_ => _.IsUpdateAllowedAsync(UserId, SubscriptionId)).ReturnsAsync(true);
            
            await this.target.HandleAsync(Command);
            await this.TakeSnapshotAsync();

            await this.target.HandleAsync(Command);
            await this.AssertNoSideEffectsAsync();
        }


//        [TestMethod]
//        public async Task DoesNotAffectOtherEntities()
//        {
//
//        }

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

        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());
        private static readonly SubscriptionName SubscriptionName = SubscriptionName.Parse("Lawrence");
        private static readonly Tagline Tagline = Tagline.Parse("Web Comics and More");
        private static readonly ChannelPriceInUsCentsPerWeek BasePrice = ChannelPriceInUsCentsPerWeek.Parse(10);
        private static readonly SetMandatorySubscriptionFieldsCommand Command = new SetMandatorySubscriptionFieldsCommand(UserId, SubscriptionId, SubscriptionName, Tagline, BasePrice);
        private Mock<ISubscriptionSecurity> subscriptionSecurity;
        private SetMandatorySubscriptionFieldsCommandHandler target;
    }
}