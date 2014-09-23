namespace Dexter.Api.Tests.CommandHandlers
{
    using System;
    using System.Threading.Tasks;

    using Dexter.Api.CommandHandlers;
    using Dexter.Api.Commands;
    using Dexter.Api.Models;
    using Dexter.Api.Queries;
    using Dexter.Api.QueryHandlers;
    using Dexter.Api.Repositories;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RegisterExternalUserCommandHandlerTests
    {
        [TestMethod]
        public async Task ItShouldVerifyTheTokenAndAddTheUser()
        {
            var authenticationRepository = new Mock<IAuthenticationRepository>();
            var getVerifiedAccessToken = new Mock<IQueryHandler<GetVerifiedAccessTokenQuery, ParsedExternalAccessToken>>();

            getVerifiedAccessToken.Setup(v => v.HandleAsync(It.IsAny<GetVerifiedAccessTokenQuery>()))
                .ReturnsAsync(new ParsedExternalAccessToken { ApplicationId = "ApplicationId", UserId = "UserId" });

            var handler = new RegisterExternalUserCommandHandler(
                authenticationRepository.Object,
                getVerifiedAccessToken.Object);

            var registrationData = new ExternalRegistrationData
            {
                Username = "Username",
                Provider = "Provider",
                ExternalAccessToken = "ABC",
            };

            await handler.HandleAsync(new RegisterExternalUserCommand(registrationData));

            getVerifiedAccessToken.Verify(v => v.HandleAsync(It.IsAny<GetVerifiedAccessTokenQuery>()));
            authenticationRepository.Verify(v => v.FindExternalUserAsync("Provider", "UserId"));
            authenticationRepository.Verify(v => v.AddExternalUserAsync("Username", "Provider", "UserId"));
        }

        [TestMethod]
        public async Task ItShouldThrowAnExceptionIfTheTokenCannotBeVerified()
        {
            var authenticationRepository = new Mock<IAuthenticationRepository>();
            var getVerifiedAccessToken = new Mock<IQueryHandler<GetVerifiedAccessTokenQuery, ParsedExternalAccessToken>>();

            getVerifiedAccessToken.Setup(v => v.HandleAsync(It.IsAny<GetVerifiedAccessTokenQuery>()))
                .ReturnsAsync(null);

            var handler = new RegisterExternalUserCommandHandler(
                authenticationRepository.Object,
                getVerifiedAccessToken.Object);

            var registrationData = new ExternalRegistrationData
            {
                Username = "Username",
                Provider = "Provider",
                ExternalAccessToken = "ABC",
            };

            Exception exception = null;
            try
            {
                await handler.HandleAsync(new RegisterExternalUserCommand(registrationData));
            }
            catch (BadRequestException t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
            getVerifiedAccessToken.Verify(v => v.HandleAsync(It.IsAny<GetVerifiedAccessTokenQuery>()));
            authenticationRepository.Verify(v => v.FindExternalUserAsync("Provider", "UserId"), Times.Never());
            authenticationRepository.Verify(v => v.AddExternalUserAsync("Username", "Provider", "UserId"), Times.Never());
        }

        [TestMethod]
        public async Task ItShouldThrowAnExceptionIfTheUserIsAlreadyRegistered()
        {
            var authenticationRepository = new Mock<IAuthenticationRepository>();
            var getVerifiedAccessToken = new Mock<IQueryHandler<GetVerifiedAccessTokenQuery, ParsedExternalAccessToken>>();

            getVerifiedAccessToken.Setup(v => v.HandleAsync(It.IsAny<GetVerifiedAccessTokenQuery>()))
                .ReturnsAsync(new ParsedExternalAccessToken { ApplicationId = "ApplicationId", UserId = "UserId" });

            authenticationRepository.Setup(v => v.FindExternalUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new IdentityUser());

            var handler = new RegisterExternalUserCommandHandler(
                authenticationRepository.Object,
                getVerifiedAccessToken.Object);

            var registrationData = new ExternalRegistrationData
            {
                Username = "Username",
                Provider = "Provider",
                ExternalAccessToken = "ABC",
            };

            Exception exception = null;
            try
            {
                await handler.HandleAsync(new RegisterExternalUserCommand(registrationData));
            }
            catch (BadRequestException t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
            getVerifiedAccessToken.Verify(v => v.HandleAsync(It.IsAny<GetVerifiedAccessTokenQuery>()));
            authenticationRepository.Verify(v => v.FindExternalUserAsync("Provider", "UserId"));
            authenticationRepository.Verify(v => v.AddExternalUserAsync("Username", "Provider", "UserId"), Times.Never());
        }
    }
}