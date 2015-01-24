namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PostOwnershipTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private PostOwnership target;

        [TestMethod]
        public async Task WhenCheckingPostOwnership_ItShouldPassIfAtThePostBelongsToTheUser()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new PostOwnership(testDatabase.NewContext());
                await this.CreatePostAsync(UserId, PostId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, PostId);

                Assert.IsTrue(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingPostOwnership_ItShouldFailIfNoPostsExist()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new PostOwnership(testDatabase.NewContext());

                using (var databaseContext = testDatabase.NewContext())
                {
                    await databaseContext.Database.Connection.ExecuteAsync("DELETE FROM Posts");
                }

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, PostId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingPostOwnership_ItShouldFailIfNoPostsMatchPostOrCreator()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new PostOwnership(testDatabase.NewContext());
                await this.CreatePostAsync(new UserId(Guid.NewGuid()), new PostId(Guid.NewGuid()), testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, PostId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingPostOwnership_ItShouldFailIfNoPostsMatchPost()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new PostOwnership(testDatabase.NewContext());
                await this.CreatePostAsync(UserId, new PostId(Guid.NewGuid()), testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, PostId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingPostOwnership_ItShouldFailIfNoPostsMatchCreator()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new PostOwnership(testDatabase.NewContext());
                await this.CreatePostAsync(new UserId(Guid.NewGuid()), PostId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, PostId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        private async Task CreatePostAsync(UserId newUserId, PostId newPostId, TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                await databaseContext.CreateTestPostAsync(newUserId.Value, newPostId.Value);
            }
        }
    }
}