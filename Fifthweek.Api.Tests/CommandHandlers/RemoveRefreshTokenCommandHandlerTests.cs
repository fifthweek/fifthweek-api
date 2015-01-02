namespace Fifthweek.Api.Tests.CommandHandlers
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RemoveRefreshTokenCommandHandlerTests
    {
        [TestMethod]
        public async Task ItShouldRemoveTheRefreshToken()
        {
            var command = new RemoveRefreshTokenCommand(new HashedRefreshTokenId("ABC"));

            var refreshTokenRepository = new Mock<IRefreshTokenRepository>();

            refreshTokenRepository.Setup(v => v.RemoveRefreshToken(command.HashedRefreshTokenId.Value))
                .Returns(Task.FromResult(0)).Verifiable();

            var handler = new RemoveRefreshTokenCommandHandler(
                refreshTokenRepository.Object);

            await handler.HandleAsync(command);

            refreshTokenRepository.Verify();
        }
    }
}