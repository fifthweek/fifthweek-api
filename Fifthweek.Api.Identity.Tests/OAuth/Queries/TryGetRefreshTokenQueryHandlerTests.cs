namespace Fifthweek.Api.Identity.Tests.OAuth.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class TryGetRefreshTokenQueryHandlerTests
    {
        private static readonly ClientId ClientId = new ClientId("clientId");
        private static readonly Username Username = new Username("username");
        private static readonly RefreshToken RefreshToken = new RefreshToken { ClientId = ClientId.Value, Username = Username.Value };

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
            this.tryGetRefreshTokenDbStatement.Setup(v => v.ExecuteAsync(ClientId, Username))
                .ReturnsAsync(RefreshToken);

            var result = await this.target.HandleAsync(new TryGetRefreshTokenQuery(ClientId, Username));

            Assert.AreEqual(RefreshToken, result);
        }
    }
}