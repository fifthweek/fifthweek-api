namespace Fifthweek.Api.Identity.Tests.OAuth
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class TryGetRefreshTokenDbStatementTests : PersistenceTestsBase
    {
        private static readonly ClientId ClientId = new ClientId("clientId");
        private static readonly Username Username = new Username("username");
        private static readonly RefreshToken RefreshToken = new RefreshToken(
            Username.Value,
            ClientId.Value,
            "hashedId",
            new SqlDateTime(DateTime.UtcNow.AddSeconds(-100)).Value,
            new SqlDateTime(DateTime.UtcNow).Value,
            "protectedTicket");

        private TryGetRefreshTokenDbStatement target;
        private Mock<IFifthweekDbConnectionFactory> connectionFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);
            this.target = new TryGetRefreshTokenDbStatement(this.connectionFactory.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenClientIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, Username);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUsernameIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(ClientId, null);
        }

        [TestMethod]
        public async Task WhenRefreshTokenIsValidAndExists_ItShouldReturnTheRefreshToken()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new TryGetRefreshTokenDbStatement(testDatabase);

                await InsertRefreshToken(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(ClientId, Username);

                Assert.AreEqual(RefreshToken, result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenRefreshTokenDoesNotExist_ItShouldReturnNull()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new TryGetRefreshTokenDbStatement(testDatabase);

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(ClientId, new Username("abc"));

                Assert.IsNull(result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenRefreshTokenDoesNotExist_ItShouldReturnNull2()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new TryGetRefreshTokenDbStatement(testDatabase);

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(new ClientId("abc"), Username);

                Assert.IsNull(result);

                return ExpectedSideEffects.None;
            });
        }

        private static async Task InsertRefreshToken(TestDatabaseContext testDatabase)
        {
            using (var context = testDatabase.CreateContext())
            {
                await context.Database.Connection.InsertAsync(RefreshToken);
            }
        }
    }
}