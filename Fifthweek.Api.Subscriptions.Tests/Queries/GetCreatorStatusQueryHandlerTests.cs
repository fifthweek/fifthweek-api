namespace Fifthweek.Api.Subscriptions.Tests.Queries
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Subscriptions.Queries;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetCreatorStatusQueryHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());
        private static readonly GetCreatorStatusQuery Query = new GetCreatorStatusQuery(Requester.Authenticated(UserId));
        private GetCreatorStatusQueryHandler target;

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            var databaseContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);

            this.target = new GetCreatorStatusQueryHandler(databaseContext.Object);

            await this.target.HandleAsync(new GetCreatorStatusQuery(Requester.Unauthenticated));
        }

        [TestMethod]
        public async Task WhenAtLeastOneSubscriptionMatchesCreator_ItShouldReturnThatSubscriptionId()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new GetCreatorStatusQueryHandler(testDatabase.NewContext());
                await this.CreateSubscriptionAsync(UserId, SubscriptionId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.HandleAsync(Query);

                Assert.AreEqual(result.SubscriptionId, SubscriptionId);
                Assert.IsTrue(result.MustWriteFirstPost);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenMultipleSubscriptionsMatchCreator_ItShouldReturnTheLatestSubscriptionId()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new GetCreatorStatusQueryHandler(testDatabase.NewContext());
                await this.CreateSubscriptionAsync(UserId, new SubscriptionId(Guid.NewGuid()), testDatabase);
                await this.CreateSubscriptionsAsync(UserId, 100, testDatabase);
                await this.CreateSubscriptionAsync(UserId, SubscriptionId, testDatabase, false, true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.HandleAsync(Query);

                Assert.AreEqual(result.SubscriptionId, SubscriptionId);
                Assert.IsTrue(result.MustWriteFirstPost);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenNoSubscriptionsExist_ItShouldReturnEmptySubscriptionId()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new GetCreatorStatusQueryHandler(testDatabase.NewContext());

                using (var databaseContext = testDatabase.NewContext())
                {
                    await databaseContext.Database.Connection.ExecuteAsync("DELETE FROM Subscriptions");
                }

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.HandleAsync(Query);

                Assert.IsNull(result.SubscriptionId);
                Assert.IsFalse(result.MustWriteFirstPost);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenNoSubscriptionsMatchCreator_ItShouldReturnEmptySubscriptionId()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new GetCreatorStatusQueryHandler(testDatabase.NewContext());
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.HandleAsync(Query);

                Assert.IsNull(result.SubscriptionId);
                Assert.IsFalse(result.MustWriteFirstPost);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateSubscriptionsAsync(UserId newUserId, int subscriptions, TestDatabaseContext testDatabase)
        {
            for (var i = 0; i < subscriptions; i++)
            {
                await this.CreateSubscriptionAsync(newUserId, new SubscriptionId(Guid.NewGuid()), testDatabase, false);
            }
        }

        private async Task CreateSubscriptionAsync(UserId newUserId, SubscriptionId newSubscriptionId, TestDatabaseContext testDatabase, bool newUser = true, bool setTodaysDate = false)
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

            using (var databaseContext = testDatabase.NewContext())
            {
                if (newUser)
                {
                    databaseContext.Users.Add(creator);
                    await databaseContext.SaveChangesAsync();
                }

                // Work around EF (in)validation
                await databaseContext.Database.Connection.InsertAsync(subscription, false);
            }
        }
    }
}