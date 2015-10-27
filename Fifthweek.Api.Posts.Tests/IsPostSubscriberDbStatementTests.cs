namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.AssemblyResolution;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Comment = Fifthweek.Api.Posts.Shared.Comment;

    [TestClass]
    public class IsPostSubscriberDbStatementTests : PersistenceTestsBase
    {
        private static readonly Random Random = new Random();
        private static readonly PostId PastPostId = new PostId(Guid.NewGuid());
        private static readonly PostId FuturePostId = new PostId(Guid.NewGuid());
        private static readonly UserId UnsubscribedUserId = new UserId(Guid.NewGuid());
        private static readonly UserId SubscribedLowPriceUserId = new UserId(Guid.NewGuid());
        private static readonly UserId SubscribedHighPriceUserId = new UserId(Guid.NewGuid());
        private static readonly UserId SubscribedUserId = new UserId(Guid.NewGuid());
        private static readonly UserId SubscribedUserIdNoBalance = new UserId(Guid.NewGuid());
        private static readonly UserId SubscribedUserIdZeroBalance = new UserId(Guid.NewGuid());
        private static readonly UserId SubscribedUserIdZeroBalancePaymentInProgress = new UserId(Guid.NewGuid());
        private static readonly UserId GuestListUserId = new UserId(Guid.NewGuid());
        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = ChannelId.Random();
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly DateTime Past = Now.AddDays(-1);
        private static readonly DateTime Future = Now.AddDays(1);
        private static readonly int ChannelPrice = 10;

        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private IsPostSubscriberDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            DapperTypeHandlerRegistration.Register(FifthweekAssembliesResolver.Assemblies);

            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new IsPostSubscriberDbStatement(connectionFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireUserId()
        {
            await this.target.ExecuteAsync(null, PastPostId, Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequirePostId()
        {
            await this.target.ExecuteAsync(SubscribedUserId, null, Now);
        }

        [TestMethod]
        public async Task ItShoudReturnExpectedResults()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();
                await this.PerformTest(UnsubscribedUserId, PastPostId, false);
                await this.PerformTest(UnsubscribedUserId, FuturePostId, false);
                await this.PerformTest(SubscribedLowPriceUserId, PastPostId, false);
                await this.PerformTest(SubscribedLowPriceUserId, FuturePostId, false);
                await this.PerformTest(SubscribedHighPriceUserId, PastPostId, true);
                await this.PerformTest(SubscribedHighPriceUserId, FuturePostId, false);
                await this.PerformTest(SubscribedUserId, PastPostId, true);
                await this.PerformTest(SubscribedUserId, FuturePostId, false);
                await this.PerformTest(SubscribedUserIdNoBalance, PastPostId, false);
                await this.PerformTest(SubscribedUserIdNoBalance, FuturePostId, false);
                await this.PerformTest(SubscribedUserIdZeroBalance, PastPostId, false);
                await this.PerformTest(SubscribedUserIdZeroBalance, FuturePostId, false);
                await this.PerformTest(SubscribedUserIdZeroBalancePaymentInProgress, PastPostId, true);
                await this.PerformTest(SubscribedUserIdZeroBalancePaymentInProgress, FuturePostId, false);
                await this.PerformTest(GuestListUserId, PastPostId, true);
                await this.PerformTest(GuestListUserId, FuturePostId, false);
                return ExpectedSideEffects.None;
            });
        }

        private async Task PerformTest(UserId userId, PostId postId, bool expectedResult)
        {
            var result = await this.target.ExecuteAsync(userId, postId, Now);
            Assert.AreEqual(expectedResult, result);
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var entities = await databaseContext.CreateTestEntitiesAsync(CreatorId.Value, ChannelId.Value, QueueId.Value, BlogId.Value);
                entities.Channel.Price = ChannelPrice;
                await databaseContext.SaveChangesAsync();

                var pastPost = PostTests.UniqueFileOrImage(Random);
                pastPost.ChannelId = ChannelId.Value;
                pastPost.Id = PastPostId.Value;
                pastPost.LiveDate = Past;
                await databaseContext.Database.Connection.InsertAsync(pastPost);

                var futurePost = PostTests.UniqueFileOrImage(Random);
                futurePost.ChannelId = ChannelId.Value;
                futurePost.Id = FuturePostId.Value;
                futurePost.LiveDate = Future;
                await databaseContext.Database.Connection.InsertAsync(futurePost);

                await this.CreateUserAsync(databaseContext, UnsubscribedUserId);
                await this.CreateUserAsync(databaseContext, SubscribedUserId);
                await this.CreateUserAsync(databaseContext, SubscribedLowPriceUserId);
                await this.CreateUserAsync(databaseContext, SubscribedHighPriceUserId);
                await this.CreateUserAsync(databaseContext, SubscribedUserIdNoBalance);
                await this.CreateUserAsync(databaseContext, SubscribedUserIdZeroBalance);
                await this.CreateUserAsync(databaseContext, SubscribedUserIdZeroBalancePaymentInProgress);
                await this.CreateUserAsync(databaseContext, GuestListUserId);

                var channelSubscriptions = new List<ChannelSubscription>();
                var calculatedAccountBalances = new List<CalculatedAccountBalance>();
                var freeAccessUsers = new List<FreeAccessUser>();
                var origins = new List<UserPaymentOrigin>();

                channelSubscriptions.Add(new ChannelSubscription(ChannelId.Value, null, SubscribedUserId.Value, null, ChannelPrice, Now, Now));
                calculatedAccountBalances.Add(new CalculatedAccountBalance(SubscribedUserId.Value, LedgerAccountType.FifthweekCredit, Now, 10));
                calculatedAccountBalances.Add(new CalculatedAccountBalance(SubscribedUserId.Value, LedgerAccountType.FifthweekCredit, Now.AddDays(-1), 0));

                channelSubscriptions.Add(new ChannelSubscription(ChannelId.Value, null, SubscribedUserIdNoBalance.Value, null, ChannelPrice, Now, Now));
                origins.Add(new UserPaymentOrigin(SubscribedUserIdNoBalance.Value, null, null, default(PaymentOriginKeyType), null, null, null, null, PaymentStatus.Failed));

                // The query should round down to zero for account balance.
                channelSubscriptions.Add(new ChannelSubscription(ChannelId.Value, null, SubscribedUserIdZeroBalance.Value, null, ChannelPrice, Now, Now));
                calculatedAccountBalances.Add(new CalculatedAccountBalance(SubscribedUserIdZeroBalance.Value, LedgerAccountType.FifthweekCredit, Now.AddDays(-1), 10));
                calculatedAccountBalances.Add(new CalculatedAccountBalance(SubscribedUserIdZeroBalance.Value, LedgerAccountType.FifthweekCredit, Now, 0.8m));

                channelSubscriptions.Add(new ChannelSubscription(ChannelId.Value, null, SubscribedUserIdZeroBalancePaymentInProgress.Value, null, ChannelPrice, Now, Now));
                calculatedAccountBalances.Add(new CalculatedAccountBalance(SubscribedUserIdZeroBalancePaymentInProgress.Value, LedgerAccountType.FifthweekCredit, Now.AddDays(-1), 10));
                calculatedAccountBalances.Add(new CalculatedAccountBalance(SubscribedUserIdZeroBalancePaymentInProgress.Value, LedgerAccountType.FifthweekCredit, Now, 0.8m));
                origins.Add(new UserPaymentOrigin(SubscribedUserIdZeroBalancePaymentInProgress.Value, null, null, default(PaymentOriginKeyType), null, null, null, null, PaymentStatus.Retry1));

                channelSubscriptions.Add(new ChannelSubscription(ChannelId.Value, null, SubscribedLowPriceUserId.Value, null, ChannelPrice / 2, Now, Now));
                calculatedAccountBalances.Add(new CalculatedAccountBalance(SubscribedLowPriceUserId.Value, LedgerAccountType.FifthweekCredit, Now, 10));

                channelSubscriptions.Add(new ChannelSubscription(ChannelId.Value, null, SubscribedHighPriceUserId.Value, null, ChannelPrice * 2, Now, Now));
                calculatedAccountBalances.Add(new CalculatedAccountBalance(SubscribedHighPriceUserId.Value, LedgerAccountType.FifthweekCredit, Now, 10));

                channelSubscriptions.Add(new ChannelSubscription(ChannelId.Value, null, GuestListUserId.Value, null, 0, Now, Now));

                freeAccessUsers.Add(new FreeAccessUser(BlogId.Value, GuestListUserId.Value + "@test.com"));

                await databaseContext.Database.Connection.InsertAsync(channelSubscriptions);
                await databaseContext.Database.Connection.InsertAsync(calculatedAccountBalances);
                await databaseContext.Database.Connection.InsertAsync(freeAccessUsers);
                await databaseContext.Database.Connection.InsertAsync(origins);
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