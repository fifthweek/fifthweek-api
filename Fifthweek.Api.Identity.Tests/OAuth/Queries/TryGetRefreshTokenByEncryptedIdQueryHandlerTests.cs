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
    public class TryGetRefreshTokenByEncryptedIdQueryHandlerTests
    {
        private static readonly EncryptedRefreshTokenId EncryptedId = new EncryptedRefreshTokenId("encryptedId");
        private static readonly RefreshToken RefreshToken = new RefreshToken { EncryptedId = EncryptedId.Value };

        private TryGetRefreshTokenByEncryptedIdQueryHandler target;
        private Mock<ITryGetRefreshTokenByEncryptedIdDbStatement> tryGetRefreshTokenDbStatement;

        [TestInitialize]
        public void TestInitialize()
        {
            this.tryGetRefreshTokenDbStatement = new Mock<ITryGetRefreshTokenByEncryptedIdDbStatement>(MockBehavior.Strict);
            this.target = new TryGetRefreshTokenByEncryptedIdQueryHandler(this.tryGetRefreshTokenDbStatement.Object);
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
            this.tryGetRefreshTokenDbStatement.Setup(v => v.ExecuteAsync(EncryptedId))
                .ReturnsAsync(RefreshToken);

            var result = await this.target.HandleAsync(new TryGetRefreshTokenByEncryptedIdQuery(EncryptedId));

            Assert.AreEqual(RefreshToken, result);
        }
    }
}