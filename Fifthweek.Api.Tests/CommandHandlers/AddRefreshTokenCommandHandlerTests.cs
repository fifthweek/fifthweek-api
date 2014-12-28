﻿namespace Fifthweek.Api.Tests.CommandHandlers
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
        public async Task ItShouldRemoveExistingTokensAndAddNewToken()
        {
            var command = new AddRefreshTokenCommand(new RefreshToken
            {
                HashedId = "HashedId",
                Username = "Username",
                ClientId = "ClientId",
            });

            var refreshTokenRepository = new Mock<IRefreshTokenRepository>();

            refreshTokenRepository.Setup(v => v.RemoveRefreshTokens("Username", "ClientId"))
                .Returns(Task.FromResult(0)).Verifiable();

            refreshTokenRepository.Setup(v => v.AddRefreshTokenAsync(command.RefreshToken))
                .Returns(Task.FromResult(0)).Verifiable();

            var addrefreshTokenCommandHandler = new AddRefreshTokenCommandHandler(
                refreshTokenRepository.Object);

            await addrefreshTokenCommandHandler.HandleAsync(command);

            refreshTokenRepository.Verify();
        }
    }
}
