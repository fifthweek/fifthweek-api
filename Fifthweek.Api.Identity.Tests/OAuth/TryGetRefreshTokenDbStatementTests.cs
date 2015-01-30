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
    public class TryGetRefreshTokenDbStatementTests : PersistenceTestsBase
    {
        private static readonly RefreshToken RefreshToken = new RefreshToken(
            "hashedId",
            "username",
            "clientId",
            new SqlDateTime(DateTime.UtcNow.AddSeconds(-100)).Value,
            new SqlDateTime(DateTime.UtcNow).Value,
            "protectedTicket");

        private TryGetRefreshTokenDbStatement target;
        private Mock<IFifthweekDbContext> fifthweekDbContext;

        [TestInitialize]
        public void TestInitialize()
        {
            this.fifthweekDbContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);
            this.target = new TryGetRefreshTokenDbStatement(this.fifthweekDbContext.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenRefreshTokenIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task WhenRefreshTokenIsValidAndExists_ItShouldReturnTheRefreshToken()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new TryGetRefreshTokenDbStatement(testDatabase.NewContext());

                await InsertRefreshToken(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(new HashedRefreshTokenId(RefreshToken.HashedId));

                Assert.AreEqual(RefreshToken, result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenRefreshTokenIsValidAndDoesNotExist_ItShouldReturnTheRefreshToken()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new TryGetRefreshTokenDbStatement(testDatabase.NewContext());

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(new HashedRefreshTokenId(RefreshToken.HashedId));

                Assert.IsNull(result);

                return ExpectedSideEffects.None;
            });
        }

        private static async Task InsertRefreshToken(TestDatabaseContext testDatabase)
        {
            using (var context = testDatabase.NewContext())
            {
                await context.Database.Connection.InsertAsync(RefreshToken);
            }
        }
    }
}