namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Data.Entity;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class SetBacklogPostLiveDateToNowDbStatementTests : PersistenceTestsBase
    {
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly DateTime Now = DateTime.UtcNow;

        private Mock<IFifthweekDbContext> databaseContext;
        private SetBacklogPostLiveDateToNowDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Mock potentially side-effecting components with strict behaviour.            
            this.databaseContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);

            this.InitializeTarget(this.databaseContext.Object);
        }

        public void InitializeTarget(IFifthweekDbContext databaseContext)
        {
            this.target = new SetBacklogPostLiveDateToNowDbStatement(databaseContext);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequirePostId()
        {
            await this.target.ExecuteAsync(null, Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ItShouldRequireUtcDate()
        {
            await this.target.ExecuteAsync(PostId, DateTime.Now);
        }

        [TestMethod]
        public async Task WhenReRun_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase, liveDateInFuture: true, scheduledByQueue: true);
                await this.target.ExecuteAsync(PostId, Now);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(PostId, Now);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task PostIsAlreadyLive_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase, liveDateInFuture: false, scheduledByQueue: true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(PostId, Now);

                return ExpectedSideEffects.None;
            });

            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase, liveDateInFuture: false, scheduledByQueue: false);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(PostId, Now);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenPostIsNotScheduledByQueue_ItShouldSetLiveDateToNow()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                var post = await this.CreateEntitiesAsync(testDatabase, liveDateInFuture: true, scheduledByQueue: false);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(PostId, Now);

                post.LiveDate = new SqlDateTime(Now).Value;

                return new ExpectedSideEffects
                {
                    Update = post
                };
            });
        }

        [TestMethod]
        public async Task WhenPostIsScheduledByQueue_ItShouldSetLiveDateToNowAndUnscheduledFromQueue()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                var post = await this.CreateEntitiesAsync(testDatabase, liveDateInFuture: true, scheduledByQueue: true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(PostId, Now);

                post.ScheduledByQueue = false;
                post.LiveDate = new SqlDateTime(Now).Value;

                return new ExpectedSideEffects
                {
                    Update = post
                };
            });
        }

        private async Task<Post> CreateEntitiesAsync(TestDatabaseContext testDatabase, bool liveDateInFuture, bool scheduledByQueue)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                var channelId = Guid.NewGuid();
                var collectionId = Guid.NewGuid();
                await databaseContext.CreateTestCollectionAsync(Guid.NewGuid(), channelId, collectionId);

                var post = PostTests.UniqueFileOrImage(new Random());
                post.Id = PostId.Value;
                post.ChannelId = channelId;
                post.CollectionId = collectionId;
                post.ScheduledByQueue = scheduledByQueue;
                post.LiveDate = Now.AddDays(liveDateInFuture ? 1 : -1);
                await databaseContext.Database.Connection.InsertAsync(post);
            }

            using (var databaseContext = testDatabase.NewContext())
            {
                var postId = PostId.Value;
                return await databaseContext.Posts.FirstAsync(_ => _.Id == postId);
            }
        }
    }
}