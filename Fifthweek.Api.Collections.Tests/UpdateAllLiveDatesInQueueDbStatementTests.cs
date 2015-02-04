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
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateAllLiveDatesInQueueDbStatementTests : PersistenceTestsBase
    {
        private static readonly Random Random = new Random(); 
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly IReadOnlyList<DateTime> PastDates = new[] { Now.AddDays(-1), Now.AddDays(-2), Now.AddDays(-3) };
        private static readonly IReadOnlyList<DateTime> FutureDatesA = new[] { Now.AddDays(1), Now.AddDays(2), Now.AddDays(3) };
        private static readonly IReadOnlyList<DateTime> FutureDatesB = new[] { Now.AddDays(4), Now.AddDays(5), Now.AddDays(6) };
        private static readonly IReadOnlyList<DateTime> FutureDatesC = new[] { Now.AddDays(7), Now.AddDays(8), Now.AddDays(9) };

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
            await this.target.ExecuteAsync(null, FutureDatesA, Now);
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
            await this.target.ExecuteAsync(CollectionId, FutureDatesA, DateTime.Now);
        }

        [TestMethod]
        public async Task WhenReRun_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreatePostsAsync(testDatabase, CollectionId, FutureDatesA, scheduledByQueue: true);
                await this.target.ExecuteAsync(CollectionId, FutureDatesB, Now);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(CollectionId, FutureDatesB, Now);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenAllLiveDatesAlreadyMatchExistingDates_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreatePostsAsync(testDatabase, CollectionId, FutureDatesA, scheduledByQueue: true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(CollectionId, FutureDatesA, Now);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenAllQueuedPostsAreAlreadyLive_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreatePostsAsync(testDatabase, CollectionId, PastDates, scheduledByQueue: true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(CollectionId, FutureDatesA, Now);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenAllNewDatesAreDifferent_ItShouldUpdateLiveDates()
        {
            await this.AssertSuccessAsync(FutureDatesA, FutureDatesB);
        }

        [TestMethod]
        public async Task WhenSomeNewDatesAreDifferent_ItShouldOnlyUpdateChangedLiveDates()
        {
            var futureDatesWithSameFirstElement = FutureDatesA.Take(1).Concat(FutureDatesB).ToArray();
            await this.AssertSuccessAsync(FutureDatesA, futureDatesWithSameFirstElement);
        }

        [TestMethod]
        public async Task WhenSomePostsAreAlreadyLive_ItShouldOnlyUpdateQueuedPostLiveDates()
        {
            await this.AssertSuccessAsync(FutureDatesA, FutureDatesB, PastDates);
        }

        [TestMethod]
        public async Task WhenTooManyLiveDatesAreProvided_ItShouldOnlyUpdateUsingLowestDates()
        {
            await this.AssertSuccessAsync(FutureDatesA, FutureDatesB.Concat(FutureDatesC).ToArray());
        }

        [TestMethod]
        public async Task WhenPostsAreNotScheduledByQueue_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreatePostsAsync(testDatabase, CollectionId, FutureDatesA, scheduledByQueue: false);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(CollectionId, FutureDatesB, Now);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenNotEnoughLiveDatesAreProvided_ItShouldThrowException()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreatePostsAsync(testDatabase, CollectionId, FutureDatesA, scheduledByQueue: true);
                await testDatabase.TakeSnapshotAsync();

                await ExpectedException.AssertExceptionAsync<ArgumentException>(() =>
                {
                    return this.target.ExecuteAsync(CollectionId, FutureDatesB.Skip(1).ToArray(), Now);
                });

                return ExpectedSideEffects.None;
            });
        }

        private async Task AssertSuccessAsync(IReadOnlyList<DateTime> existingFutureLiveDates, IReadOnlyList<DateTime> newLiveDates, IReadOnlyList<DateTime> existingPastLiveDates = null)
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());

                var newEntityDates = existingPastLiveDates == null 
                    ? existingFutureLiveDates
                    : existingFutureLiveDates.Concat(existingPastLiveDates).ToArray();

                var posts = await this.CreatePostsAsync(testDatabase, CollectionId, newEntityDates, scheduledByQueue: true);
                var futurePostFilter = new HashSet<DateTime>(existingFutureLiveDates.Select(_ => new SqlDateTime(_).Value));                
                var futurePosts = posts.Where(_ => futurePostFilter.Contains(_.LiveDate)).ToArray();

                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(CollectionId, newLiveDates, Now);

                var updatedPosts = new List<Post>();
                for (var i = 0; i < futurePosts.Length; i++)
                {
                    var newDate = new SqlDateTime(newLiveDates[i]).Value;
                    if (futurePosts[i].LiveDate == newDate)
                    {
                        continue;
                    }

                    futurePosts[i].LiveDate = newDate;
                    updatedPosts.Add(futurePosts[i]);
                }

                return new ExpectedSideEffects
                {
                    Updates = updatedPosts
                };
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