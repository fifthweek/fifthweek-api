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
    public class UpsertRefreshTokenDbStatementTests : PersistenceTestsBase
    {
        private static readonly RefreshToken RefreshToken = new RefreshToken(
            "hashedId",
            "username",
            "clientId",
            new SqlDateTime(DateTime.UtcNow.AddSeconds(-100)).Value,
            new SqlDateTime(DateTime.UtcNow).Value,
            "protectedTicket");

        private UpsertRefreshTokenDbStatement target;
        private Mock<IFifthweekDbContext> fifthweekDbContext;

        [TestInitialize]
        public void TestInitialize()
        {
            this.fifthweekDbContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);
            this.target = new UpsertRefreshTokenDbStatement(this.fifthweekDbContext.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenRefreshTokenIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task WhenRefreshTokenIsValidAndNew_ItShouldInsertARecordInTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpsertRefreshTokenDbStatement(testDatabase.NewContext());

                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(RefreshToken);

                return new ExpectedSideEffects
                {
                    Insert = RefreshToken
                };
            });
        }

        [TestMethod]
        public async Task WhenRefreshTokenIsValidAndNew_ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpsertRefreshTokenDbStatement(testDatabase.NewContext());

                await this.target.ExecuteAsync(RefreshToken);

                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(RefreshToken);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenRefreshTokenIsValidAndExists_ItShouldUpdateARecordInTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpsertRefreshTokenDbStatement(testDatabase.NewContext());

                await InsertRefreshToken(testDatabase);

                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(RefreshToken);

                return new ExpectedSideEffects
                {
                    Update = RefreshToken
                };
            });
        }

        [TestMethod]
        public async Task WhenRefreshTokenIsValidAndExists_ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpsertRefreshTokenDbStatement(testDatabase.NewContext());

                await InsertRefreshToken(testDatabase);
                await this.target.ExecuteAsync(RefreshToken);

                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(RefreshToken);

                return ExpectedSideEffects.None;
            });
        }

        private static async Task InsertRefreshToken(TestDatabaseContext testDatabase)
        {
            using (var context = testDatabase.NewContext())
            {
                await context.Database.Connection.InsertAsync(
                    new RefreshToken(
                        RefreshToken.HashedId,
                        RefreshToken.Username,
                        RefreshToken.ClientId,
                        RefreshToken.IssuedDate.AddDays(-1),
                        RefreshToken.ExpiresDate.AddDays(-1),
                        RefreshToken.ProtectedTicket + "2"));
            }
        }
    }
}