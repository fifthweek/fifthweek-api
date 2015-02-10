namespace Fifthweek.Api.Availability.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CountUsersDbStatementTests : PersistenceTestsBase
    {
        private Mock<IFifthweekDbContext> databaseContext;
        private CountUsersDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give potentially side-effective components strict mock behaviour.
            this.databaseContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);

            this.InitializeTarget(this.databaseContext.Object);
        }

        public void InitializeTarget(IFifthweekDbContext databaseContext)
        {
            this.target = new CountUsersDbStatement(databaseContext);
        }

        [TestMethod]
        public async Task ItShouldCountUsers()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());

                var result = await this.target.ExecuteAsync();

                Assert.IsTrue(result > 0);

                var random = new Random();
                var user = UserTests.UniqueEntity(random);
                using (var context = testDatabase.NewContext())
                {
                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                }

                await testDatabase.TakeSnapshotAsync();

                var result2 = await this.target.ExecuteAsync();

                Assert.IsTrue(result == result2 - 1);

                return ExpectedSideEffects.None;
            });
        }
    }
}