namespace Fifthweek.Api.Tests.CommandHandlers
{
    using System.Threading.Tasks;

    using Fifthweek.Api.CommandHandlers;
    using Fifthweek.Api.Commands;
    using Fifthweek.Api.Models;
    using Fifthweek.Api.Repositories;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RegisterInternalUserCommandHandlerTests
    {
        [TestMethod]
        public async Task ItShouldRegisterTheUser()
        {
            var authenticationRepository = new Mock<IAuthenticationRepository>();

            var handler = new RegisterInternalUserCommandHandler(authenticationRepository.Object);

            var registrationData = new InternalRegistrationData
            {
                Username = "Username",
                Password = "Password",
            };

            await handler.HandleAsync(new RegisterInternalUserCommand(registrationData));

            authenticationRepository.Verify(v => v.AddInternalUserAsync("Username", "Password"));
        }
    }
}