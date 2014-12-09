using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Fifthweek.Api.CommandHandlers;
using Fifthweek.Api.Commands;
using Fifthweek.Api.Controllers;
using Fifthweek.Api.Models;
using Fifthweek.Api.Queries;
using Fifthweek.Api.QueryHandlers;
using Fifthweek.Api.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Fifthweek.Api.Tests
{
    [TestClass]
    public class MembershipControllerTests
    {
        [TestMethod]
        public async Task ItShouldIssueRegisterUserCommand_WhenPostingRegistrations()
        {
            var registration = RegistrationDataTests.NewRegistrationData();
            var command = new RegisterUserCommand(registration);
            var commandHandler = new Mock<ICommandHandler<RegisterUserCommand>>();
            var queryHandler = new Mock<IQueryHandler<GetUsernameAvailabilityQuery, bool>>();
            var normalization = new Mock<IUserInputNormalization>();
            var controller = new MembershipController(commandHandler.Object, queryHandler.Object, normalization.Object);

            normalization.Setup(v => v.NormalizeEmailAddress(registration.Email)).Returns(registration.Email);
            normalization.Setup(v => v.NormalizeUsername(registration.Username)).Returns(registration.Username);
            commandHandler.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0));

            var result = await controller.PostRegistrationAsync(registration);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            commandHandler.Verify(v => v.HandleAsync(command));
        }

        [TestMethod]
        public async Task ItShouldNormalizeUsernameAndEmail_WhenPostingRegistrations()
        {
            const string emailTransformation = "!";
            const string usernameTransformation = "?";
            var registration = RegistrationDataTests.NewRegistrationData();
            var command = new RegisterUserCommand(registration);
            var commandHandler = new Mock<ICommandHandler<RegisterUserCommand>>();
            var queryHandler = new Mock<IQueryHandler<GetUsernameAvailabilityQuery, bool>>();
            var normalization = new Mock<IUserInputNormalization>();
            var controller = new MembershipController(commandHandler.Object, queryHandler.Object, normalization.Object);

            normalization.Setup(v => v.NormalizeEmailAddress(registration.Email)).Returns(registration.Email + emailTransformation);
            normalization.Setup(v => v.NormalizeUsername(registration.Username)).Returns(registration.Username + usernameTransformation);
            commandHandler.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0));

            var result = await controller.PostRegistrationAsync(registration);

            var expectedRegistration = RegistrationDataTests.NewRegistrationData();
            expectedRegistration.Email += emailTransformation;
            expectedRegistration.Username += usernameTransformation;
            var expectedCommand = new RegisterUserCommand(expectedRegistration);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            commandHandler.Verify(v => v.HandleAsync(expectedCommand));
        }

        [TestMethod]
        public async Task ItShouldNormalizeWithoutMutatingRegistration_WhenPostingRegistrations()
        {
            const string emailTransformation = "!";
            const string usernameTransformation = "?";
            var registration = RegistrationDataTests.NewRegistrationData();
            var commandHandler = new Mock<ICommandHandler<RegisterUserCommand>>();
            var queryHandler = new Mock<IQueryHandler<GetUsernameAvailabilityQuery, bool>>();
            var normalization = new Mock<IUserInputNormalization>();
            var controller = new MembershipController(commandHandler.Object, queryHandler.Object, normalization.Object);
            RegisterUserCommand actualCommand = null;

            normalization.Setup(v => v.NormalizeEmailAddress(registration.Email)).Returns(registration.Email + emailTransformation);
            normalization.Setup(v => v.NormalizeUsername(registration.Username)).Returns(registration.Username + usernameTransformation);
            commandHandler.Setup(v => v.HandleAsync(It.IsAny<RegisterUserCommand>()))
                .Returns(Task.FromResult(0))
                .Callback<RegisterUserCommand>(c => actualCommand = c);

            var result = await controller.PostRegistrationAsync(registration);

            Assert.IsFalse(
                Object.ReferenceEquals(actualCommand.RegistrationData, registration), 
                "Objects received through method parameters must not be mutated.");
            Assert.IsInstanceOfType(result, typeof(OkResult));

            var expectedRegistration = RegistrationDataTests.NewRegistrationData();
            expectedRegistration.Email += emailTransformation;
            expectedRegistration.Username += usernameTransformation;
            var expectedCommand = new RegisterUserCommand(expectedRegistration);

            commandHandler.Verify(v => v.HandleAsync(expectedCommand));
        }

        [TestMethod]
        public async Task ItShouldYieldOkIfUsernameAvailable_WhenGettingUsernameAvailability()
        {
            const string username = "Lawrence";
            var query = new GetUsernameAvailabilityQuery(username);
            var commandHandler = new Mock<ICommandHandler<RegisterUserCommand>>();
            var queryHandler = new Mock<IQueryHandler<GetUsernameAvailabilityQuery, bool>>();
            var normalization = new Mock<IUserInputNormalization>();
            var controller = new MembershipController(commandHandler.Object, queryHandler.Object, normalization.Object);

            normalization.Setup(v => v.NormalizeUsername(username)).Returns(username);
            queryHandler.Setup(v => v.HandleAsync(query)).Returns(Task.FromResult(true));

            var result = await controller.GetUsernameAvailabilityAsync(username);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task ItShouldYieldNotFoundIfUsernameUnavailable_WhenGettingUsernameAvailability()
        {
            const string username = "Lawrence";
            var query = new GetUsernameAvailabilityQuery(username);
            var commandHandler = new Mock<ICommandHandler<RegisterUserCommand>>();
            var queryHandler = new Mock<IQueryHandler<GetUsernameAvailabilityQuery, bool>>();
            var normalization = new Mock<IUserInputNormalization>();
            var controller = new MembershipController(commandHandler.Object, queryHandler.Object, normalization.Object);

            normalization.Setup(v => v.NormalizeUsername(username)).Returns(username);
            queryHandler.Setup(v => v.HandleAsync(query)).Returns(Task.FromResult(false));

            var result = await controller.GetUsernameAvailabilityAsync(username);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ItShouldNormalizeUsername_WhenGettingUsernameAvailability()
        {
            const string usernameTransformation = "?";
            const string username = "Lawrence";
            var expectedQuery = new GetUsernameAvailabilityQuery(username + usernameTransformation);
            var commandHandler = new Mock<ICommandHandler<RegisterUserCommand>>();
            var queryHandler = new Mock<IQueryHandler<GetUsernameAvailabilityQuery, bool>>();
            var normalization = new Mock<IUserInputNormalization>();
            var controller = new MembershipController(commandHandler.Object, queryHandler.Object, normalization.Object);

            normalization.Setup(v => v.NormalizeUsername(username)).Returns(username + usernameTransformation);
            queryHandler.Setup(v => v.HandleAsync(expectedQuery)).Returns(Task.FromResult(true));

            var result = await controller.GetUsernameAvailabilityAsync(username);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            queryHandler.Verify(v => v.HandleAsync(expectedQuery));
        }
    }
}
