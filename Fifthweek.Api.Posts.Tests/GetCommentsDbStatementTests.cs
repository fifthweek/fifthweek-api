namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Controllers;
    using Fifthweek.Api.Posts.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetCommentsDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly UserId UserId1 = new UserId(Guid.NewGuid());
        private static readonly UserId UserId2 = new UserId(Guid.NewGuid());
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly DateTime Timestamp = new SqlDateTime(DateTime.UtcNow).Value;

        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private GetCommentsDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new GetCommentsDbStatement(connectionFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequirePostId()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task ItShouldCompleteSuccessfully()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                var expectedResult = await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(PostId);

                Assert.AreEqual(expectedResult, result);

                return ExpectedSideEffects.None;
            });
        }

        private async Task<CommentsResult> CreateEntitiesAsync(TestDatabaseContext testDatabase)
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
                post.FileId = FileId.Value;
                post.CreationDate = new SqlDateTime(post.CreationDate).Value;
                post.LiveDate = new SqlDateTime(post.LiveDate).Value;
                await databaseContext.Database.Connection.InsertAsync(post);

                var user1 = await databaseContext.CreateTestUserAsync(UserId1.Value, random);
                var user2 = await databaseContext.CreateTestUserAsync(UserId2.Value, random);

                var comment1 = CommentTests.Unique(random);
                comment1.PostId = PostId.Value;
                comment1.UserId = UserId1.Value;
                comment1.CreationDate = Timestamp.AddSeconds(-1);
                await databaseContext.Database.Connection.InsertAsync(comment1);
                var comment2 = CommentTests.Unique(random);
                comment2.PostId = PostId.Value;
                comment2.UserId = UserId2.Value;
                comment2.CreationDate = Timestamp.AddSeconds(-2);
                await databaseContext.Database.Connection.InsertAsync(comment2);

                return new CommentsResult(new List<CommentsResult.Item> 
                {
                    this.GetCommentsResultItem(comment2, user2),
                    this.GetCommentsResultItem(comment1, user1),
                });
            }
        }
        
        private CommentsResult.Item GetCommentsResultItem(Persistence.Comment comment, FifthweekUser user)
        {
            return new CommentsResult.Item(
                new CommentId(comment.Id),
                new PostId(comment.PostId),
                new UserId(comment.UserId),
                new Username(user.UserName),
                new Shared.Comment(comment.Content),
                comment.CreationDate);
        }
    }
}