namespace Fifthweek.Api.Identity.Tests.Membership.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.Tests.Membership.Commands;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class MembershipControllerTests
    {
        [TestMethod]
        public async Task WhenPostingRegistrations_ItShouldIssueRegisterUserCommand()
        {
            var registration = RegistrationDataTests.NewRegistrationData();
            var command = RegisterUserCommandTests.NewRegisterUserCommand(registration);

            this.normalization.Setup(v => v.NormalizeEmailAddress(registration.Email)).Returns(registration.Email);
            this.normalization.Setup(v => v.NormalizeUsername(registration.Username)).Returns(registration.Username);
            this.registerUser.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0));

            var result = await this.controller.PostRegistrationAsync(registration);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.registerUser.Verify(v => v.HandleAsync(command));
        }


        [TestMethod]
        public async Task WhenGettingUsernameAvailability_ItShouldYieldOkIfUsernameAvailable()
        {
            const string username = "Lawrence";
            var query = new GetUsernameAvailabilityQuery(username);

            this.normalization.Setup(v => v.NormalizeUsername(username)).Returns(username);
            this.getUsernameAvailability.Setup(v => v.HandleAsync(query)).Returns(Task.FromResult(true));

            var result = await this.controller.GetUsernameAvailabilityAsync(username);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task WhenGettingUsernameAvailability_ItShouldYieldNotFoundIfUsernameUnavailable()
        {
            const string username = "Lawrence";
            var query = new GetUsernameAvailabilityQuery(username);

            this.normalization.Setup(v => v.NormalizeUsername(username)).Returns(username);
            this.getUsernameAvailability.Setup(v => v.HandleAsync(query)).Returns(Task.FromResult(false));

            var result = await this.controller.GetUsernameAvailabilityAsync(username);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task WhenPostingPasswordResetRequests_ItShouldIssueRequestPasswordResetCommand()
        {
            var passwordResetRequest = PasswordResetRequestDataTests.NewPasswordResetRequestData();
            var command = RequestPasswordResetCommandTests.NewRequestPasswordResetCommand(passwordResetRequest);

            var result = await this.controller.PostPasswordResetRequestAsync(passwordResetRequest);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.requestPasswordReset.Verify(v => v.HandleAsync(command));
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this.registerUser = new Mock<ICommandHandler<RegisterUserCommand>>();
            this.requestPasswordReset = new Mock<ICommandHandler<RequestPasswordResetCommand>>();
            this.getUsernameAvailability = new Mock<IQueryHandler<GetUsernameAvailabilityQuery, bool>>();
            this.normalization = new Mock<IUserInputNormalization>();
            this.controller = new MembershipController(this.registerUser.Object, this.requestPasswordReset.Object, this.getUsernameAvailability.Object, this.normalization.Object);
        }

        private Mock<ICommandHandler<RegisterUserCommand>> registerUser;
        private Mock<ICommandHandler<RequestPasswordResetCommand>> requestPasswordReset;
        private Mock<IQueryHandler<GetUsernameAvailabilityQuery, bool>> getUsernameAvailability;
        private Mock<IUserInputNormalization> normalization;
        private MembershipController controller;
    }
}
