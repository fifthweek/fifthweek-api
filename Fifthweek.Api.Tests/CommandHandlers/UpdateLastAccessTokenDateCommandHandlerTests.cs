namespace Fifthweek.Api.Tests.CommandHandlers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateLastAccessTokenDateCommandHandlerTests
    {
        private string testUsername = "TestUsername";

        [TestMethod]
        public async Task WhenTheCreationTypeIsSignInItSouldUpdateBothTimestamps()
        {
            var command = new UpdateLastAccessTokenDateCommand(
                this.testUsername,
                DateTime.UtcNow,
                UpdateLastAccessTokenDateCommand.AccessTokenCreationType.SignIn);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(v => v.UpdateLastSignInDateAndAccessTokenDateAsync(command.Username, command.Timestamp))
                .Returns(Task.FromResult(0)).Verifiable();

            var target = new UpdateLastAccessTokenDateCommandHandler(userRepository.Object);

            await target.HandleAsync(command);

            userRepository.Verify();
        }

        [TestMethod]
        public async Task WhenTheCreationTypeIsRefreshTokenItSouldUpdateAccessTokenTimestampOnly()
        {
            var command = new UpdateLastAccessTokenDateCommand(
                this.testUsername,
                DateTime.UtcNow,
                UpdateLastAccessTokenDateCommand.AccessTokenCreationType.RefreshToken);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(v => v.UpdateLastAccessTokenDateAsync(command.Username, command.Timestamp))
                .Returns(Task.FromResult(0)).Verifiable();

            var target = new UpdateLastAccessTokenDateCommandHandler(userRepository.Object);

            await target.HandleAsync(command);

            userRepository.Verify();
        }
    }
}