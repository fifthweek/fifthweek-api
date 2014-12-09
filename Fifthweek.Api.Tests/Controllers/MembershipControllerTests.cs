using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task ItShouldIssueRegisterUserCommandForPostedRegistration()
        {
            var registration = RegistrationDataTests.NewRegistrationData();
            var command = new RegisterUserCommand(registration);
            var commandHandler = new Mock<ICommandHandler<RegisterUserCommand>>();
            var queryHandler = new Mock<IQueryHandler<GetUsernameAvailabilityQuery, bool>>();
            var controller = new MembershipController(commandHandler.Object, queryHandler.Object);

            commandHandler.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0));

            var result = await controller.PostRegistrationAsync(registration);

            commandHandler.Verify(v => v.HandleAsync(command));
        }
    }
}
