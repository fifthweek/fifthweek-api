namespace Fifthweek.Api.Identity.Tests.OAuth.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CreateRefreshTokenCommandHandlerTests
    {
        private static readonly Username Username = new Username("username");
        private static readonly ClientId ClientId = new ClientId("clientId");
        private static readonly RefreshTokenId RefreshTokenId = new RefreshTokenId("blah");
        private static readonly HashedRefreshTokenId HashedRefreshTokenId = HashedRefreshTokenId.FromRefreshTokenId(RefreshTokenId);
        private static readonly DateTime IssuedDate = DateTime.UtcNow.AddSeconds(-10);
        private static readonly DateTime ExpiresDate = DateTime.UtcNow;
        private static readonly string ProtectedTicket = "bfdksl";

        private CreateRefreshTokenCommandHandler target;
        private Mock<IUpsertRefreshTokenDbStatement> upsertRefreshToken;

        [TestInitialize]
        public void TestInitialize()
        {
            this.upsertRefreshToken = new Mock<IUpsertRefreshTokenDbStatement>(MockBehavior.Strict);
            this.target = new CreateRefreshTokenCommandHandler(this.upsertRefreshToken.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCommandIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        public async Task WhenCommandIsValid_ItShouldCallTheDbStatementWithAToken()
        {
            var command = new CreateRefreshTokenCommand(
                RefreshTokenId,
                ClientId,
                Username,
                ProtectedTicket,
                IssuedDate,
                ExpiresDate);

            this.upsertRefreshToken.Setup(v => v.ExecuteAsync(
                new RefreshToken(
                    HashedRefreshTokenId.Value,
                    Username.Value,
                    ClientId.Value,
                    IssuedDate,
                    ExpiresDate,
                    ProtectedTicket))).Returns(Task.FromResult(0)).Verifiable();

            await this.target.HandleAsync(command);

            this.upsertRefreshToken.Verify();
        }
    }
}
