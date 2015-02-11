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
        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private CountUsersDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give potentially side-effective components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new CountUsersDbStatement(connectionFactory);
        }

        [TestMethod]
        public async Task ItShouldCountUsers()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);

                var result = await this.target.ExecuteAsync();

                Assert.IsTrue(result > 0);

                var random = new Random();
                var user = UserTests.UniqueEntity(random);
                using (var context = testDatabase.CreateContext())
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