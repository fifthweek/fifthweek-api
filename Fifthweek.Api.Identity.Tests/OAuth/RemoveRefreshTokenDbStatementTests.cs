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
    public class RemoveRefreshTokenDbStatementTests : PersistenceTestsBase
    {
        private static readonly RefreshToken RefreshToken = new RefreshToken(
            "hashedId",
            "username",
            "clientId",
            new SqlDateTime(DateTime.UtcNow.AddSeconds(-100)).Value,
            new SqlDateTime(DateTime.UtcNow).Value,
            "protectedTicket");

        private RemoveRefreshTokenDbStatement target;
        private Mock<IFifthweekDbConnectionFactory> connectionFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);
            this.target = new RemoveRefreshTokenDbStatement(this.connectionFactory.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenRefreshTokenIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task WhenRefreshTokenIsValidAndExists_ItShouldDeleteTheRecordFromTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new RemoveRefreshTokenDbStatement(testDatabase);

                await InsertRefreshToken(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(new HashedRefreshTokenId(RefreshToken.HashedId));

                return new ExpectedSideEffects
                {
                    Delete = RefreshToken
                };
            });
        }

        [TestMethod]
        public async Task WhenRefreshTokenIsValidAndNew_ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new RemoveRefreshTokenDbStatement(testDatabase);

                await InsertRefreshToken(testDatabase);
                await this.target.ExecuteAsync(new HashedRefreshTokenId(RefreshToken.HashedId));

                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(new HashedRefreshTokenId(RefreshToken.HashedId));

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenRefreshTokenIsValidAndDoesNotExist_ItShouldNotUpdateTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new RemoveRefreshTokenDbStatement(testDatabase);

                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(new HashedRefreshTokenId(RefreshToken.HashedId));

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenRefreshTokenIsValidAndExists_ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new RemoveRefreshTokenDbStatement(testDatabase);

                await this.target.ExecuteAsync(new HashedRefreshTokenId(RefreshToken.HashedId));

                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(new HashedRefreshTokenId(RefreshToken.HashedId));

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