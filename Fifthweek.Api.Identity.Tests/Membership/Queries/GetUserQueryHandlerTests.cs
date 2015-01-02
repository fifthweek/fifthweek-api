namespace Fifthweek.Api.Identity.Tests.Membership.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Persistence;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetUserQueryHandlerTests
    {
        [TestMethod]
        public async Task ItShouldReturnTheRequestedUser()
        {
            var authenticationRepository = new Mock<IUserManager>();

            authenticationRepository.Setup(v => v.FindAsync("Username", "Password"))
                .ReturnsAsync(new ApplicationUser { UserName = "Username" });

            var handler = new GetUserQueryHandler(authenticationRepository.Object);

            var result = await handler.HandleAsync(new GetUserQuery("Username", "Password"));

            authenticationRepository.Verify(v => v.FindAsync("Username", "Password"));

            Assert.AreEqual("Username", result.UserName);
        }
    }
}