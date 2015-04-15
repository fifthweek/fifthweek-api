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

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class DeleteChannelDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());

        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private DeleteChannelDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give potentially side-effective components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new DeleteChannelDbStatement(connectionFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireChannelId()
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
                await this.target.ExecuteAsync(ChannelId);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(ChannelId);

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

                await this.target.ExecuteAsync(ChannelId);

                return new ExpectedSideEffects
                {
                    Deletes = expectedDeletions,
                };
            });
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestCollectionAsync(UserId.Value, ChannelId.Value, CollectionId.Value);
                await databaseContext.CreateTestFileWithExistingUserAsync(UserId.Value, FileId.Value);

                var post = PostTests.UniqueFileOrImage(new Random());
                post.Id = PostId.Value;
                post.ChannelId = ChannelId.Value;
                post.CollectionId = CollectionId.Value;
                post.FileId = FileId.Value;
                post.CreationDate = new SqlDateTime(post.CreationDate).Value;
                post.LiveDate = new SqlDateTime(post.LiveDate).Value;
                await databaseContext.Database.Connection.InsertAsync(post);

                await databaseContext.CreateTestChannelSubscriptionWithExistingReferences(UserId.Value, ChannelId.Value);

                var weeklyReleaseTimes = WeeklyReleaseTimeTests.GenerateSortedWeeklyReleaseTimes(CollectionId.Value, 3);
                await databaseContext.Database.Connection.InsertAsync(weeklyReleaseTimes);
            }
        }

        private async Task<List<IIdentityEquatable>> GetExpectedDeletionsAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var channel = await databaseContext.Channels.SingleAsync(v => v.Id == ChannelId.Value);
                var collection = await databaseContext.Collections.SingleAsync(v => v.Id == CollectionId.Value);
                var subscription = await databaseContext.ChannelSubscriptions.SingleAsync(v => v.ChannelId == ChannelId.Value && v.UserId == UserId.Value);
                var post = await databaseContext.Posts.SingleAsync(v => v.Id == PostId.Value);
                var weeklyReleaseTimes = await databaseContext.WeeklyReleaseTimes.Where(v => v.CollectionId == CollectionId.Value).ToListAsync();

                var result = new List<IIdentityEquatable>
                {
                    channel,
                    collection,
                    post,
                    subscription,
                };

                result.AddRange(weeklyReleaseTimes);

                return result;
            }
        }
    } 
}