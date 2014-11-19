namespace Fifthweek.Api.Tests.QueryHandlers
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Queries;
    using Fifthweek.Api.QueryHandlers;
    using Fifthweek.Api.Repositories;

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