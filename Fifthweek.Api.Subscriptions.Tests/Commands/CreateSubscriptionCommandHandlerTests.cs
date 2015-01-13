namespace Fifthweek.Api.Subscriptions.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Subscriptions.Commands;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CreateSubscriptionCommandHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());
        private static readonly SubscriptionName SubscriptionName = SubscriptionName.Parse("Lawrence");
        private static readonly Tagline Tagline = Tagline.Parse("Web Comics and More");
        private static readonly ChannelPriceInUsCentsPerWeek BasePrice = ChannelPriceInUsCentsPerWeek.Parse(10);
        private static readonly CreateSubscriptionCommand Command = new CreateSubscriptionCommand(UserId, SubscriptionId, SubscriptionName, Tagline, BasePrice);
        private Mock<ISubscriptionSecurity> subscriptionSecurity;
        private CreateSubscriptionCommandHandler target;

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            this.subscriptionSecurity = new Mock<ISubscriptionSecurity>();
            this.target = new CreateSubscriptionCommandHandler(this.NewDbContext(), this.subscriptionSecurity.Object);
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public async Task WhenNotAllowedToUpdate_ItShouldReportAnError()
        {
            this.subscriptionSecurity.Setup(_ => _.IsCreationAllowedAsync(UserId)).ReturnsAsync(false);

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
            this.subscriptionSecurity.Setup(_ => _.IsCreationAllowedAsync(UserId)).ReturnsAsync(true);
            await this.CreateUserAsync(UserId);
            await this.target.HandleAsync(Command);

            await this.SnapshotDatabaseAsync();

            await this.target.HandleAsync(Command);

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task ItShouldCreateSubscription()
        {
            this.subscriptionSecurity.Setup(_ => _.IsCreationAllowedAsync(UserId)).ReturnsAsync(true);
            await this.CreateUserAsync(UserId);

            await this.SnapshotDatabaseAsync();

            await this.target.HandleAsync(Command);

            var expectedSubscription = new Subscription(
                SubscriptionId.Value,
                UserId.Value,
                null,
                SubscriptionName.Value,
                Tagline.Value,
                Introduction.Default.Value,
                null,
                null,
                null,
                null,
                default(DateTime));
            
            await this.AssertDatabaseAsync(new ExpectedSideEffects
            {
                Insert = new WildcardEntity<Subscription>(expectedSubscription)
                {
                    AreEqual = actualSubscription =>
                    {
                        expectedSubscription.CreationDate = actualSubscription.CreationDate; // Take wildcard properties from actual value.
                        return Equals(expectedSubscription, actualSubscription);
                    }
                }, 
                ExcludedFromTest = entity => entity is Channel
            });
        }

        [TestMethod]
        public async Task ItShouldCreateTheDefaultChannel()
        {
            this.subscriptionSecurity.Setup(_ => _.IsCreationAllowedAsync(UserId)).ReturnsAsync(true);
            await this.CreateUserAsync(UserId);

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

        private async Task CreateUserAsync(UserId newUserId)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId.Value;

            using (var dbContext = this.NewDbContext())
            {
                dbContext.Users.Add(creator);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}