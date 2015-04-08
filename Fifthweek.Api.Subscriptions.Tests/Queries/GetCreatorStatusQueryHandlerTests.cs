namespace Fifthweek.Api.Subscriptions.Tests.Queries
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Subscriptions.Queries;
    using Fifthweek.Api.Subscriptions.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetCreatorStatusQueryHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly ChannelId DefaultChannelId = new ChannelId(BlogId.Value);
        private static readonly GetCreatorStatusQuery Query = new GetCreatorStatusQuery(Requester, UserId);
        private GetCreatorStatusQueryHandler target;
        private Mock<IRequesterSecurity> requesterSecurity;

        [TestInitialize]
        public void TestInitialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            var connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.target = new GetCreatorStatusQueryHandler(this.requesterSecurity.Object, connectionFactory.Object);

            await this.target.HandleAsync(new GetCreatorStatusQuery(Requester.Unauthenticated, UserId));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenNotAuthenticatedAsRequiredUser_ItShouldThrowUnauthorizedException()
        {
            var connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.target = new GetCreatorStatusQueryHandler(this.requesterSecurity.Object, connectionFactory.Object);

            await this.target.HandleAsync(new GetCreatorStatusQuery(Requester, new UserId(Guid.NewGuid())));
        }

        [TestMethod]
        public async Task WhenAtLeastOneSubscriptionMatchesCreator_ItShouldReturnThatSubscriptionId()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorStatusQueryHandler(this.requesterSecurity.Object, testDatabase);
                await this.CreateSubscriptionAsync(UserId, BlogId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.HandleAsync(Query);

                Assert.AreEqual(result.BlogId, BlogId);
                Assert.IsTrue(result.MustWriteFirstPost);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCreatorHasNoPosts_ItShouldReturnTheyMustWriteFirstPost()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorStatusQueryHandler(this.requesterSecurity.Object, testDatabase);
                await this.CreateSubscriptionAsync(UserId, BlogId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.HandleAsync(Query);

                Assert.AreEqual(result.BlogId, BlogId);
                Assert.IsTrue(result.MustWriteFirstPost);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCreatorHasOnePost_ItShouldNotReturnTheyMustWriteFirstPost()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorStatusQueryHandler(this.requesterSecurity.Object, testDatabase);
                await this.CreateSubscriptionAsync(UserId, BlogId, testDatabase);
                await this.CreatePostAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.HandleAsync(Query);

                Assert.AreEqual(result.BlogId, BlogId);
                Assert.IsFalse(result.MustWriteFirstPost);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCreatorHasManyPosts_ItShouldNotReturnTheyMustWriteFirstPost()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorStatusQueryHandler(this.requesterSecurity.Object, testDatabase);
                await this.CreateSubscriptionAsync(UserId, BlogId, testDatabase);
                await this.CreatePostAsync(testDatabase);
                await this.CreatePostAsync(testDatabase);
                await this.CreatePostAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.HandleAsync(Query);

                Assert.AreEqual(result.BlogId, BlogId);
                Assert.IsFalse(result.MustWriteFirstPost);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCreatorHasManyPostsInDifferentChannels_ItShouldNotReturnTheyMustWriteFirstPost()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorStatusQueryHandler(this.requesterSecurity.Object, testDatabase);
                await this.CreateSubscriptionAsync(UserId, BlogId, testDatabase);
                await this.CreatePostAsync(testDatabase);
                await this.CreatePostAsync(testDatabase);
                await this.CreatePostAsync(testDatabase, true);
                await this.CreatePostAsync(testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.HandleAsync(Query);

                Assert.AreEqual(result.BlogId, BlogId);
                Assert.IsFalse(result.MustWriteFirstPost);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenMultipleSubscriptionsMatchCreator_ItShouldReturnTheLatestSubscriptionId()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorStatusQueryHandler(this.requesterSecurity.Object, testDatabase);
                await this.CreateSubscriptionAsync(UserId, new BlogId(Guid.NewGuid()), testDatabase, newUser: true, setTodaysDate: false);
                await this.CreateSubscriptionsAsync(UserId, 100, testDatabase);
                await this.CreateSubscriptionAsync(UserId, BlogId, testDatabase, newUser: false, setTodaysDate: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.HandleAsync(Query);

                Assert.AreEqual(result.BlogId, BlogId);
                Assert.IsTrue(result.MustWriteFirstPost);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenNoSubscriptionsExist_ItShouldReturnEmptySubscriptionId()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorStatusQueryHandler(this.requesterSecurity.Object, testDatabase);

                using (var databaseContext = testDatabase.CreateContext())
                {
                    await databaseContext.Database.Connection.ExecuteAsync("DELETE FROM Blogs");
                }

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.HandleAsync(Query);

                Assert.IsNull(result.BlogId);
                Assert.IsFalse(result.MustWriteFirstPost);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenNoSubscriptionsMatchCreator_ItShouldReturnEmptySubscriptionId()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorStatusQueryHandler(this.requesterSecurity.Object, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.HandleAsync(Query);

                Assert.IsNull(result.BlogId);
                Assert.IsFalse(result.MustWriteFirstPost);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateSubscriptionsAsync(UserId newUserId, int subscriptions, TestDatabaseContext testDatabase)
        {
            for (var i = 0; i < subscriptions; i++)
            {
                await this.CreateSubscriptionAsync(newUserId, new BlogId(Guid.NewGuid()), testDatabase, false);
            }
        }

        private async Task CreateSubscriptionAsync(UserId newUserId, BlogId newBlogId, TestDatabaseContext testDatabase, bool newUser = true, bool setTodaysDate = false)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId.Value;

            var subscription = SubscriptionTests.UniqueEntity(random);
            subscription.Id = newBlogId.Value;
            subscription.CreatorId = creator.Id;
            subscription.HeaderImageFileId = null;

            var channel = ChannelTests.UniqueEntity(random);
            channel.Id = newBlogId.Value; // Create default channel.
            channel.Blog = subscription;
            channel.BlogId = subscription.Id;

            if (newUser)
            {
                subscription.Creator = creator;
            }
            else
            {
                subscription.Creator = null; // Set by helper method.
            }

            if (setTodaysDate)
            {
                subscription.CreationDate = DateTime.UtcNow;
            }

            using (var databaseContext = testDatabase.CreateContext())
            {
                if (newUser)
                {
                    databaseContext.Users.Add(creator);
                    await databaseContext.SaveChangesAsync();
                }

                await databaseContext.Database.Connection.InsertAsync(subscription, false);
                await databaseContext.Database.Connection.InsertAsync(channel, false);
            }
        }

        private async Task CreatePostAsync(TestDatabaseContext testDatabase, bool createNewChannel = false)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var random = new Random();

                var post = PostTests.UniqueNote(random);
                post.ChannelId = DefaultChannelId.Value;

                if (createNewChannel)
                {
                    var channel = ChannelTests.UniqueEntity(random);
                    channel.BlogId = BlogId.Value;
                    await databaseContext.Database.Connection.InsertAsync(channel, false);

                    post.ChannelId = channel.Id;
                }
                
                await databaseContext.Database.Connection.InsertAsync(post, false);
            }
        }
    }
}