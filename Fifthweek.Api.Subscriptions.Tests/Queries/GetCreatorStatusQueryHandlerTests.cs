using System;
using System.Threading.Tasks;
using Dapper;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Persistence;
using Fifthweek.Api.Persistence.Tests.Shared;
using Fifthweek.Api.Subscriptions.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Subscriptions.Tests.Queries
{
    [TestClass]
    public class GetCreatorStatusQueryHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());
        private static readonly GetCreatorStatusQuery Query = new GetCreatorStatusQuery(UserId);
        private GetCreatorStatusQueryHandler target;

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            this.target = new GetCreatorStatusQueryHandler(this.NewDbContext());
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public async Task WhenAtLeastOneSubscriptionMatchesCreator_ItShouldReturnThatSubscriptionId()
        {
            await this.CreateSubscriptionAsync(UserId, SubscriptionId);
            await this.SnapshotDatabaseAsync();

            var result = await this.target.HandleAsync(Query);

            Assert.AreEqual(result.SubscriptionId, SubscriptionId);
            Assert.IsTrue(result.MustWriteFirstPost);

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenMultipleSubscriptionsMatchCreator_ItShouldReturnTheLatestSubscriptionId()
        {
            await this.CreateSubscriptionAsync(UserId, new SubscriptionId(Guid.NewGuid()));
            await this.CreateSubscriptionsAsync(UserId, 100);
            await this.CreateSubscriptionAsync(UserId, SubscriptionId, false, true);
            await this.SnapshotDatabaseAsync();

            var result = await this.target.HandleAsync(Query);

            Assert.AreEqual(result.SubscriptionId, SubscriptionId);
            Assert.IsTrue(result.MustWriteFirstPost);

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenNoSubscriptionsExist_ItShouldReturnEmptySubscriptionId()
        {
            using (var dbContext = this.NewDbContext())
            {
                await dbContext.Database.Connection.ExecuteAsync("DELETE FROM Subscriptions");
            }

            await this.SnapshotDatabaseAsync();

            var result = await this.target.HandleAsync(Query);

            Assert.IsNull(result.SubscriptionId);
            Assert.IsFalse(result.MustWriteFirstPost);

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenNoSubscriptionsMatchCreator_ItShouldReturnEmptySubscriptionId()
        {
            await this.SnapshotDatabaseAsync();

            var result = await this.target.HandleAsync(Query);

            Assert.IsNull(result.SubscriptionId);
            Assert.IsFalse(result.MustWriteFirstPost);

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        private async Task CreateSubscriptionsAsync(UserId newUserId, int subscriptions)
        {
            for (var i = 0; i < subscriptions; i++)
            {
                await this.CreateSubscriptionAsync(newUserId, new SubscriptionId(Guid.NewGuid()), false);
            }
        }

        private async Task CreateSubscriptionAsync(UserId newUserId, SubscriptionId newSubscriptionId, bool newUser = true, bool setTodaysDate = false)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId.Value;

            var subscription = SubscriptionTests.UniqueEntity(random);
            subscription.Id = newSubscriptionId.Value;
            subscription.CreatorId = creator.Id;
            subscription.HeaderImageFileId = null;

            if (newUser)
            {
                subscription.Creator = creator;
            }
            else
            {
                subscription.Creator = null; // Set by helper method.
            }

            if (setTodaysDate)
            {
                subscription.CreationDate = DateTime.UtcNow;
            }

            using (var dbContext = this.NewDbContext())
            {
                if (newUser)
                {
                    dbContext.Users.Add(creator);
                    await dbContext.SaveChangesAsync();
                }

                // Work around EF (in)validation
                await dbContext.Database.Connection.InsertAsync(subscription, false);
            }
        }
    }
}