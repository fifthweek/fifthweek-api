namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Commands;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateLastAccessTokenDateCommandHandlerTests
    {
        private string testUsername = "TestUsername";

        [TestMethod]
        public async Task WhenTheCreationTypeIsSignIn_ItSouldUpdateBothTimestamps()
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
        public async Task WhenTheCreationTypeIsRefreshToken_ItSouldUpdateAccessTokenTimestampOnly()
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