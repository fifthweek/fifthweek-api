namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DeletePostDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly CommentId CommentId = CommentId.Random();

        private DeletePostDbStatement target;

        [TestMethod]
        public async Task WhenDeletingPost_ItShouldRemoveTheRequestedPostFromTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                target = new DeletePostDbStatement(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                var expectedDeletions = await this.GetExpectedDeletionsAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(PostId);

                return new ExpectedSideEffects
                {
                    Deletes = expectedDeletions,
                };
            });
        }

        [TestMethod]
        public async Task WhenDeletingNonExistantPost_ItShouldNotModifyTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                target = new DeletePostDbStatement(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(new PostId(Guid.NewGuid()));

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var random = new Random();
                await databaseContext.CreateTestEntitiesAsync(CreatorId.Value, ChannelId.Value, QueueId.Value);
                await databaseContext.CreateTestFileWithExistingUserAsync(CreatorId.Value, FileId.Value);

                var post = PostTests.UniqueFileOrImage(random);
                post.Id = PostId.Value;
                post.ChannelId = ChannelId.Value;
                post.QueueId = QueueId.Value;
                post.PreviewImageId = FileId.Value;
                post.CreationDate = new SqlDateTime(post.CreationDate).Value;
                post.LiveDate = new SqlDateTime(post.LiveDate).Value;
                await databaseContext.Database.Connection.InsertAsync(post);
                await databaseContext.Database.Connection.InsertAsync(new PostFile(PostId.Value, FileId.Value));
                
                await databaseContext.CreateTestUserAsync(UserId.Value, random);

                var comment = CommentTests.Unique(random);
                comment.Id = CommentId.Value;
                comment.PostId = PostId.Value;
                comment.UserId = UserId.Value;
                await databaseContext.Database.Connection.InsertAsync(comment);

                var like = LikeTests.Unique(random);
                like.PostId = PostId.Value;
                like.UserId = UserId.Value;
                await databaseContext.Database.Connection.InsertAsync(like);
            }
        }

        private async Task<List<IIdentityEquatable>> GetExpectedDeletionsAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var post = await databaseContext.Posts.SingleAsync(v => v.Id == PostId.Value);
                var comment = await databaseContext.Comments.SingleAsync(v => v.Id == CommentId.Value);
                var like = await databaseContext.Likes.SingleAsync(v => v.UserId == UserId.Value && v.PostId == PostId.Value);
                var postFile = await databaseContext.PostFiles.SingleAsync(v => v.PostId == PostId.Value);

                var result = new List<IIdentityEquatable>
                {
                    post,
                    comment,
                    like,
                    postFile,
                };

                return result;
            }
        }
    }
}