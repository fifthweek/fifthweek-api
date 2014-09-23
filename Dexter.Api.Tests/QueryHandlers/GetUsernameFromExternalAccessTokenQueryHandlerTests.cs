namespace Dexter.Api.Tests.QueryHandlers
{
    using System;
    using System.Threading.Tasks;

    using Dexter.Api.Models;
    using Dexter.Api.Queries;
    using Dexter.Api.QueryHandlers;
    using Dexter.Api.Repositories;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetUsernameFromExternalAccessTokenQueryHandlerTests
    {
        private Mock<IAuthenticationRepository> authenticationRespository;

        private Mock<IQueryHandler<GetVerifiedAccessTokenQuery, ParsedExternalAccessToken>> getVerifiedAccessToken;

        private GetUsernameFromExternalAccessTokenQueryHandler handler;

        [TestInitialize]
        public void TestInitialize()
        {
            this.authenticationRespository = new Mock<IAuthenticationRepository>();
            this.getVerifiedAccessToken = new Mock<IQueryHandler<GetVerifiedAccessTokenQuery,ParsedExternalAccessToken>>();

            this.handler = new GetUsernameFromExternalAccessTokenQueryHandler(
                this.authenticationRespository.Object,
                this.getVerifiedAccessToken.Object);
        }

        [TestMethod]
        public async Task ItShouldReturnTheRequestedUsername()
        {
            this.getVerifiedAccessToken.Setup(v => v.HandleAsync(It.IsAny<GetVerifiedAccessTokenQuery>()))
                .ReturnsAsync(new ParsedExternalAccessToken { UserId = "UserId" });

            this.authenticationRespository.Setup(v => v.FindExternalUserAsync("Provider", "UserId"))
                .ReturnsAsync(new IdentityUser("Username"));

            var query = new GetUsernameFromExternalAccessTokenQuery("Provider", "ExternalAccessToken");

            var result = await this.handler.HandleAsync(query);

            this.getVerifiedAccessToken.Verify(v => v.HandleAsync(It.IsAny<GetVerifiedAccessTokenQuery>()));
            this.authenticationRespository.Verify(v => v.FindExternalUserAsync("Provider", "UserId"));

            Assert.AreEqual("Username", result);
        }

        [TestMethod]
        public async Task ItShouldThrowAnExceptionIfTheProviderIsEmpty()
        {
            var query = new GetUsernameFromExternalAccessTokenQuery(string.Empty, "ExternalAccessToken");

            Exception exception = null;
            try
            {
                await this.handler.HandleAsync(query);
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);

            this.getVerifiedAccessToken.Verify(v => v.HandleAsync(It.IsAny<GetVerifiedAccessTokenQuery>()), Times.Never());
            this.authenticationRespository.Verify(v => v.FindExternalUserAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        public async Task ItShouldThrowAnExceptionIfTheExternalAccessTokenIsEmpty()
        {
            var query = new GetUsernameFromExternalAccessTokenQuery("Provider", string.Empty);

            Exception exception = null;
            try
            {
                await this.handler.HandleAsync(query);
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);

            this.getVerifiedAccessToken.Verify(v => v.HandleAsync(It.IsAny<GetVerifiedAccessTokenQuery>()), Times.Never());
            this.authenticationRespository.Verify(v => v.FindExternalUserAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        public async Task ItShouldThrowAnExceptionIfTheAccessTokenIsNotFound()
        {
            this.getVerifiedAccessToken.Setup(v => v.HandleAsync(It.IsAny<GetVerifiedAccessTokenQuery>()))
                .ReturnsAsync(null);

            var query = new GetUsernameFromExternalAccessTokenQuery("Provider", "ExternalAccessToken");

            Exception exception = null;
            try
            {
                await this.handler.HandleAsync(query);
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);

            this.getVerifiedAccessToken.Verify(v => v.HandleAsync(It.IsAny<GetVerifiedAccessTokenQuery>()));
            this.authenticationRespository.Verify(v => v.FindExternalUserAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        public async Task ItShouldThrowAnExceptionIfTheUserIsNotFound()
        {
            this.getVerifiedAccessToken.Setup(v => v.HandleAsync(It.IsAny<GetVerifiedAccessTokenQuery>()))
                .ReturnsAsync(new ParsedExternalAccessToken { UserId = "UserId" });

            this.authenticationRespository.Setup(v => v.FindExternalUserAsync("Provider", "UserId"))
                .ReturnsAsync(null);

            var query = new GetUsernameFromExternalAccessTokenQuery("Provider", "ExternalAccessToken");

            Exception exception = null;
            try
            {
                await this.handler.HandleAsync(query);
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);

            this.getVerifiedAccessToken.Verify(v => v.HandleAsync(It.IsAny<GetVerifiedAccessTokenQuery>()));
            this.authenticationRespository.Verify(v => v.FindExternalUserAsync("Provider", "UserId"));
        }
    }
}