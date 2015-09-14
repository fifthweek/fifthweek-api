namespace Fifthweek.Api.Channels.Tests
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
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Payments.Tests.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class DeleteChannelDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly CommentId CommentId = CommentId.Random();

        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private MockRequestSnapshotService requestSnapshot;
        private DeleteChannelDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give potentially side-effective components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);
            this.requestSnapshot = new MockRequestSnapshotService();
            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new DeleteChannelDbStatement(connectionFactory, this.requestSnapshot);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireChannelId()
        {
            await this.target.ExecuteAsync(CreatorId, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireUserId()
        {
            await this.target.ExecuteAsync(null, ChannelId);
        }

        [TestMethod]
        public async Task ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await this.target.ExecuteAsync(CreatorId, ChannelId);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(CreatorId, ChannelId);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldDeleteChannelAndChildTypes()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                var expectedDeletions = await this.GetExpectedDeletionsAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(CreatorId, ChannelId);

                return new ExpectedSideEffects
                {
                    Deletes = expectedDeletions,
                };
            });
        }

        [TestMethod]
        public async Task ItShouldRequestSnapshotAfterUpdate()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var trackingDatabase = new TrackingConnectionFactory(testDatabase);
                this.InitializeTarget(trackingDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                var expectedDeletions = await this.GetExpectedDeletionsAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                this.requestSnapshot.VerifyConnectionDisposed(trackingDatabase);

                await this.target.ExecuteAsync(CreatorId, ChannelId);

                this.requestSnapshot.VerifyCalledWith(CreatorId, SnapshotType.CreatorChannels);

                return new ExpectedSideEffects
                {
                    Deletes = expectedDeletions,
                };
            });
        }

        [TestMethod]
        public async Task ItShouldAbortUpdateIfSnapshotFails()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                this.requestSnapshot.ThrowException();

                await ExpectedException.AssertExceptionAsync<SnapshotException>(
                    () => this.target.ExecuteAsync(CreatorId, ChannelId));

                return ExpectedSideEffects.TransactionAborted;
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
                post.FileId = FileId.Value;
                post.CreationDate = new SqlDateTime(post.CreationDate).Value;
                post.LiveDate = new SqlDateTime(post.LiveDate).Value;
                await databaseContext.Database.Connection.InsertAsync(post);

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
            }
        }

        private async Task<List<IIdentityEquatable>> GetExpectedDeletionsAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var channel = await databaseContext.Channels.SingleAsync(v => v.Id == ChannelId.Value);
                var subscription = await databaseContext.ChannelSubscriptions.SingleAsync(v => v.ChannelId == ChannelId.Value && v.UserId == CreatorId.Value);
                var post = await databaseContext.Posts.SingleAsync(v => v.Id == PostId.Value);
                var comment = await databaseContext.Comments.SingleAsync(v => v.Id == CommentId.Value);
                var like = await databaseContext.Likes.SingleAsync(v => v.UserId == UserId.Value && v.PostId == PostId.Value);

                var result = new List<IIdentityEquatable>
                {
                    channel,
                    post,
                    subscription,
                    comment,
                    like,
                };

                return result;
            }
        }
    } 
}