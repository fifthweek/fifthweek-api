namespace Fifthweek.Api.Identity.Tests.OAuth.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateLastAccessTokenDateCommandHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());

        [TestMethod]
        public async Task WhenTheCreationTypeIsSignIn_ItSouldUpdateBothTimestamps()
        {
            var command = new UpdateLastAccessTokenDateCommand(
                UserId,
                DateTime.UtcNow,
                UpdateLastAccessTokenDateCommand.AccessTokenCreationType.SignIn);

            var userRepository = new Mock<IUpdateUserTimeStampsDbStatement>();
            userRepository.Setup(v => v.UpdateSignInAndAccessTokenAsync(command.UserId, command.Timestamp))
                .Returns(Task.FromResult(0)).Verifiable();

            var target = new UpdateLastAccessTokenDateCommandHandler(userRepository.Object);

            await target.HandleAsync(command);

            userRepository.Verify();
        }

        [TestMethod]
        public async Task WhenTheCreationTypeIsRefreshToken_ItSouldUpdateAccessTokenTimestampOnly()
        {
            var command = new UpdateLastAccessTokenDateCommand(
                UserId,
                DateTime.UtcNow,
                UpdateLastAccessTokenDateCommand.AccessTokenCreationType.RefreshToken);

            var userRepository = new Mock<IUpdateUserTimeStampsDbStatement>();
            userRepository.Setup(v => v.UpdateAccessTokenAsync(command.UserId, command.Timestamp))
                .Returns(Task.FromResult(0)).Verifiable();

            var target = new UpdateLastAccessTokenDateCommandHandler(userRepository.Object);

            await target.HandleAsync(command);

            userRepository.Verify();
        }
    }
}