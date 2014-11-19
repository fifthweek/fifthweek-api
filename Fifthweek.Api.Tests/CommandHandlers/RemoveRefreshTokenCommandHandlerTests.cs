namespace Fifthweek.Api.Tests.CommandHandlers
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.CommandHandlers;
    using Fifthweek.Api.Commands;
    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Models;
    using Fifthweek.Api.Repositories;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RemoveRefreshTokenCommandHandlerTests
    {
        [TestMethod]
        public async Task ItShouldRemoveTheRefreshTokenAndSave()
        {
            var refreshTokenRepository = new Mock<IRefreshTokenRepository>();
            var saveChanges = new Mock<ICommandHandler<SaveChangesCommand>>();

            refreshTokenRepository.Setup(v => v.TryGetRefreshTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(new RefreshToken());

            var handler = new RemoveRefreshTokenCommandHandler(
                refreshTokenRepository.Object,
                saveChanges.Object);

            await handler.HandleAsync(new RemoveRefreshTokenCommand(new HashedRefreshTokenId("ABC")));

            refreshTokenRepository.Verify(v => v.TryGetRefreshTokenAsync(It.IsAny<string>()));
            refreshTokenRepository.Verify(v => v.RemoveRefreshTokenAsync(It.IsAny<RefreshToken>()));
            saveChanges.Verify(v => v.HandleAsync(It.IsAny<SaveChangesCommand>()));
        }

        [TestMethod]
        public async Task ItShouldThrowAnExceptionIfTheRefreshTokenDoesNotExist()
        {
            var refreshTokenRepository = new Mock<IRefreshTokenRepository>();
            var saveChanges = new Mock<ICommandHandler<SaveChangesCommand>>();

            refreshTokenRepository.Setup(v => v.TryGetRefreshTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(null);

            var handler = new RemoveRefreshTokenCommandHandler(
                refreshTokenRepository.Object,
                saveChanges.Object);

            Exception exception = null;
            try
            {
                await handler.HandleAsync(new RemoveRefreshTokenCommand(new HashedRefreshTokenId("ABC")));
            }
            catch (BadRequestException t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);

            refreshTokenRepository.Verify(v => v.TryGetRefreshTokenAsync(It.IsAny<string>()));
            refreshTokenRepository.Verify(v => v.RemoveRefreshTokenAsync(It.IsAny<RefreshToken>()), Times.Never());
            saveChanges.Verify(v => v.HandleAsync(It.IsAny<SaveChangesCommand>()), Times.Never());
        }
    }
}