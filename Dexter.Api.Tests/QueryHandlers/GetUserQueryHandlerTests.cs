namespace Dexter.Api.Tests.QueryHandlers
{
    using System.Threading.Tasks;

    using Dexter.Api.Queries;
    using Dexter.Api.QueryHandlers;
    using Dexter.Api.Repositories;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetUserQueryHandlerTests
    {
        [TestMethod]
        public async Task ItShouldReturnTheRequestedUser()
        {
            var authenticationRepository = new Mock<IAuthenticationRepository>();

            authenticationRepository.Setup(v => v.FindInternalUserAsync("Username", "Password"))
                .ReturnsAsync(new IdentityUser("Username"));

            var handler = new GetUserQueryHandler(authenticationRepository.Object);

            var result = await handler.HandleAsync(new GetUserQuery("Username", "Password"));

            authenticationRepository.Verify(v => v.FindInternalUserAsync("Username", "Password"));

            Assert.AreEqual("Username", result.UserName);
        }
    }
}