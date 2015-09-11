namespace Fifthweek.Api.Channels.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Commands;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Payments.Tests.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CreateChannelCommandHandlerTests : PersistenceTestsBase
    {
        private const bool IsVisibleToNonSubscribers = true;
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly ValidChannelName Name = ValidChannelName.Parse("Bat puns");
        private static readonly ValidChannelPrice Price = ValidChannelPrice.Parse(10);
        private static readonly CreateChannelCommand Command = new CreateChannelCommand(Requester, ChannelId, BlogId, Name, Price, IsVisibleToNonSubscribers);

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IBlogSecurity> subscriptionSecurity;
        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private MockRequestSnapshotService requestSnapshot;
        private CreateChannelCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
            this.subscriptionSecurity = new Mock<IBlogSecurity>();
            this.requestSnapshot = new MockRequestSnapshotService();

            // Give potentially side-effective components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new CreateChannelCommandHandler(this.requesterSecurity.Object, this.subscriptionSecurity.Object, connectionFactory, this.requestSnapshot);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireCommand()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldRequireUserIsAuthenticated()
        {
            await this.target.HandleAsync(new CreateChannelCommand(Requester.Unauthenticated, ChannelId, BlogId, Name, Price, IsVisibleToNonSubscribers));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldRequireUserHasWriteAccessToSubscription()
        {
            this.subscriptionSecurity.Setup(_ => _.AssertWriteAllowedAsync(UserId, BlogId)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(Command);
        }

        [TestMethod]
        public async Task ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await this.target.HandleAsync(Command);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldCreateChannel()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                var expectedChannel = new Channel(
                    ChannelId.Value,
                    BlogId.Value,
                    null,
                    Name.Value,
                    Price.Value,
                    IsVisibleToNonSubscribers,
                    default(DateTime),
                    default(DateTime));

                return new ExpectedSideEffects
                {
                    Insert = new WildcardEntity<Channel>(expectedChannel)
                    {
                        Expected = actual =>
                        {
                            expectedChannel.CreationDate = actual.CreationDate;
                            expectedChannel.PriceLastSetDate = actual.PriceLastSetDate;
                            Assert.AreEqual(actual.CreationDate, actual.PriceLastSetDate);
                            return expectedChannel;
                        }
                    }
                };
            });
        }

        [TestMethod]
        public async Task ItShouldRequestSnapshotAfterUpdate()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var trackingDatabase = new TrackingConnectionFactory(testDatabase);
                this.InitializeTarget(trackingDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                this.requestSnapshot.VerifyConnectionDisposed(trackingDatabase);

                await this.target.HandleAsync(Command);

                this.requestSnapshot.VerifyCalledWith(UserId, SnapshotType.CreatorChannels);

                var expectedChannel = new Channel(
                    ChannelId.Value,
                    BlogId.Value,
                    null,
                    Name.Value,
                    Price.Value,
                    IsVisibleToNonSubscribers,
                    default(DateTime),
                    default(DateTime));

                return new ExpectedSideEffects
                {
                    Insert = new WildcardEntity<Channel>(expectedChannel)
                    {
                        Expected = actual =>
                        {
                            expectedChannel.CreationDate = actual.CreationDate;
                            expectedChannel.PriceLastSetDate = actual.PriceLastSetDate;
                            Assert.AreEqual(actual.CreationDate, actual.PriceLastSetDate);
                            return expectedChannel;
                        }
                    }
                };
            });
        }

        [TestMethod]
        public async Task ItShouldAbortUpdateIfSnapshotFails()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                this.requestSnapshot.ThrowException();

                await ExpectedException.AssertExceptionAsync<SnapshotException>(
                    () => this.target.HandleAsync(Command));

                return ExpectedSideEffects.TransactionAborted;
            });
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestBlogAsync(UserId.Value, BlogId.Value);
            }
        }
    } 
}