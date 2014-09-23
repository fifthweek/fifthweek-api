namespace Dexter.Api.Tests.CommandHandlers
{
    using System.Threading.Tasks;

    using Dexter.Api.CommandHandlers;
    using Dexter.Api.Commands;
    using Dexter.Api.Models;
    using Dexter.Api.Repositories;

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