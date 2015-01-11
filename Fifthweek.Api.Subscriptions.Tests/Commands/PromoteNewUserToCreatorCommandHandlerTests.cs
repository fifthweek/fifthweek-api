using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Fifthweek.Api.Core;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Persistence;
using Fifthweek.Api.Persistence.Tests.Shared;
using Fifthweek.Api.Subscriptions.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Fifthweek.Api.Subscriptions.Tests.Commands
{
    [TestClass]
    public class PromoteNewUserToCreatorCommandHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly SubscriptionId DefaultSubscriptionId = new SubscriptionId(UserId.Value); // The default subscription uses the creator ID.
        private static readonly PromoteNewUserToCreatorCommand Command = new PromoteNewUserToCreatorCommand(UserId);
        private PromoteNewUserToCreatorCommandHandler target;

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            this.target = new PromoteNewUserToCreatorCommandHandler(this.NewDbContext());
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public async Task WhenReRun_ItShouldHaveNoEffect()
        {
            await this.CreateUserAsync(UserId);
            await this.target.HandleAsync(Command);

            await this.SnapshotDatabaseAsync();

            await this.target.HandleAsync(Command);

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task ItShouldCreateTheDefaultSubscription()
        {
            await this.CreateUserAsync(UserId);

            await this.SnapshotDatabaseAsync();

            await this.target.HandleAsync(Command);

            var expectedSubscription = new Subscription(
                DefaultSubscriptionId.Value, 
                null,
                UserId.Value,
                null,
                null,
                DateTime.MinValue);

            await this.AssertDatabaseAsync(new ExpectedSideEffects
            {
                Insert = new WildcardEntity<Subscription>(expectedSubscription)
                {
                    AreEqual = actualSubscription =>
                    {
                        expectedSubscription.CreationDate = actualSubscription.CreationDate; // Take wildcard properties from actual value.
                        return Equals(expectedSubscription, actualSubscription);
                    }
                },
                ExcludedFromTest = entity => entity is Subscription
            });
        }

        private async Task CreateUserAsync(UserId newUserId)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId.Value;

            using (var dbContext = this.NewDbContext())
            {
                dbContext.Users.Add(creator);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}