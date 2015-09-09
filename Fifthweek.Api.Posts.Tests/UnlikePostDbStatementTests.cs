namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Data.SqlTypes;
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
    public class UnlikePostDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());

        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private UnlikePostDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);
            
            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new UnlikePostDbStatement(connectionFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireUserId()
        {
            await this.target.ExecuteAsync(null, PostId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequirePostId()
        {
            await this.target.ExecuteAsync(UserId, null);
        }

        [TestMethod]
        public async Task ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await this.target.ExecuteAsync(UserId, PostId);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(UserId, PostId);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldCompleteSuccessfully()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                var expectedDelete = await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(UserId, PostId);

                return new ExpectedSideEffects
                {
                    Delete = expectedDelete,
                };
            });
        }

        private async Task<Like> CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var random = new Random();
                await databaseContext.CreateTestCollectionAsync(CreatorId.Value, ChannelId.Value, CollectionId.Value);
                await databaseContext.CreateTestFileWithExistingUserAsync(CreatorId.Value, FileId.Value);

                var post = PostTests.UniqueFileOrImage(random);
                post.Id = PostId.Value;
                post.ChannelId = ChannelId.Value;
                post.CollectionId = CollectionId.Value;
                post.FileId = FileId.Value;
                post.CreationDate = new SqlDateTime(post.CreationDate).Value;
                post.LiveDate = new SqlDateTime(post.LiveDate).Value;
                await databaseContext.Database.Connection.InsertAsync(post);

                await databaseContext.CreateTestUserAsync(UserId.Value, random);

                var like = LikeTests.Unique(random);
                like.PostId = PostId.Value;
                like.UserId = UserId.Value;
                like.CreationDate = new SqlDateTime(DateTime.UtcNow).Value;

                await databaseContext.Database.Connection.InsertAsync(like);

                return like;
            }
        }
    }
}