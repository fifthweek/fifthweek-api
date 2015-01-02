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
        public async Task ItShouldIssueRegisterUserCommand_WhenPostingRegistrations()
        {
            var registration = RegistrationDataTests.NewRegistrationData();
            var command = RegisterUserCommandTests.NewRegisterUserCommand(registration);
            var fixture = new TestFixture();
            var normalization = fixture.Normalization;
            var registerUser = fixture.RegisterUser;
            var controller = fixture.Controller;

            normalization.Setup(v => v.NormalizeEmailAddress(registration.Email)).Returns(registration.Email);
            normalization.Setup(v => v.NormalizeUsername(registration.Username)).Returns(registration.Username);
            registerUser.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0));

            var result = await controller.PostRegistrationAsync(registration);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            registerUser.Verify(v => v.HandleAsync(command));
        }

        [TestMethod]
        public async Task ItShouldNormalizeUsernameAndEmail_WhenPostingRegistrations()
        {
            const string emailTransformation = "!";
            const string usernameTransformation = "?";
            var registration = RegistrationDataTests.NewRegistrationData();
            var command = RegisterUserCommandTests.NewRegisterUserCommand(registration);
            var fixture = new TestFixture();
            var normalization = fixture.Normalization;
            var registerUser = fixture.RegisterUser;
            var controller = fixture.Controller;

            normalization.Setup(v => v.NormalizeEmailAddress(registration.Email)).Returns(registration.Email + emailTransformation);
            normalization.Setup(v => v.NormalizeUsername(registration.Username)).Returns(registration.Username + usernameTransformation);
            registerUser.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0));

            var result = await controller.PostRegistrationAsync(registration);

            var expectedRegistration = RegistrationDataTests.NewRegistrationData();
            expectedRegistration.Email += emailTransformation;
            expectedRegistration.Username += usernameTransformation;
            var expectedCommand = RegisterUserCommandTests.NewRegisterUserCommand(expectedRegistration);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            registerUser.Verify(v => v.HandleAsync(expectedCommand));
        }

        [TestMethod]
        public async Task ItShouldNormalizeWithoutMutatingRegistration_WhenPostingRegistrations()
        {
            const string emailTransformation = "!";
            const string usernameTransformation = "?";
            var registration = RegistrationDataTests.NewRegistrationData();
            var fixture = new TestFixture();
            var normalization = fixture.Normalization;
            var registerUser = fixture.RegisterUser;
            var controller = fixture.Controller;
            RegisterUserCommand actualCommand = null;

            normalization.Setup(v => v.NormalizeEmailAddress(registration.Email)).Returns(registration.Email + emailTransformation);
            normalization.Setup(v => v.NormalizeUsername(registration.Username)).Returns(registration.Username + usernameTransformation);
            registerUser.Setup(v => v.HandleAsync(It.IsAny<RegisterUserCommand>()))
                .Returns(Task.FromResult(0))
                .Callback<RegisterUserCommand>(c => actualCommand = c);

            var result = await controller.PostRegistrationAsync(registration);

            Assert.AreEqual(registration.ExampleWork, actualCommand.ExampleWork);
            Assert.AreEqual(registration.Password, actualCommand.Password);
            Assert.AreNotEqual(registration.Email, actualCommand.Email);
            Assert.AreNotEqual(registration.Username, actualCommand.Username);
            Assert.IsInstanceOfType(result, typeof(OkResult));

            var expectedRegistration = RegistrationDataTests.NewRegistrationData();
            expectedRegistration.Email += emailTransformation;
            expectedRegistration.Username += usernameTransformation;
            var expectedCommand = RegisterUserCommandTests.NewRegisterUserCommand(expectedRegistration);

            registerUser.Verify(v => v.HandleAsync(expectedCommand));
        }

        [TestMethod]
        public async Task ItShouldYieldOkIfUsernameAvailable_WhenGettingUsernameAvailability()
        {
            const string username = "Lawrence";
            var query = new GetUsernameAvailabilityQuery(username);
            var fixture = new TestFixture();
            var normalization = fixture.Normalization;
            var getUsernameAvailability = fixture.GetUsernameAvailability;
            var controller = fixture.Controller;

            normalization.Setup(v => v.NormalizeUsername(username)).Returns(username);
            getUsernameAvailability.Setup(v => v.HandleAsync(query)).Returns(Task.FromResult(true));

            var result = await controller.GetUsernameAvailabilityAsync(username);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task ItShouldYieldNotFoundIfUsernameUnavailable_WhenGettingUsernameAvailability()
        {
            const string username = "Lawrence";
            var query = new GetUsernameAvailabilityQuery(username);
            var fixture = new TestFixture();
            var normalization = fixture.Normalization;
            var getUsernameAvailability = fixture.GetUsernameAvailability;
            var controller = fixture.Controller;

            normalization.Setup(v => v.NormalizeUsername(username)).Returns(username);
            getUsernameAvailability.Setup(v => v.HandleAsync(query)).Returns(Task.FromResult(false));

            var result = await controller.GetUsernameAvailabilityAsync(username);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ItShouldNormalizeUsername_WhenGettingUsernameAvailability()
        {
            const string usernameTransformation = "?";
            const string username = "Lawrence";
            var expectedQuery = new GetUsernameAvailabilityQuery(username + usernameTransformation);
            var fixture = new TestFixture();
            var normalization = fixture.Normalization;
            var getUsernameAvailability = fixture.GetUsernameAvailability;
            var controller = fixture.Controller;

            normalization.Setup(v => v.NormalizeUsername(username)).Returns(username + usernameTransformation);
            getUsernameAvailability.Setup(v => v.HandleAsync(expectedQuery)).Returns(Task.FromResult(true));

            var result = await controller.GetUsernameAvailabilityAsync(username);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            getUsernameAvailability.Verify(v => v.HandleAsync(expectedQuery));
        }

        [TestMethod]
        public async Task ItShouldIssueRequestPasswordResetCommand_WhenPostingPasswordResetRequests()
        {
            var passwordResetRequest = PasswordResetRequestDataTests.NewPasswordResetRequestData();
            var command = RequestPasswordResetCommandTests.NewRequestPasswordResetCommand(passwordResetRequest);
            var fixture = new TestFixture();
            var controller = fixture.Controller;
            var requestPasswordReset = fixture.RequestPasswordReset;

            var result = await controller.PostPasswordResetRequestAsync(passwordResetRequest);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            requestPasswordReset.Verify(v => v.HandleAsync(command));
        }

        private class TestFixture
        {
            public TestFixture()
            {
                this.RegisterUser = new Mock<ICommandHandler<RegisterUserCommand>>();
                this.RequestPasswordReset = new Mock<ICommandHandler<RequestPasswordResetCommand>>();
                this.GetUsernameAvailability = new Mock<IQueryHandler<GetUsernameAvailabilityQuery, bool>>();
                this.Normalization = new Mock<IUserInputNormalization>();
                this.Controller = new MembershipController(this.RegisterUser.Object, this.RequestPasswordReset.Object, this.GetUsernameAvailability.Object, this.Normalization.Object);
            }

            public Mock<ICommandHandler<RegisterUserCommand>> RegisterUser { get; private set; }
            public Mock<ICommandHandler<RequestPasswordResetCommand>> RequestPasswordReset { get; private set; }
            public Mock<IQueryHandler<GetUsernameAvailabilityQuery, bool>> GetUsernameAvailability { get; private set; }
            public Mock<IUserInputNormalization> Normalization { get; private set; }
            public MembershipController Controller { get; private set; }
        }
    }
}
