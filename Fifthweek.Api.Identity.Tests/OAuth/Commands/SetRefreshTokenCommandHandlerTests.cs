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
    public class SetRefreshTokenCommandHandlerTests
    {
        private static readonly Username Username = new Username("username");
        private static readonly ClientId ClientId = new ClientId("clientId");
        private static readonly RefreshTokenId RefreshTokenId = new RefreshTokenId("blah");
        private static readonly EncryptedRefreshTokenId EncryptedRefreshTokenId = new EncryptedRefreshTokenId("encrypted");
        private static readonly DateTime IssuedDate = DateTime.UtcNow.AddSeconds(-10);
        private static readonly DateTime ExpiresDate = DateTime.UtcNow;
        private static readonly string ProtectedTicket = "bfdksl";

        private SetRefreshTokenCommandHandler target;
        private Mock<IRefreshTokenIdEncryptionService> encryptionService;
        private Mock<IUpsertRefreshTokenDbStatement> upsertRefreshToken;

        [TestInitialize]
        public void TestInitialize()
        {
            this.encryptionService = new Mock<IRefreshTokenIdEncryptionService>(MockBehavior.Strict);
            this.upsertRefreshToken = new Mock<IUpsertRefreshTokenDbStatement>(MockBehavior.Strict);

            this.target = new SetRefreshTokenCommandHandler(this.encryptionService.Object, this.upsertRefreshToken.Object);
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
            var command = new SetRefreshTokenCommand(
                RefreshTokenId,
                ClientId,
                Username,
                ProtectedTicket,
                IssuedDate,
                ExpiresDate);

            this.encryptionService.Setup(v => v.EncryptRefreshTokenId(RefreshTokenId)).Returns(EncryptedRefreshTokenId);

            this.upsertRefreshToken.Setup(v => v.ExecuteAsync(
                new RefreshToken(Username.Value,
                    ClientId.Value,
                    EncryptedRefreshTokenId.Value,
                    IssuedDate,
                    ExpiresDate, ProtectedTicket))).Returns(Task.FromResult(0)).Verifiable();

            await this.target.HandleAsync(command);

            this.upsertRefreshToken.Verify();
        }
    }
}
