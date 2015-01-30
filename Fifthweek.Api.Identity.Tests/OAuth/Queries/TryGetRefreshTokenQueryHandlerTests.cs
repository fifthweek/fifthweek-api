namespace Fifthweek.Api.Identity.Tests.OAuth.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Persistence;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class TryGetRefreshTokenQueryHandlerTests
    {
        private static readonly HashedRefreshTokenId HashedRefreshTokenId = new HashedRefreshTokenId("h");
        private static readonly RefreshToken RefreshToken = new RefreshToken { HashedId = HashedRefreshTokenId.Value };

        private TryGetRefreshTokenQueryHandler target;
        private Mock<ITryGetRefreshTokenDbStatement> tryGetRefreshTokenDbStatement;

        [TestInitialize]
        public void TestInitialize()
        {
            this.tryGetRefreshTokenDbStatement = new Mock<ITryGetRefreshTokenDbStatement>(MockBehavior.Strict);
            this.target = new TryGetRefreshTokenQueryHandler(this.tryGetRefreshTokenDbStatement.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenQueryIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        public async Task WhenQueryIsValid_ItShouldReturnTheRefreshToken()
        {
            this.tryGetRefreshTokenDbStatement.Setup(v => v.ExecuteAsync(HashedRefreshTokenId))
                .ReturnsAsync(RefreshToken);

            var result = await this.target.HandleAsync(new TryGetRefreshTokenQuery(HashedRefreshTokenId));

            Assert.AreEqual(RefreshToken, result);
        }
    }
}