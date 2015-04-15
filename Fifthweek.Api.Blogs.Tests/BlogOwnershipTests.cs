namespace Fifthweek.Api.Blogs.Tests
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BlogOwnershipTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private BlogOwnership target;

        [TestMethod]
        public async Task WhenCheckingBlogOwnership_ItShouldPassIfAtLeastOneBlogMatchesBlogAndCreator()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new BlogOwnership(testDatabase);
                await this.CreateBlogAsync(UserId, BlogId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, BlogId);

                Assert.IsTrue(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingBlogOwnership_ItShouldFailIfNoBlogsExist()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new BlogOwnership(testDatabase);

                using (var databaseContext = testDatabase.CreateContext())
                {
                    // We must delete ChannelSubscriptions first as there isn't a cascade delete setup
                    // due to multiple cascade branches.
                    await databaseContext.Database.Connection.ExecuteAsync("DELETE FROM ChannelSubscriptions;DELETE FROM Blogs");
                }

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, BlogId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingBlogOwnership_ItShouldFailIfNoBlogsMatchBlogOrCreator()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new BlogOwnership(testDatabase);
                await this.CreateBlogAsync(new UserId(Guid.NewGuid()), new BlogId(Guid.NewGuid()), testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, BlogId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingBlogOwnership_ItShouldFailIfNoBlogsMatchBlog()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new BlogOwnership(testDatabase);
                await this.CreateBlogAsync(UserId, new BlogId(Guid.NewGuid()), testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, BlogId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingBlogOwnership_ItShouldFailIfNoBlogsMatchCreator()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new BlogOwnership(testDatabase);
                await this.CreateBlogAsync(new UserId(Guid.NewGuid()), BlogId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, BlogId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateBlogAsync(UserId newUserId, BlogId newBlogId, TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestBlogAsync(newUserId.Value, newBlogId.Value);
            }
        }
    }
}