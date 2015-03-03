namespace Fifthweek.Api.Subscriptions.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Subscriptions.Commands;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CreateSubscriptionCommandHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());
        private static readonly ValidSubscriptionName SubscriptionName = ValidSubscriptionName.Parse("Lawrence");
        private static readonly ValidTagline Tagline = ValidTagline.Parse("Web Comics and More");
        private static readonly ValidChannelPriceInUsCentsPerWeek BasePrice = ValidChannelPriceInUsCentsPerWeek.Parse(10);
        private static readonly CreateSubscriptionCommand Command = new CreateSubscriptionCommand(Requester, SubscriptionId, SubscriptionName, Tagline, BasePrice);
        private Mock<ISubscriptionSecurity> subscriptionSecurity;
        private Mock<IRequesterSecurity> requesterSecurity;
        private CreateSubscriptionCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.subscriptionSecurity = new Mock<ISubscriptionSecurity>();
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            // Give side-effecting components strict mock behaviour.
            var connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.target = new CreateSubscriptionCommandHandler(this.subscriptionSecurity.Object, this.requesterSecurity.Object, connectionFactory.Object);

            await this.target.HandleAsync(new CreateSubscriptionCommand(Requester.Unauthenticated, SubscriptionId, SubscriptionName, Tagline, BasePrice));
        }

        [TestMethod]
        public async Task WhenNotAllowedToCreate_ItShouldReportAnError()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.subscriptionSecurity.Setup(_ => _.AssertCreationAllowedAsync(UserId)).Throws<UnauthorizedException>();
                this.target = new CreateSubscriptionCommandHandler(this.subscriptionSecurity.Object, this.requesterSecurity.Object, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                Func<Task> badMethodCall = () => this.target.HandleAsync(Command);

                await badMethodCall.AssertExceptionAsync<UnauthorizedException>();

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenReRun_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new CreateSubscriptionCommandHandler(this.subscriptionSecurity.Object, this.requesterSecurity.Object, testDatabase);
                await this.CreateUserAsync(UserId, testDatabase);
                await this.target.HandleAsync(Command);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldCreateSubscription()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new CreateSubscriptionCommandHandler(this.subscriptionSecurity.Object, this.requesterSecurity.Object, testDatabase);
                await this.CreateUserAsync(UserId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

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

                return new ExpectedSideEffects
                {
                    Insert = new WildcardEntity<Subscription>(expectedSubscription)
                    {
                        Expected = actualSubscription =>
                        {
                            expectedSubscription.CreationDate = actualSubscription.CreationDate; // Take wildcard properties from actual value.
                            return expectedSubscription;
                        }
                    },
                    ExcludedFromTest = entity => entity is Channel
                };
            });
        }

        [TestMethod]
        public async Task ItShouldCreateTheDefaultChannel()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new CreateSubscriptionCommandHandler(this.subscriptionSecurity.Object, this.requesterSecurity.Object, testDatabase);
                await this.CreateUserAsync(UserId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                var expectedChannel = new Channel(
                SubscriptionId.Value, // The default channel uses the subscription ID.
                SubscriptionId.Value,
                null,
                "Basic Subscription",
                "Exclusive News Feed" + Environment.NewLine + "Early Updates on New Releases",
                BasePrice.Value,
                true,
                DateTime.MinValue);

                return new ExpectedSideEffects
                {
                    Insert = new WildcardEntity<Channel>(expectedChannel)
                    {
                        Expected = actualChannel =>
                        {
                            expectedChannel.CreationDate = actualChannel.CreationDate; // Take wildcard properties from actual value.
                            return expectedChannel;
                        }
                    },
                    ExcludedFromTest = entity => entity is Subscription
                };
            });
        }

        private async Task CreateUserAsync(UserId newUserId, TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestUserAsync(newUserId.Value);
            }
        }
    }
}