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
        private static readonly ValidSubscriptionName SubscriptionName = ValidSubscriptionName.Parse("Lawrence");
        private static readonly ValidTagline Tagline = ValidTagline.Parse("Web Comics and More");
        private static readonly ChannelPriceInUsCentsPerWeek BasePrice = ChannelPriceInUsCentsPerWeek.Parse(10);
        private static readonly CreateSubscriptionCommand Command = new CreateSubscriptionCommand(UserId, SubscriptionId, SubscriptionName, Tagline, BasePrice);
        private Mock<ISubscriptionSecurity> subscriptionSecurity;
        private CreateSubscriptionCommandHandler target;

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            this.subscriptionSecurity = new Mock<ISubscriptionSecurity>();
            this.target = new CreateSubscriptionCommandHandler(this.subscriptionSecurity.Object, this.NewDbContext());
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public async Task WhenNotAllowedToCreate_ItShouldReportAnError()
        {
            await this.InitializeDatabaseAsync();
            await this.SnapshotDatabaseAsync();

            this.subscriptionSecurity.Setup(_ => _.AssertCreationAllowedAsync(UserId)).Throws<UnauthorizedException>();

            try
            {
                await this.target.HandleAsync(Command);
                Assert.Fail("Expected recoverable exception");
            }
            catch (UnauthorizedException)
            {
            }

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenReRun_ItShouldHaveNoEffect()
        {
            await this.InitializeDatabaseAsync();
            await this.CreateUserAsync(UserId);
            await this.target.HandleAsync(Command);
            await this.SnapshotDatabaseAsync();

            await this.target.HandleAsync(Command);

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task ItShouldCreateSubscription()
        {
            await this.InitializeDatabaseAsync();
            await this.CreateUserAsync(UserId);
            await this.SnapshotDatabaseAsync();

            await this.target.HandleAsync(Command);

            var expectedSubscription = new Subscription(
                SubscriptionId.Value,
                UserId.Value,
                null,
                SubscriptionName.Value,
                Tagline.Value,
                ValidIntroduction.Default.Value,
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
            await this.InitializeDatabaseAsync();
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
            using (var databaseContext = this.NewDbContext())
            {
                await databaseContext.CreateTestUserAsync(newUserId.Value);
            }
        }
    }
}