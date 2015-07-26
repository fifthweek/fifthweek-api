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
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CreateBlogCommandHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly ValidBlogName BlogName = ValidBlogName.Parse("Lawrence");
        private static readonly ValidTagline Tagline = ValidTagline.Parse("Web Comics and More");
        private static readonly ValidChannelPrice BasePrice = ValidChannelPrice.Parse(10);
        private static readonly CreateBlogCommand Command = new CreateBlogCommand(Requester, BlogId, BlogName, Tagline, BasePrice);
        private Mock<IBlogSecurity> blogSecurity;
        private Mock<IRequesterSecurity> requesterSecurity;
        private CreateBlogCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.blogSecurity = new Mock<IBlogSecurity>();
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            // Give side-effecting components strict mock behaviour.
            var connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.target = new CreateBlogCommandHandler(this.blogSecurity.Object, this.requesterSecurity.Object, connectionFactory.Object);

            await this.target.HandleAsync(new CreateBlogCommand(Requester.Unauthenticated, BlogId, BlogName, Tagline, BasePrice));
        }

        [TestMethod]
        public async Task WhenNotAllowedToCreate_ItShouldReportAnError()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.blogSecurity.Setup(_ => _.AssertCreationAllowedAsync(Requester)).Throws<UnauthorizedException>();
                this.target = new CreateBlogCommandHandler(this.blogSecurity.Object, this.requesterSecurity.Object, testDatabase);
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
                this.target = new CreateBlogCommandHandler(this.blogSecurity.Object, this.requesterSecurity.Object, testDatabase);
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
                this.target = new CreateBlogCommandHandler(this.blogSecurity.Object, this.requesterSecurity.Object, testDatabase);
                await this.CreateUserAsync(UserId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                var expectedBlog = new Blog(
                    BlogId.Value,
                    UserId.Value,
                    null,
                    BlogName.Value,
                    Tagline.Value,
                    ValidIntroduction.Default.Value,
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
                this.target = new CreateBlogCommandHandler(this.blogSecurity.Object, this.requesterSecurity.Object, testDatabase);
                await this.CreateUserAsync(UserId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                var expectedChannel = new Channel(
                BlogId.Value, // The default channel uses the blog ID.
                BlogId.Value,
                null,
                "Basic Subscription",
                "Exclusive News Feed" + Environment.NewLine + "Early Updates on New Releases",
                BasePrice.Value,
                true,
                DateTime.MinValue,
                DateTime.MinValue);

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

        private async Task CreateUserAsync(UserId newUserId, TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestUserAsync(newUserId.Value);
            }
        }
    }
}