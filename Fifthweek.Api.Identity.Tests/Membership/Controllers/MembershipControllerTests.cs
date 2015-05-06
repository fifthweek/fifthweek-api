namespace Fifthweek.Api.Identity.Tests.Membership.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Membership.Commands;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class MembershipControllerTests
    {
        private const string UsernameValue = "lawrence";
        private const string Token = "Password Token";
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly ValidUsername Username = ValidUsername.Parse(UsernameValue);
        private Mock<ICommandHandler<RegisterUserCommand>> registerUser;
        private Mock<ICommandHandler<RequestPasswordResetCommand>> requestPasswordReset;
        private Mock<ICommandHandler<ConfirmPasswordResetCommand>> confirmPasswordReset;
        private Mock<IQueryHandler<IsUsernameAvailableQuery, bool>> isUsernameAvailable;
        private Mock<IQueryHandler<IsPasswordResetTokenValidQuery, bool>> isPasswordResetTokenValid;
        private Mock<ICommandHandler<RegisterInterestCommand>> registerInterest;
        private Mock<IGuidCreator> guidCreator;
        private MembershipController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            this.registerUser = new Mock<ICommandHandler<RegisterUserCommand>>();
            this.requestPasswordReset = new Mock<ICommandHandler<RequestPasswordResetCommand>>();
            this.confirmPasswordReset = new Mock<ICommandHandler<ConfirmPasswordResetCommand>>();
            this.isUsernameAvailable = new Mock<IQueryHandler<IsUsernameAvailableQuery, bool>>();
            this.isPasswordResetTokenValid = new Mock<IQueryHandler<IsPasswordResetTokenValidQuery, bool>>();
            this.registerInterest = new Mock<ICommandHandler<RegisterInterestCommand>>();
            this.guidCreator = new Mock<IGuidCreator>();
            this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(Guid.Empty);

            this.controller = new MembershipController(
                this.registerUser.Object,
                this.requestPasswordReset.Object,
                this.confirmPasswordReset.Object,
                this.registerInterest.Object,
                this.isUsernameAvailable.Object,
                this.isPasswordResetTokenValid.Object,
                this.guidCreator.Object);
        }
        
        [TestMethod]
        public async Task WhenPostingRegistrations_ItShouldIssueRegisterUserCommand()
        {
            var registration = NewRegistrationData();
            var command = new RegisterUserCommand(
                UserId,
                registration.ExampleWork,
                ValidEmail.Parse(registration.Email),
                ValidUsername.Parse(registration.Username),
                ValidPassword.Parse(registration.Password));

            this.guidCreator.Setup(_ => _.CreateSqlSequential()).Returns(UserId.Value);
            this.registerUser.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0));

            var result = await this.controller.PostRegistrationAsync(registration);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.registerUser.Verify(v => v.HandleAsync(command));
            this.guidCreator.Verify(v => v.CreateSqlSequential());
        }

        [TestMethod]
        public async Task WhenGettingUsernameAvailability_ItShouldYieldOkIfUsernameAvailable()
        {
            var query = new IsUsernameAvailableQuery(Username);

            this.isUsernameAvailable.Setup(v => v.HandleAsync(query)).Returns(Task.FromResult(true));

            var result = await this.controller.GetUsernameAvailabilityAsync(UsernameValue);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task WhenGettingUsernameAvailability_ItShouldYieldNotFoundIfUsernameUnavailable()
        {
            var query = new IsUsernameAvailableQuery(Username);

            this.isUsernameAvailable.Setup(v => v.HandleAsync(query)).Returns(Task.FromResult(false));

            var result = await this.controller.GetUsernameAvailabilityAsync(UsernameValue);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task WhenPostingPasswordResetRequests_ItShouldIssueRequestPasswordResetCommand()
        {
            var passwordResetRequest = NewPasswordResetRequestData();
            var command = RequestPasswordResetCommandTests.NewCommand(passwordResetRequest);

            var result = await this.controller.PostPasswordResetRequestAsync(passwordResetRequest);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.requestPasswordReset.Verify(v => v.HandleAsync(command));
        }

        [TestMethod]
        public async Task WhenPostingPasswordResetConfirmations_ItShouldIssueConfirmPasswordResetCommand()
        {
            var passwordResetConfirmation = NewPasswordResetConfirmationData();
            var command = ConfirmPasswordResetCommandTests.NewCommand(passwordResetConfirmation);

            var result = await this.controller.PostPasswordResetConfirmationAsync(passwordResetConfirmation);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.confirmPasswordReset.Verify(v => v.HandleAsync(command));
        }

        [TestMethod]
        public async Task WhenGettingPasswordResetTokenValidity_ItShouldYieldOkIfTokenValid()
        {
            var query = new IsPasswordResetTokenValidQuery(UserId, Token);

            this.isPasswordResetTokenValid.Setup(v => v.HandleAsync(query)).Returns(Task.FromResult(true));

            var result = await this.controller.GetPasswordResetTokenValidityAsync(UserId.Value.EncodeGuid(), Token);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task WhenGettingPasswordResetTokenValidity_ItShouldYieldNotFoundIfTokenInvalid()
        {
            var query = new IsPasswordResetTokenValidQuery(UserId, Token);

            this.isPasswordResetTokenValid.Setup(v => v.HandleAsync(query)).Returns(Task.FromResult(false));

            var result = await this.controller.GetPasswordResetTokenValidityAsync(UserId.Value.EncodeGuid(), Token);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task WhenPostingRegisteredInterest_ItShouldIssueRegisterInterestCommand()
        {
            var registration = NewRegisteredInterestData();
            var command = new RegisterInterestCommand(
                registration.Name,
                ValidEmail.Parse(registration.Email));

            this.registerInterest.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0));

            var result = await this.controller.PostRegisteredInterestAsync(registration);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.registerInterest.Verify(v => v.HandleAsync(command));
        }

        public static PasswordResetConfirmationData NewPasswordResetConfirmationData()
        {
            return new PasswordResetConfirmationData
            {
                UserId = new UserId(Guid.Parse("{5E41A09B-0523-4FD3-BC82-9D5A02D2FB97}")),
                Token = "SomeLongBase64EncodedGumpf",
                NewPassword = "SecretSquirrel",
            };
        }

        public static PasswordResetRequestData NewPasswordResetRequestData()
        {
            return new PasswordResetRequestData
            {
                Email = "test@test.com",
                Username = "test_username",
            };
        }

        public static RegistrationData NewRegistrationData()
        {
            return new RegistrationData
            {
                ExampleWork = "TestExampleWork",
                Email = "test@test.com",
                Username = "test_username",
                Password = "TestPassword"
            };
        }

        public static RegisterInterestData NewRegisteredInterestData()
        {
            return new RegisterInterestData
            {
                Name = "phil",
                Email = "test@test.com"
            };
        }
    }
}
