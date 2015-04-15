namespace Fifthweek.Api.Blogs.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class AcceptChannelSubscriptionPriceChangeDbStatementTests : PersistenceTestsBase
    {
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly ChannelId[] ChannelIds = new[] { new ChannelId(Guid.NewGuid()), new ChannelId(Guid.NewGuid()), new ChannelId(Guid.NewGuid()) };
        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Username Username = new Username("usertwo");
        private static readonly Email UserEmail = new Email("two@test.com");
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly DateTime PriceLastAcceptedDate = new SqlDateTime(DateTime.UtcNow.AddDays(-8)).Value;
        private static readonly DateTime SubscriptionStartDate = new SqlDateTime(DateTime.UtcNow.AddDays(-10)).Value;
        private static readonly ValidAcceptedChannelPriceInUsCentsPerWeek ChannelPrice = ValidAcceptedChannelPriceInUsCentsPerWeek.Parse(10);
        private static readonly ValidAcceptedChannelPriceInUsCentsPerWeek NewAcceptedPrice = ValidAcceptedChannelPriceInUsCentsPerWeek.Parse(15);

        private static readonly List<AcceptedChannelSubscription> Subscriptions =
            new List<AcceptedChannelSubscription> 
            { 
                new AcceptedChannelSubscription(ChannelIds[0], ChannelPrice),
                new AcceptedChannelSubscription(ChannelIds[1], ChannelPrice),
            };

        private readonly Random random = new Random();

        private AcceptChannelSubscriptionPriceChangeDbStatement target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.target = new AcceptChannelSubscriptionPriceChangeDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, ChannelIds[0], ChannelPrice, Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenChannelIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(UserId, null, ChannelPrice, Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAcceptedPriceIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(UserId, ChannelIds[0], null, Now);
        }

        [TestMethod]
        public async Task WhenTheSubscriptionExists_ItShouldUpdateTheSubscription()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new AcceptChannelSubscriptionPriceChangeDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(UserId, Subscriptions[0].ChannelId, NewAcceptedPrice, Now);

                var update = new ChannelSubscription(
                            Subscriptions[0].ChannelId.Value,
                            null,
                            UserId.Value,
                            null,
                            NewAcceptedPrice.Value,
                            Now,
                            SubscriptionStartDate);

                return new ExpectedSideEffects { Update = update };
            });
        }

        [TestMethod]
        public async Task WhenTheChannelDoesNotExist_ItShouldDoNothing()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new AcceptChannelSubscriptionPriceChangeDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(UserId, new ChannelId(Guid.NewGuid()), NewAcceptedPrice, Now);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenTheUserDoesNotExist_ItShouldDoNothing()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new AcceptChannelSubscriptionPriceChangeDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(new UserId(Guid.NewGuid()), Subscriptions[0].ChannelId, NewAcceptedPrice, Now);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateDataAsync(TestDatabaseContext testDatabase)
        {
            await this.CreateChannelsAsync(testDatabase);
            await this.CreateUserAsync(testDatabase);
            await this.CreateSubscriptionsAsync(testDatabase);
        }

        private async Task CreateSubscriptionsAsync(TestDatabaseContext testDatabase)
        {
            using (var connection = testDatabase.CreateConnection())
            {
                foreach (var item in Subscriptions)
                {
                    await connection.InsertAsync(
                            new ChannelSubscription(
                                item.ChannelId.Value,
                                null,
                                UserId.Value,
                                null,
                                item.AcceptedPrice.Value,
                                PriceLastAcceptedDate,
                                SubscriptionStartDate));
                }
            }
        }

        private async Task CreateChannelsAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestChannelsAsync(
                        CreatorId.Value,
                        BlogId.Value,
                        ChannelIds.Select(v => v.Value).ToArray());

                await databaseContext.SaveChangesAsync();
            }
        }

        private async Task CreateUserAsync(TestDatabaseContext testDatabase)
        {
            var user = UserTests.UniqueEntity(this.random);
            user.Id = UserId.Value;
            user.Email = UserEmail.Value;
            user.UserName = Username.Value;

            using (var databaseContext = testDatabase.CreateContext())
            {
                databaseContext.Users.Add(user);
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}