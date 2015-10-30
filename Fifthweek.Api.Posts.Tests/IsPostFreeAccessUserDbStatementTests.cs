namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.AssemblyResolution;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class IsPostFreeAccessUserDbStatementTests : PersistenceTestsBase
    {
        private static readonly Random Random = new Random();
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly UserId UnsubscribedUserId = new UserId(Guid.NewGuid());
        private static readonly UserId SubscribedUserId = new UserId(Guid.NewGuid());
        private static readonly UserId GuestListUserId = new UserId(Guid.NewGuid());
        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = ChannelId.Random();
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly int ChannelPrice = 10;

        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private IsPostFreeAccessUserDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            DapperTypeHandlerRegistration.Register(FifthweekAssembliesResolver.Assemblies);

            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new IsPostFreeAccessUserDbStatement(connectionFactory);
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
            await this.target.ExecuteAsync(SubscribedUserId, null);
        }

        [TestMethod]
        public async Task ItShoudReturnExpectedResults()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();
                await this.PerformTest(CreatorId, PostId, false);
                await this.PerformTest(UnsubscribedUserId, PostId, false);
                await this.PerformTest(SubscribedUserId, PostId, false);
                await this.PerformTest(GuestListUserId, PostId, true);
                return ExpectedSideEffects.None;
            });
        }

        private async Task PerformTest(UserId userId, PostId postId, bool expectedResult)
        {
            var result = await this.target.ExecuteAsync(userId, postId);
            Assert.AreEqual(expectedResult, result);
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestEntitiesAsync(CreatorId.Value, ChannelId.Value, QueueId.Value, BlogId.Value);
                await databaseContext.SaveChangesAsync();

                var post = PostTests.UniqueFileOrImage(Random);
                post.ChannelId = ChannelId.Value;
                post.Id = PostId.Value;
                await databaseContext.Database.Connection.InsertAsync(post);

                await this.CreateUserAsync(databaseContext, UnsubscribedUserId);
                await this.CreateUserAsync(databaseContext, SubscribedUserId);
                await this.CreateUserAsync(databaseContext, GuestListUserId);

                var channelSubscriptions = new List<ChannelSubscription>();
                var freeAccessUsers = new List<FreeAccessUser>();

                channelSubscriptions.Add(new ChannelSubscription(ChannelId.Value, null, SubscribedUserId.Value, null, ChannelPrice, Now, Now));

                freeAccessUsers.Add(new FreeAccessUser(BlogId.Value, GuestListUserId.Value + "@test.com"));

                await databaseContext.Database.Connection.InsertAsync(channelSubscriptions);
                await databaseContext.Database.Connection.InsertAsync(freeAccessUsers);
            }
        }

        private async Task CreateUserAsync(FifthweekDbContext databaseContext, UserId userId)
        {
            var user = UserTests.UniqueEntity(Random);
            user.Id = userId.Value;
            user.Email = userId.Value + "@test.com";

            databaseContext.Users.Add(user);
            await databaseContext.SaveChangesAsync();
        }
    }
}