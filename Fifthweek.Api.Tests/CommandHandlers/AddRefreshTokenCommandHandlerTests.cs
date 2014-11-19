namespace Fifthweek.Api.Tests.CommandHandlers
{
    using System.Threading.Tasks;

    using Fifthweek.Api.CommandHandlers;
    using Fifthweek.Api.Commands;
    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Repositories;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class AddRefreshTokenCommandHandlerTests
    {
        [TestMethod]
        public async Task WhenNoExistingTokenItShouldAddNewTokenAndSave()
        {
            var refreshTokenRepository = new Mock<IRefreshTokenRepository>();
            var saveChanges = new Mock<ICommandHandler<SaveChangesCommand>>();
            var removeRefreshToken = new Mock<ICommandHandler<RemoveRefreshTokenCommand>>();

            refreshTokenRepository.Setup(v => v.TryGetRefreshTokenAsync("Username", "ClientId"))
                .ReturnsAsync(null);

            var addrefreshTokenCommandHandler = new AddRefreshTokenCommandHandler(
                refreshTokenRepository.Object,
                saveChanges.Object,
                removeRefreshToken.Object);

            var command = new AddRefreshTokenCommand(new RefreshToken
            {
                HashedId = "HashedId",
                Username = "Username",
                ClientId = "ClientId",
            });

            await addrefreshTokenCommandHandler.HandleAsync(command);

            refreshTokenRepository.Verify(v => v.TryGetRefreshTokenAsync("Username", "ClientId"));
            removeRefreshToken.Verify(v => v.HandleAsync(It.IsAny<RemoveRefreshTokenCommand>()), Times.Never());
            refreshTokenRepository.Verify(v => v.AddRefreshTokenAsync(command.RefreshToken));
            saveChanges.Verify(v => v.HandleAsync(It.IsAny<SaveChangesCommand>()));
        }

        [TestMethod]
        public async Task WhenExistingTokenItShouldRemoveExistingAndAddNewTokenAndSave()
        {
            var refreshTokenRepository = new Mock<IRefreshTokenRepository>();
            var saveChanges = new Mock<ICommandHandler<SaveChangesCommand>>();
            var removeRefreshToken = new Mock<ICommandHandler<RemoveRefreshTokenCommand>>();

            refreshTokenRepository.Setup(v => v.TryGetRefreshTokenAsync("Username", "ClientId"))
                .ReturnsAsync(new RefreshToken());

            var addrefreshTokenCommandHandler = new AddRefreshTokenCommandHandler(
                refreshTokenRepository.Object,
                saveChanges.Object,
                removeRefreshToken.Object);

            var command = new AddRefreshTokenCommand(new RefreshToken
            {
                HashedId = "HashedId",
                Username = "Username",
                ClientId = "ClientId",
            });

            await addrefreshTokenCommandHandler.HandleAsync(command);

            refreshTokenRepository.Verify(v => v.TryGetRefreshTokenAsync("Username", "ClientId"));
            removeRefreshToken.Verify(v => v.HandleAsync(It.IsAny<RemoveRefreshTokenCommand>()));
            refreshTokenRepository.Verify(v => v.AddRefreshTokenAsync(command.RefreshToken));
            saveChanges.Verify(v => v.HandleAsync(It.IsAny<SaveChangesCommand>()));
        }
    }
}
