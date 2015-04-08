namespace Fifthweek.Api.Posts.Tests.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ReorderQueueCommandHandlerTests : PersistenceTestsBase
    {
        private const int CollectionTotal = 20;
        private const int CollectionSubset = 5;

        // Ensure tests run deterministically on every run from same source.
        private static readonly Random Random = new Random(12345); 

        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly IReadOnlyList<PostId> NewOrder = new[]
        {
            new PostId(Guid.NewGuid()), 
            new PostId(Guid.NewGuid()), 
            new PostId(Guid.NewGuid()), 
            new PostId(Guid.NewGuid()), 
            new PostId(Guid.NewGuid())
        };

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IFifthweekDbConnectionFactory> connectionFactory;

        private ReorderQueueCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);

            // Mock potentially side-effecting components with strict behaviour.            
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.target = new ReorderQueueCommandHandler(this.requesterSecurity.Object, this.connectionFactory.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireCommand()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldRequireUserIsAuthenticated()
        {
            await this.target.HandleAsync(new ReorderQueueCommand(Requester.Unauthenticated, CollectionId, NewOrder));
        }

        [TestMethod]
        public async Task WhenFewerThan2IdsAreProvided_ItShouldHaveNoEffect()
        {
            await this.target.HandleAsync(new ReorderQueueCommand(Requester, CollectionId, new PostId[0]));
            await this.target.HandleAsync(new ReorderQueueCommand(Requester, CollectionId, new PostId[1]));
        }

        [TestMethod]
        public async Task ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new ReorderQueueCommandHandler(this.requesterSecurity.Object, testDatabase);

                var validPosts = await this.CreateValidQueueCandidatesAsync(testDatabase);
                var attemptedNewQueueOrder = GetRandomSubset(validPosts);
                var command = new ReorderQueueCommand(Requester, CollectionId, attemptedNewQueueOrder);
                await this.target.HandleAsync(command);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(command);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReorderPostsInTheProvidedList()
        {
            // Test with a subset of the collection's posts.
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var validPosts = await this.CreateValidQueueCandidatesAsync(testDatabase);

                var attemptedNewQueueOrder = GetRandomSubset(validPosts);

                return await this.TestExpectedQueueOrderAsync(testDatabase, validPosts, new ReorderQueueCommand(Requester, CollectionId, attemptedNewQueueOrder));
            });

            // Test with all of the collection's posts.
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var validPosts = await this.CreateValidQueueCandidatesAsync(testDatabase);

                var attemptedNewQueueOrder = Shuffle(validPosts);

                return await this.TestExpectedQueueOrderAsync(testDatabase, validPosts, new ReorderQueueCommand(Requester, CollectionId, attemptedNewQueueOrder));
            });
        }

        [TestMethod]
        public async Task ItShouldOnlyReorderPostsThatBelongToTheCreator()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var invalidPosts = await this.CreatePostsAsync(
                    testDatabase,
                    new UserId(Guid.NewGuid()),
                    CollectionId,
                    liveDateInFuture: true,
                    scheduledByQueue: true);

                var attemptedNewQueueOrder = GetRandomSubset(invalidPosts);

                return await this.TestExpectedQueueOrderAsync(testDatabase, new Post[0], new ReorderQueueCommand(Requester, CollectionId, attemptedNewQueueOrder));
            });
        }

        [TestMethod]
        public async Task ItShouldOnlyReorderPostsThatBelongToTheCollection()
        {
            await this.TestInvalidReorderCandidates(testDatabase =>
                this.CreatePostsAsync(
                        testDatabase,
                        UserId,
                        new CollectionId(Guid.NewGuid()),
                        liveDateInFuture: true,
                        scheduledByQueue: true));
        }

        [TestMethod]
        public async Task ItShouldOnlyReorderQueuedPosts()
        {
            await this.TestInvalidReorderCandidates(testDatabase =>
                this.CreatePostsAsync(
                        testDatabase,
                        UserId,
                        CollectionId,
                        liveDateInFuture: true,
                        scheduledByQueue: false));
        }

        [TestMethod]
        public async Task ItShouldOnlyReorderNonLivePosts()
        {
            await this.TestInvalidReorderCandidates(testDatabase =>
                this.CreatePostsAsync(
                        testDatabase,
                        UserId,
                        CollectionId,
                        liveDateInFuture: false,
                        scheduledByQueue: true));
        }

        private static IReadOnlyList<PostId> Shuffle(IEnumerable<Post> collection)
        {
            return collection.Shuffle(Random).Select(_ => new PostId(_.Id)).ToArray();
        }

        private static IReadOnlyList<PostId> ShuffledConcat(IEnumerable<PostId> collectionA, IEnumerable<PostId> collectionB)
        {
            return collectionA.Concat(collectionB).Shuffle(Random).ToArray();
        }

        private static IReadOnlyList<PostId> GetRandomSubset(IReadOnlyList<Post> superset)
        {
            if (superset.Count != CollectionTotal)
            {
                // Ensure we are working to a known collection size, rather than calculating some relative subset. Ensures
                // all our tests are consistent, and will flag any 'arrange' errors as currently all our post ID collections
                // are created to this size.
                throw new Exception("Expected a superset of size " + CollectionTotal);
            }

            return superset.Shuffle(Random).Take(CollectionSubset).Select(_ => new PostId(_.Id)).ToArray();
        }

        private async Task TestInvalidReorderCandidates(Func<TestDatabaseContext, Task<IReadOnlyList<Post>>> getInvalidReorderCandidates)
        {
            // Test with mixture of valid and invalid posts. Expect only valid posts to be re-ordered.
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var validPosts = await this.CreateValidQueueCandidatesAsync(testDatabase);
                var invalidPosts = await getInvalidReorderCandidates(testDatabase);

                var attemptedNewQueueOrder = ShuffledConcat(
                    GetRandomSubset(validPosts),
                    GetRandomSubset(invalidPosts));

                return await this.TestExpectedQueueOrderAsync(testDatabase, validPosts, new ReorderQueueCommand(Requester, CollectionId, attemptedNewQueueOrder));
            });

            // Test with invalid posts only. Expect no posts to be re-ordered.
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var invalidPosts = await getInvalidReorderCandidates(testDatabase);

                var attemptedNewQueueOrder = GetRandomSubset(invalidPosts);

                return await this.TestExpectedQueueOrderAsync(testDatabase, new Post[0], new ReorderQueueCommand(Requester, CollectionId, attemptedNewQueueOrder));
            });
        }

        private async Task<ExpectedSideEffects> TestExpectedQueueOrderAsync(TestDatabaseContext testDatabase, IReadOnlyList<Post> validSuperset, ReorderQueueCommand command)
        {
            this.target = new ReorderQueueCommandHandler(this.requesterSecurity.Object, testDatabase);

            await testDatabase.TakeSnapshotAsync();

            await this.target.HandleAsync(command);

            if (validSuperset.Count == 0)
            {
                return ExpectedSideEffects.None;
            }

            // Posts that are both valid for update, and have been requested to be updated by the command.
            var allowedUpdates = validSuperset.Where(_ => command.NewPartialQueueOrder.Any(id => id.Value == _.Id)).ToArray();
            
            // Descending range of live dates for these posts.
            var expectedDateRange = allowedUpdates.Select(_ => _.LiveDate).OrderByDescending(_ => _).ToArray();
            
            // Re-assign live dates in order specified by command.
            var dateIndex = 0;
            var expectedUpdates = new List<Post>();
            foreach (var postId in command.NewPartialQueueOrder)
            {
                // Request may contain IDs that are not valid for update, so we ignore these, as the implementation should.
                var allowedUpdate = allowedUpdates.SingleOrDefault(_ => _.Id == postId.Value);
                if (allowedUpdate != null)
                {
                    var newLiveDate = expectedDateRange[dateIndex++];
                    if (allowedUpdate.LiveDate == newLiveDate)
                    {
                        continue;
                    }

                    // Sometimes the live date may not have changed.
                    allowedUpdate.LiveDate = newLiveDate;
                    expectedUpdates.Add(allowedUpdate);
                }
            }

            if (expectedUpdates.Count == 0)
            {
                throw new Exception(
                    "It is unlikely that all posts will keep their original live date after being shuffled. " + 
                    "Please check test code. If all is good and this is coincidental, then simply change the random seed number.");
            }

            return new ExpectedSideEffects
            {
                Updates = expectedUpdates
            };
        }

        private Task<IReadOnlyList<Post>> CreateValidQueueCandidatesAsync(TestDatabaseContext testDatabase)
        {
            return this.CreatePostsAsync(
                testDatabase,
                UserId,
                CollectionId,
                liveDateInFuture: true,
                scheduledByQueue: true);
        }

        private async Task<IReadOnlyList<Post>> CreatePostsAsync(
            TestDatabaseContext testDatabase,
            UserId userId,
            CollectionId collectionId,
            bool liveDateInFuture,
            bool scheduledByQueue)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var user = UserTests.UniqueEntity(Random);
                user.Id = userId.Value;
                await databaseContext.Database.Connection.InsertAsync(user);

                var file = FileTests.UniqueEntity(Random);
                file.Id = FileId.Value;
                file.UserId = user.Id;
                await databaseContext.Database.Connection.InsertAsync(file);

                var subscription = BlogTests.UniqueEntity(Random);
                subscription.CreatorId = user.Id;
                await databaseContext.Database.Connection.InsertAsync(subscription);

                var channel = ChannelTests.UniqueEntity(Random);
                channel.BlogId = subscription.Id;
                await databaseContext.Database.Connection.InsertAsync(channel);

                var collection = CollectionTests.UniqueEntity(Random);
                collection.Id = collectionId.Value;
                collection.ChannelId = channel.Id;
                await databaseContext.Database.Connection.InsertAsync(collection);

                var notes = new List<Post>();
                for (var i = 0; i < CollectionTotal; i++)
                {
                    // Notes are not covered by this feature as they do not belong in a collection, but we add them to create a more realistic test state.
                    var post = PostTests.UniqueNote(Random);
                    post.ChannelId = channel.Id;
                    
                    notes.Add(post);
                }

                var postsInCollection = new List<Post>();
                for (var i = 0; i < CollectionTotal; i++)
                {
                    var post = PostTests.UniqueFileOrImage(Random);
                    post.ChannelId = channel.Id;
                    post.CollectionId = collectionId.Value;
                    post.FileId = file.Id;
                    post.ScheduledByQueue = scheduledByQueue;
                    post.LiveDate = Now.AddDays((1 + Random.Next(100)) * (liveDateInFuture ? 1 : -1));

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