namespace Fifthweek.Api.Blogs.Tests.Queries
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

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
        public async Task WhenAtLeastOneBlogMatchesCreator_ItShouldReturnThatBlogId()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorStatusQueryHandler(this.requesterSecurity.Object, testDatabase);
                await this.CreateBlogAsync(UserId, BlogId, testDatabase);
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
                await this.CreateBlogAsync(UserId, BlogId, testDatabase);
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
                await this.CreateBlogAsync(UserId, BlogId, testDatabase);
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
                await this.CreateBlogAsync(UserId, BlogId, testDatabase);
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
                await this.CreateBlogAsync(UserId, BlogId, testDatabase);
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
        public async Task WhenMultipleBlogsMatchCreator_ItShouldReturnTheLatestBlogId()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorStatusQueryHandler(this.requesterSecurity.Object, testDatabase);
                await this.CreateBlogAsync(UserId, new BlogId(Guid.NewGuid()), testDatabase, newUser: true, setTodaysDate: false);
                await this.CreateBlogsAsync(UserId, 100, testDatabase);
                await this.CreateBlogAsync(UserId, BlogId, testDatabase, newUser: false, setTodaysDate: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.HandleAsync(Query);

                Assert.AreEqual(result.BlogId, BlogId);
                Assert.IsTrue(result.MustWriteFirstPost);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenNoBlogsExist_ItShouldReturnEmptyBlogId()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorStatusQueryHandler(this.requesterSecurity.Object, testDatabase);

                using (var databaseContext = testDatabase.CreateContext())
                {
                    // We must delete ChannelSubscriptions first as there isn't a cascade delete setup
                    // due to multiple cascade branches.
                    await databaseContext.Database.Connection.ExecuteAsync("DELETE FROM Likes;DELETE FROM Comments;DELETE FROM ChannelSubscriptions;DELETE FROM Blogs");
                }

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.HandleAsync(Query);

                Assert.IsNull(result.BlogId);
                Assert.IsFalse(result.MustWriteFirstPost);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenNoBlogsMatchCreator_ItShouldReturnEmptyBlogId()
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

        private async Task CreateBlogsAsync(UserId newUserId, int blogs, TestDatabaseContext testDatabase)
        {
            for (var i = 0; i < blogs; i++)
            {
                await this.CreateBlogAsync(newUserId, new BlogId(Guid.NewGuid()), testDatabase, false);
            }
        }

        private async Task CreateBlogAsync(UserId newUserId, BlogId newBlogId, TestDatabaseContext testDatabase, bool newUser = true, bool setTodaysDate = false)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId.Value;

            var blog = BlogTests.UniqueEntity(random);
            blog.Id = newBlogId.Value;
            blog.CreatorId = creator.Id;
            blog.HeaderImageFileId = null;

            var channel = ChannelTests.UniqueEntity(random);
            channel.Id = newBlogId.Value; // Create default channel.
            channel.Blog = blog;
            channel.BlogId = blog.Id;

            if (newUser)
            {
                blog.Creator = creator;
            }
            else
            {
                blog.Creator = null; // Set by helper method.
            }

            if (setTodaysDate)
            {
                blog.CreationDate = DateTime.UtcNow;
            }

            using (var databaseContext = testDatabase.CreateContext())
            {
                if (newUser)
                {
                    databaseContext.Users.Add(creator);
                    await databaseContext.SaveChangesAsync();
                }

                await databaseContext.Database.Connection.InsertAsync(blog, false);
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