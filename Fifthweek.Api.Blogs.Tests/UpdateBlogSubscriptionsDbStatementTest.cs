namespace Fifthweek.Api.Blogs.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateBlogSubscriptionsDbStatementTest : PersistenceTestsBase
    {
        private static readonly BlogId Blog1Id = new BlogId(Guid.NewGuid());
        private static readonly ChannelId[] Blog1ChannelIds = new[] { new ChannelId(Guid.NewGuid()), new ChannelId(Guid.NewGuid()) };
        private static readonly BlogId Blog2Id = new BlogId(Guid.NewGuid());
        private static readonly ChannelId[] Blog2ChannelIds = new[] { new ChannelId(Guid.NewGuid()) };
        private static readonly BlogId Blog3Id = new BlogId(Guid.NewGuid());
        private static readonly ChannelId[] Blog3ChannelIds = new[] { new ChannelId(Guid.NewGuid()), new ChannelId(Guid.NewGuid()), new ChannelId(Guid.NewGuid()) };
        private static readonly UserId Creator1Id = new UserId(Guid.NewGuid());
        private static readonly UserId Creator2Id = new UserId(Guid.NewGuid());
        private static readonly UserId Creator3Id = new UserId(Guid.NewGuid());
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Username Username = new Username("usertwo");
        private static readonly Email UserEmail = new Email("two@test.com");
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly DateTime PriceLastAcceptedDate = new SqlDateTime(DateTime.UtcNow.AddDays(-8)).Value;
        private static readonly DateTime PriceLastSetDate = new SqlDateTime(DateTime.UtcNow.AddDays(-9)).Value;
        private static readonly DateTime SubscriptionStartDate = new SqlDateTime(DateTime.UtcNow.AddDays(-10)).Value;
        private static readonly int ChannelPrice = 10;

        private static readonly List<AcceptedChannelSubscription> AcceptedBlog1Subscriptions1 =
            new List<AcceptedChannelSubscription> 
            { 
                new AcceptedChannelSubscription(Blog1ChannelIds[0], ValidAcceptedChannelPriceInUsCentsPerWeek.Parse(ChannelPrice)),
            };

        private static readonly List<AcceptedChannelSubscription> AcceptedBlog3Subscriptions1 =
            new List<AcceptedChannelSubscription> 
            { 
                new AcceptedChannelSubscription(Blog3ChannelIds[0], ValidAcceptedChannelPriceInUsCentsPerWeek.Parse(ChannelPrice)),
                new AcceptedChannelSubscription(Blog3ChannelIds[1], ValidAcceptedChannelPriceInUsCentsPerWeek.Parse(ChannelPrice)),
            };

        private static readonly List<AcceptedChannelSubscription> AcceptedBlog3Subscriptions2 =
            new List<AcceptedChannelSubscription> 
            { 
                new AcceptedChannelSubscription(Blog3ChannelIds[0], ValidAcceptedChannelPriceInUsCentsPerWeek.Parse(ChannelPrice + 5)),
                new AcceptedChannelSubscription(Blog3ChannelIds[2], ValidAcceptedChannelPriceInUsCentsPerWeek.Parse(ChannelPrice)),
            };

        private readonly Random random = new Random();

        private UpdateBlogSubscriptionsDbStatement target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.target = new UpdateBlogSubscriptionsDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, Blog1Id, AcceptedBlog1Subscriptions1, Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenBlogIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(UserId, null, AcceptedBlog1Subscriptions1, Now);
        }

        [TestMethod]
        public async Task WhenTheSubscriptionHasNotChanged_ItShouldUpdatePriceLastAcceptedDate()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateBlogSubscriptionsDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(UserId, Blog3Id, AcceptedBlog3Subscriptions1, Now);

                var expectedResults =
                    AcceptedBlog3Subscriptions1.Select(
                        v => new ChannelSubscription(
                            v.ChannelId.Value,
                            null,
                            UserId.Value,
                            null,
                            v.AcceptedPrice.Value,
                            Now,
                            SubscriptionStartDate)).ToList();

                return new ExpectedSideEffects { Updates = expectedResults };
            });
        }

        [TestMethod]
        public async Task WhenTheSubscriptionHasChangedChanged_ItShouldUpdateTheSubscription()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateBlogSubscriptionsDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(UserId, Blog3Id, AcceptedBlog3Subscriptions2, Now);

                var expectedUpdate = new ChannelSubscription(
                    AcceptedBlog3Subscriptions2[0].ChannelId.Value,
                    null,
                    UserId.Value,
                    null,
                    AcceptedBlog3Subscriptions2[0].AcceptedPrice.Value,
                    Now,
                    SubscriptionStartDate);

                var expectedInsert = new ChannelSubscription(
                    AcceptedBlog3Subscriptions2[1].ChannelId.Value,
                    null,
                    UserId.Value,
                    null,
                    AcceptedBlog3Subscriptions2[1].AcceptedPrice.Value,
                    Now,
                    Now);

                var expectedDelete = new ChannelSubscription(
                    AcceptedBlog3Subscriptions1[1].ChannelId.Value,
                    null,
                    UserId.Value,
                    null,
                    AcceptedBlog3Subscriptions1[1].AcceptedPrice.Value,
                    PriceLastAcceptedDate,
                    SubscriptionStartDate);

                return new ExpectedSideEffects 
                {
                    Update = expectedUpdate,
                    Insert = expectedInsert,
                    Delete = expectedDelete
                };
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
                foreach (var item in AcceptedBlog1Subscriptions1.Concat(AcceptedBlog3Subscriptions1))
                {
                    await
                        connection.InsertAsync(
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
                var blog1Channels =
                    await
                    databaseContext.CreateTestChannelsAsync(
                        Creator1Id.Value,
                        Blog1Id.Value,
                        Blog1ChannelIds.Select(v => v.Value).ToArray());

                var blog2Channels =
                    await
                    databaseContext.CreateTestChannelsAsync(
                        Creator2Id.Value,
                        Blog2Id.Value,
                        Blog2ChannelIds.Select(v => v.Value).ToArray());

                var blog3Channels =
                    await
                    databaseContext.CreateTestChannelsAsync(
                        Creator3Id.Value,
                        Blog3Id.Value,
                        Blog3ChannelIds.Select(v => v.Value).ToArray());

                blog2Channels[0].PriceInUsCentsPerWeek = ChannelPrice;
                blog2Channels[0].PriceLastSetDate = PriceLastSetDate;

                blog3Channels[0].PriceInUsCentsPerWeek = ChannelPrice;
                blog3Channels[0].PriceLastSetDate = PriceLastSetDate;
                blog3Channels[2].PriceInUsCentsPerWeek = ChannelPrice;
                blog3Channels[2].PriceLastSetDate = PriceLastSetDate;

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