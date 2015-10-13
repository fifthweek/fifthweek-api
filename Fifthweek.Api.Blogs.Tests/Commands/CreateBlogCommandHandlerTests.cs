namespace Fifthweek.Api.Blogs.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
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
    public class CreateBlogCommandHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly ChannelId FirstChannelId = new ChannelId(Guid.NewGuid());
        private static readonly ValidBlogName BlogName = ValidBlogName.Parse("Lawrence");
        private static readonly ValidIntroduction Introduction = ValidIntroduction.Parse("Introduction");
        private static readonly ValidChannelPrice BasePrice = ValidChannelPrice.Parse(10);
        private static readonly CreateBlogCommand Command = new CreateBlogCommand(Requester, BlogId, FirstChannelId, BlogName, BasePrice);
        private Mock<IBlogSecurity> blogSecurity;
        private Mock<IRequesterSecurity> requesterSecurity;
        private MockRequestSnapshotService requestSnapshotService;
        private CreateBlogCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.blogSecurity = new Mock<IBlogSecurity>();
            this.requestSnapshotService = new MockRequestSnapshotService();
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            // Give side-effecting components strict mock behaviour.
            var connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.target = new CreateBlogCommandHandler(this.blogSecurity.Object, this.requesterSecurity.Object, connectionFactory.Object, this.requestSnapshotService);

            await this.target.HandleAsync(new CreateBlogCommand(Requester.Unauthenticated, BlogId, FirstChannelId, BlogName, BasePrice));
        }

        [TestMethod]
        public async Task WhenNotAllowedToCreate_ItShouldReportAnError()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.blogSecurity.Setup(_ => _.AssertCreationAllowedAsync(Requester)).Throws<UnauthorizedException>();
                this.target = new CreateBlogCommandHandler(this.blogSecurity.Object, this.requesterSecurity.Object, testDatabase, this.requestSnapshotService);
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
                this.target = new CreateBlogCommandHandler(this.blogSecurity.Object, this.requesterSecurity.Object, testDatabase, this.requestSnapshotService);
                await this.CreateUserAsync(UserId, testDatabase);
                await this.target.HandleAsync(Command);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldCreateBlog()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                await this.ExecuteSuccessfully(testDatabase);

                var expectedBlog = new Blog(
                    BlogId.Value,
                    UserId.Value,
                    null,
                    BlogName.Value,
                    null,
                    null,
                    null,
                    null,
                    null,
                    default(DateTime));

                return new ExpectedSideEffects
                {
                    Insert = new WildcardEntity<Blog>(expectedBlog)
                    {
                        Expected = actualBlog =>
                        {
                            expectedBlog.CreationDate = actualBlog.CreationDate; // Take wildcard properties from actual value.
                            return expectedBlog;
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
                await this.ExecuteSuccessfully(testDatabase);

                var expectedChannel = new Channel(
                FirstChannelId.Value,
                BlogId.Value,
                null,
                BlogName.Value,
                BasePrice.Value,
                true,
                DateTime.MinValue,
                DateTime.MinValue,
                false);

                return new ExpectedSideEffects
                {
                    Insert = new WildcardEntity<Channel>(expectedChannel)
                    {
                        Expected = actualChannel =>
                        {
                            expectedChannel.CreationDate = actualChannel.CreationDate; // Take wildcard properties from actual value.
                            expectedChannel.PriceLastSetDate = actualChannel.PriceLastSetDate;
                            Assert.AreEqual(expectedChannel.CreationDate, expectedChannel.PriceLastSetDate);
                            return expectedChannel;
                        }
                    },
                    ExcludedFromTest = entity => entity is Blog
                };
            });
        }

        [TestMethod]
        public async Task ItShouldRequestSnapshot()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var trackingDatabase = new TrackingConnectionFactory(testDatabase);
                await this.InitializeTarget(trackingDatabase);
                await this.CreateUserAsync(UserId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                this.requestSnapshotService.VerifyConnectionDisposed(trackingDatabase);
                
                await this.target.HandleAsync(Command);

                this.requestSnapshotService.VerifyCalledWith(UserId, SnapshotType.CreatorChannels);

                return new ExpectedSideEffects
                {
                    ExcludedFromTest = entity => entity is Channel || entity is Blog
                };
            });
        }

        [TestMethod]
        public async Task ItShouldAbortUpdateIfSnapshotFails()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                await this.ExecuteSuccessfully(testDatabase);

                this.requestSnapshotService.ThrowException();

                await ExpectedException.AssertExceptionAsync<SnapshotException>(
                    () => this.target.HandleAsync(Command));

                return ExpectedSideEffects.TransactionAborted;
            });
        }

        private async Task ExecuteSuccessfully(TestDatabaseContext testDatabase)
        {
            await this.InitializeTarget(testDatabase);

            await this.CreateUserAsync(UserId, testDatabase);
            await testDatabase.TakeSnapshotAsync();
            await this.target.HandleAsync(Command);
        }

        private async Task InitializeTarget(IFifthweekDbConnectionFactory testDatabase)
        {
            this.target = new CreateBlogCommandHandler(
                this.blogSecurity.Object,
                this.requesterSecurity.Object,
                testDatabase,
                this.requestSnapshotService);
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