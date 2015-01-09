namespace Fifthweek.Api.Identity.Tests.Membership.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetUserQueryHandlerTests
    {
        [TestMethod]
        public async Task ItShouldReturnTheRequestedUser()
        {
            var authenticationRepository = new Mock<IUserManager>();

            authenticationRepository.Setup(v => v.FindAsync("username", "Password"))
                .ReturnsAsync(new FifthweekUser { UserName = "username" });

            var handler = new GetUserQueryHandler(authenticationRepository.Object);

            var username = Username.Parse("username");
            var password = Password.Parse("Password");
            var result = await handler.HandleAsync(new GetUserQuery(username, password));

            authenticationRepository.Verify(v => v.FindAsync("username", "Password"));

            Assert.AreEqual("username", result.UserName);
        }
    }
}