namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.AssemblyResolution;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class TryGetQueuedPostCollectionDbStatementTests : PersistenceTestsBase
    {
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly DateTime Now = DateTime.UtcNow;

        private Mock<IFifthweekDbContext> databaseContext;
        private TryGetQueuedPostCollectionDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            DapperTypeHandlerRegistration.Register(FifthweekAssembliesResolver.Assemblies);

            // Give potentially side-effecting components strict mock behaviour.
            this.databaseContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);

            this.InitializeTarget(this.databaseContext.Object);
        }

        public void InitializeTarget(IFifthweekDbContext databaseContext)
        {
            this.target = new TryGetQueuedPostCollectionDbStatement(databaseContext);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequirePostId()
        {
            await this.target.ExecuteAsync(null, DateTime.UtcNow);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ItShouldRequireUtcDate()
        {
            await this.target.ExecuteAsync(PostId, DateTime.Now);
        }

        [TestMethod]
        public async Task WhenPostDoesNotExist_ItShouldReturnNull()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(PostId, Now);

                Assert.IsNull(result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenPostExistsAndIsNotQueued_ItShouldReturnNull()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase, queuePost: false);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(PostId, Now);

                Assert.IsNull(result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenPostExistsAndWasQueuedButNowLive_ItShouldReturnNull()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase, queuePost: true, queueInFuture: false);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(PostId, Now);

                Assert.IsNull(result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenPostExistsAndIsQueued_ItShouldReturnCollectionId()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase, queuePost: true, queueInFuture: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(PostId, Now);

                Assert.AreEqual(result, CollectionId);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase, bool queuePost, bool queueInFuture = false)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                var userId = Guid.NewGuid();
                var channelId = Guid.NewGuid();
                await databaseContext.CreateTestCollectionAsync(userId, channelId, CollectionId.Value);

                var post = PostTests.UniqueFileOrImage(new Random());
                post.Id = PostId.Value;
                post.ChannelId = channelId;
                post.CollectionId = CollectionId.Value;
                post.ScheduledByQueue = queuePost;
                post.LiveDate = queueInFuture ? Now.AddDays(1) : Now.AddDays(-1);
                await databaseContext.Database.Connection.InsertAsync(post);
            }
        }
    }
}