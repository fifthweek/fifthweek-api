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
        public async Task WhenNotAllowedToUpdate_ItShouldNotMakeChangesAndShouldThrowException()
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