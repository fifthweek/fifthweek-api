namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateAllLiveDatesInQueueDbStatementTests : PersistenceTestsBase
    {
        private static readonly Random Random = new Random(); 
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly IReadOnlyList<DateTime> AscendingLiveDates = new[] { Now.AddDays(1), Now.AddDays(2), Now.AddDays(3), new DateTime(2015, 2, 12, 4, 56, 23, DateTimeKind.Utc),  };

        private Mock<IFifthweekDbContext> databaseContext;
        private UpdateAllLiveDatesInQueueDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give potentially side-effecting components strict mock behaviour.
            this.databaseContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);

            this.InitializeTarget(this.databaseContext.Object);
        }

        public void InitializeTarget(IFifthweekDbContext databaseContext)
        {
            this.target = new UpdateAllLiveDatesInQueueDbStatement(databaseContext);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireCollectionId()
        {
            await this.target.ExecuteAsync(null, AscendingLiveDates, Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireAscendingLiveDates()
        {
            await this.target.ExecuteAsync(CollectionId, null, Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ItShouldRequireAscendingLiveDatesToBeUtc()
        {
            await this.target.ExecuteAsync(
                CollectionId,
                new[] { Now.AddDays(1), DateTime.Now.AddDays(2), Now.AddDays(3) }, 
                Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ItShouldRequireAscendingLiveDatesToBeGreaterThanNow()
        {
            await this.target.ExecuteAsync(
                CollectionId,
                new[] { Now, Now.AddDays(2), Now.AddDays(3) },
                Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ItShouldRequireAscendingLiveDatesToBeGreaterThanNow2()
        {
            await this.target.ExecuteAsync(
                CollectionId,
                new[] { Now.AddDays(-1), Now.AddDays(2), Now.AddDays(3) },
                Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ItShouldRequireAscendingLiveDatesToContainNoDuplicates()
        {
            await this.target.ExecuteAsync(
                CollectionId,
                new[] { Now.AddDays(1), Now.AddDays(2), Now.AddDays(2), Now.AddDays(3) },
                Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ItShouldRequireAscendingLiveDatesToBeSorted()
        {
            await this.target.ExecuteAsync(
                CollectionId,
                new[] { Now.AddDays(1), Now.AddDays(4), Now.AddDays(2), Now.AddDays(3) },
                Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ItShouldRequireUtcDate()
        {
            await this.target.ExecuteAsync(CollectionId, AscendingLiveDates, DateTime.Now);
        }


        [TestMethod]
        public async Task WhenReRun_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreatePostsAsync(testDatabase, CollectionId, AscendingLiveDates, scheduledByQueue: true);
                await this.target.ExecuteAsync(CollectionId, AscendingLiveDates, Now);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(CollectionId, AscendingLiveDates, Now);

                return ExpectedSideEffects.None;
            });
        }

        private async Task<IReadOnlyList<Post>> CreatePostsAsync(
            TestDatabaseContext testDatabase,
            CollectionId collectionId,
            IReadOnlyList<DateTime> liveDates,
            bool scheduledByQueue)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                var user = UserTests.UniqueEntity(Random);
                await databaseContext.Database.Connection.InsertAsync(user);

                var file = FileTests.UniqueEntity(Random);
                file.UserId = user.Id;
                await databaseContext.Database.Connection.InsertAsync(file);

                var subscription = SubscriptionTests.UniqueEntity(Random);
                subscription.CreatorId = user.Id;
                await databaseContext.Database.Connection.InsertAsync(subscription);

                var channel = ChannelTests.UniqueEntity(Random);
                channel.SubscriptionId = subscription.Id;
                await databaseContext.Database.Connection.InsertAsync(channel);

                var collection = CollectionTests.UniqueEntity(Random);
                collection.Id = collectionId.Value;
                collection.ChannelId = channel.Id;
                await databaseContext.Database.Connection.InsertAsync(collection);

                var notes = new List<Post>();
                for (var i = 0; i < 10; i++)
                {
                    // Notes are not covered by this feature as they do not belong in a collection, but we add them to create a more realistic test state.
                    var post = PostTests.UniqueNote(Random);
                    post.ChannelId = channel.Id;

                    notes.Add(post);
                }

                var postsInCollection = new List<Post>();
                foreach (var liveDate in liveDates)
                {
                    var post = PostTests.UniqueFileOrImage(Random);
                    post.ChannelId = channel.Id;
                    post.CollectionId = collectionId.Value;
                    post.FileId = file.Id;
                    post.ScheduledByQueue = scheduledByQueue;
                    post.LiveDate = liveDate;

                    // Clip dates as we will be comparing from these entities.
                    post.LiveDate = new SqlDateTime(post.LiveDate).Value;
                    post.CreationDate = new SqlDateTime(post.CreationDate).Value;

                    postsInCollection.Add(post);
                }

                await databaseContext.Database.Connection.InsertAsync(notes.Concat(postsInCollection));

                return postsInCollection;
            }
        }
    }
}