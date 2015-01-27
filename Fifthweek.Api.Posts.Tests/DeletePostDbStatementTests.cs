namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ChannelId = Fifthweek.Api.Channels.Shared.ChannelId;
    using FileId = Fifthweek.Api.FileManagement.Shared.FileId;
    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    [TestClass]
    public class DeletePostDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());

        private DeletePostDbStatement target;

        [TestMethod]
        public async Task WhenDeletingPost_ItShouldRemoveTheRequestedPostFromTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                target = new DeletePostDbStatement(testDatabase.NewContext());
                var post = await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(PostId);

                return new ExpectedSideEffects
                {
                    Delete = post,
                };
            });
        }

        [TestMethod]
        public async Task WhenDeletingNonExistantPost_ItShouldNotModifyTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                target = new DeletePostDbStatement(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(new PostId(Guid.NewGuid()));

                return ExpectedSideEffects.None;
            });
        }

        private async Task<Post> CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                await databaseContext.CreateTestCollectionAsync(UserId.Value, ChannelId.Value, CollectionId.Value);
                await databaseContext.CreateTestFileWithExistingUserAsync(UserId.Value, FileId.Value);

                var post = PostTests.UniqueFileOrImage(new Random());
                post.Id = PostId.Value;
                post.ChannelId = ChannelId.Value;
                post.CollectionId = CollectionId.Value;
                post.FileId = FileId.Value;
                post.CreationDate = new SqlDateTime(post.CreationDate).Value;
                post.LiveDate = new SqlDateTime(post.LiveDate).Value;
                await databaseContext.Database.Connection.InsertAsync(post);

                return post;
            }
        }
    }
}