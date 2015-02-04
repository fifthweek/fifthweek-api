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
        private static readonly DateTime NewDate = Now.AddDays(2);
        private static readonly DateTime ClippedDate = Now.AddDays(1);

        private Mock<IScheduledDateClippingFunction> scheduledDateClipping;
        private Mock<IFifthweekDbContext> databaseContext;
        private SetBacklogPostLiveDateDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.scheduledDateClipping = new Mock<IScheduledDateClippingFunction>();
            this.scheduledDateClipping.Setup(_ => _.Apply(Now, NewDate)).Returns(ClippedDate);

            // Mock potentially side-effecting components with strict behaviour.            
            this.databaseContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);

            this.InitializeTarget(this.databaseContext.Object);
        }

        public void InitializeTarget(IFifthweekDbContext databaseContext)
        {
            this.target = new SetBacklogPostLiveDateDbStatement(this.scheduledDateClipping.Object, databaseContext);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequirePostId()
        {
            await this.target.ExecuteAsync(null, NewDate, Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ItShouldRequireUtcDates()
        {
            await this.target.ExecuteAsync(PostId, DateTime.Now, Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ItShouldRequireUtcDates2()
        {
            await this.target.ExecuteAsync(PostId, NewDate, DateTime.Now);
        }

        [TestMethod]
        public async Task WhenReRun_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase, liveDateInFuture: true, scheduledByQueue: true);
                await this.target.ExecuteAsync(PostId, NewDate, Now);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(PostId, NewDate, Now);

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

                await this.target.ExecuteAsync(PostId, NewDate, Now);

                return ExpectedSideEffects.None;
            });

            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase, liveDateInFuture: false, scheduledByQueue: false);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(PostId, NewDate, Now);

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

                await this.target.ExecuteAsync(PostId, NewDate, Now);

                post.LiveDate = new SqlDateTime(ClippedDate).Value;

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

                await this.target.ExecuteAsync(PostId, NewDate, Now);

                post.ScheduledByQueue = false;
                post.LiveDate = new SqlDateTime(ClippedDate).Value;

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
                post.LiveDate = Now.AddDays(liveDateInFuture ? 10 : -10);
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