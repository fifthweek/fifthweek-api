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

            this.Normalization.Setup(v => v.NormalizeEmailAddress(registration.Email)).Returns(registration.Email);
            this.Normalization.Setup(v => v.NormalizeUsername(registration.Username)).Returns(registration.Username);
            this.RegisterUser.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0));

            var result = await this.Controller.PostRegistrationAsync(registration);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.RegisterUser.Verify(v => v.HandleAsync(command));
        }

        [TestMethod]
        public async Task WhenPostingRegistrations_ItShouldNormalizeUsernameAndEmail()
        {
            const string emailTransformation = "!";
            const string usernameTransformation = "?";
            var registration = RegistrationDataTests.NewRegistrationData();
            var command = RegisterUserCommandTests.NewRegisterUserCommand(registration);

            this.Normalization.Setup(v => v.NormalizeEmailAddress(registration.Email)).Returns(registration.Email + emailTransformation);
            this.Normalization.Setup(v => v.NormalizeUsername(registration.Username)).Returns(registration.Username + usernameTransformation);
            this.RegisterUser.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0));

            var result = await this.Controller.PostRegistrationAsync(registration);

            var expectedRegistration = RegistrationDataTests.NewRegistrationData();
            expectedRegistration.Email += emailTransformation;
            expectedRegistration.Username += usernameTransformation;
            var expectedCommand = RegisterUserCommandTests.NewRegisterUserCommand(expectedRegistration);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.RegisterUser.Verify(v => v.HandleAsync(expectedCommand));
        }

        [TestMethod]
        public async Task WhenPostingRegistrations_ItShouldNormalizeWithoutMutatingRegistration()
        {
            const string emailTransformation = "!";
            const string usernameTransformation = "?";
            var registration = RegistrationDataTests.NewRegistrationData();
            RegisterUserCommand actualCommand = null;

            this.Normalization.Setup(v => v.NormalizeEmailAddress(registration.Email)).Returns(registration.Email + emailTransformation);
            this.Normalization.Setup(v => v.NormalizeUsername(registration.Username)).Returns(registration.Username + usernameTransformation);
            this.RegisterUser.Setup(v => v.HandleAsync(It.IsAny<RegisterUserCommand>()))
                .Returns(Task.FromResult(0))
                .Callback<RegisterUserCommand>(c => actualCommand = c);

            var result = await this.Controller.PostRegistrationAsync(registration);

            Assert.AreEqual(registration.ExampleWork, actualCommand.ExampleWork);
            Assert.AreEqual(registration.Password, actualCommand.Password);
            Assert.AreNotEqual(registration.Email, actualCommand.Email);
            Assert.AreNotEqual(registration.Username, actualCommand.Username);
            Assert.IsInstanceOfType(result, typeof(OkResult));

            var expectedRegistration = RegistrationDataTests.NewRegistrationData();
            expectedRegistration.Email += emailTransformation;
            expectedRegistration.Username += usernameTransformation;
            var expectedCommand = RegisterUserCommandTests.NewRegisterUserCommand(expectedRegistration);

            this.RegisterUser.Verify(v => v.HandleAsync(expectedCommand));
        }

        [TestMethod]
        public async Task WhenGettingUsernameAvailability_ItShouldYieldOkIfUsernameAvailable()
        {
            const string username = "Lawrence";
            var query = new GetUsernameAvailabilityQuery(username);

            this.Normalization.Setup(v => v.NormalizeUsername(username)).Returns(username);
            this.GetUsernameAvailability.Setup(v => v.HandleAsync(query)).Returns(Task.FromResult(true));

            var result = await this.Controller.GetUsernameAvailabilityAsync(username);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task WhenGettingUsernameAvailability_ItShouldYieldNotFoundIfUsernameUnavailable()
        {
            const string username = "Lawrence";
            var query = new GetUsernameAvailabilityQuery(username);

            this.Normalization.Setup(v => v.NormalizeUsername(username)).Returns(username);
            this.GetUsernameAvailability.Setup(v => v.HandleAsync(query)).Returns(Task.FromResult(false));

            var result = await this.Controller.GetUsernameAvailabilityAsync(username);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task WhenGettingUsernameAvailability_ItShouldNormalizeUsername()
        {
            const string usernameTransformation = "?";
            const string username = "Lawrence";
            var expectedQuery = new GetUsernameAvailabilityQuery(username + usernameTransformation);

            this.Normalization.Setup(v => v.NormalizeUsername(username)).Returns(username + usernameTransformation);
            this.GetUsernameAvailability.Setup(v => v.HandleAsync(expectedQuery)).Returns(Task.FromResult(true));

            var result = await this.Controller.GetUsernameAvailabilityAsync(username);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.GetUsernameAvailability.Verify(v => v.HandleAsync(expectedQuery));
        }

        [TestMethod]
        public async Task WhenPostingPasswordResetRequests_ItShouldIssueRequestPasswordResetCommand()
        {
            var passwordResetRequest = PasswordResetRequestDataTests.NewPasswordResetRequestData();
            var command = RequestPasswordResetCommandTests.NewRequestPasswordResetCommand(passwordResetRequest);

            var result = await this.Controller.PostPasswordResetRequestAsync(passwordResetRequest);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.RequestPasswordReset.Verify(v => v.HandleAsync(command));
        }

        [TestInitialize]
        public void TestInitialize()
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
