namespace Fifthweek.Api.Identity.Tests.OAuth
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class TryGetRefreshTokenByEncryptedIdDbStatementTests : PersistenceTestsBase
    {
        private static readonly EncryptedRefreshTokenId EncryptedId = new EncryptedRefreshTokenId("encryptedId");

        private static readonly RefreshToken RefreshToken = new RefreshToken(
            "username",
            "clientId",
            EncryptedId.Value,
            new SqlDateTime(DateTime.UtcNow.AddSeconds(-100)).Value,
            new SqlDateTime(DateTime.UtcNow).Value,
            "protectedTicket");

        private TryGetRefreshTokenByEncryptedIdDbStatement target;
        private Mock<IFifthweekDbConnectionFactory> connectionFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);
            this.target = new TryGetRefreshTokenByEncryptedIdDbStatement(this.connectionFactory.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenEncryptedIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task WhenRefreshTokenIsValidAndExists_ItShouldReturnTheRefreshToken()
        {
            await this.DatabaseTestAsync(async testDatabase =>
                {
                    this.target = new TryGetRefreshTokenByEncryptedIdDbStatement(testDatabase);

                    await InsertRefreshToken(testDatabase);
                    await testDatabase.TakeSnapshotAsync();

                    var result = await this.target.ExecuteAsync(EncryptedId);

                    Assert.AreEqual(RefreshToken, result);

                    return ExpectedSideEffects.None;
                });
        }

        [TestMethod]
        public async Task WhenRefreshTokenDoesNotExist_ItShouldReturnNull()
        {
            await this.DatabaseTestAsync(async testDatabase =>
                {
                    this.target = new TryGetRefreshTokenByEncryptedIdDbStatement(testDatabase);

                    await testDatabase.TakeSnapshotAsync();

                    var result = await this.target.ExecuteAsync(new EncryptedRefreshTokenId("abc"));

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