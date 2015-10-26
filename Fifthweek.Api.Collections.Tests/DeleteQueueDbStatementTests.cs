namespace Fifthweek.Api.Collections.Tests
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

    using Moq;

    [TestClass]
    public class DeleteQueueDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly CommentId CommentId = CommentId.Random();
        
        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private DeleteQueueDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give potentially side-effective components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new DeleteQueueDbStatement(connectionFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireCollectionId()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await this.target.ExecuteAsync(QueueId);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(QueueId);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldDeleteQueue()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                var post = await this.CreateEntitiesAsync(testDatabase);
                var expectedDeletions = await this.GetExpectedDeletionsAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var expectedPost = post.Copy(_ => _.QueueId = null);

                await this.target.ExecuteAsync(QueueId);

                return new ExpectedSideEffects
                {
                    Deletes = expectedDeletions,
                    Update = expectedPost,
                };
            });
        }

        private async Task<Post> CreateEntitiesAsync(TestDatabaseContext testDatabase)
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
                
                await databaseContext.CreateTestChannelSubscriptionWithExistingReferences(CreatorId.Value, ChannelId.Value);

                var weeklyReleaseTimes = WeeklyReleaseTimeTests.GenerateSortedWeeklyReleaseTimes(QueueId.Value, 3);
                await databaseContext.Database.Connection.InsertAsync(weeklyReleaseTimes);

                return post;
            }
        }

        private async Task<List<IIdentityEquatable>> GetExpectedDeletionsAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var queue = await databaseContext.Queues.SingleAsync(v => v.Id == QueueId.Value);
                var weeklyReleaseTimes = await databaseContext.WeeklyReleaseTimes.Where(v => v.QueueId == QueueId.Value).ToListAsync();

                var result = new List<IIdentityEquatable>
                {
                    queue,
                };

                result.AddRange(weeklyReleaseTimes);
                return result;
            }
        }
    } 
}