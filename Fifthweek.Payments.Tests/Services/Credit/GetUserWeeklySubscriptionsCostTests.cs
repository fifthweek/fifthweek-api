namespace Fifthweek.Payments.Tests.Services.Credit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.Services.Credit;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetUserWeeklySubscriptionsCostTests : PersistenceTestsBase
    {
        private static readonly UserId CreatorId = UserId.Random();
        private static readonly UserId SubscriberId = UserId.Random();

        private GetUserWeeklySubscriptionsCost target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new GetUserWeeklySubscriptionsCost(new Mock<FifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task ItShouldReturnTheSumOfSubscribedChannels()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetUserWeeklySubscriptionsCost(testDatabase);
                var expectedResult = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(SubscriberId);

                Assert.AreEqual(expectedResult, result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenNoSubscribedChannels_ItShouldReturnTheSumOfSubscribedChannels()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetUserWeeklySubscriptionsCost(testDatabase);
                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CreatorId);

                Assert.AreEqual(0, result);

                return ExpectedSideEffects.None;
            });
        }

        private async Task<int> CreateDataAsync(TestDatabaseContext testDatabase)
        {
            IReadOnlyList<Channel> channels;
            using (var context = testDatabase.CreateContext())
            {
                channels = await context.CreateTestChannelsAsync(CreatorId.Value, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
                await context.CreateTestUserAsync(SubscriberId.Value);
            }

            using (var connection = testDatabase.CreateConnection())
            {
                foreach (var channel in channels)
                {
                    await connection.InsertAsync(new ChannelSubscription(channel.Id, null, SubscriberId.Value, null, channel.PriceInUsCentsPerWeek / 2, DateTime.UtcNow, DateTime.UtcNow));
                }
            }

            return channels.Sum(v => v.PriceInUsCentsPerWeek / 2);
        }
    }
}