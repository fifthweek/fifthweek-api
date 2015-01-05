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
    public class IsUsernameAvailableQueryHandlerTests
    {
        [TestMethod]
        public async Task WhenUsernameNotRegistered_ItShouldReturnTrue()
        {
            var userManager = new Mock<IUserManager>();
            userManager.Setup(v => v.FindByNameAsync(Username.Value)).ReturnsAsync(null);
            var handler = new IsUsernameAvailableQueryHandler(userManager.Object);
            var result = await handler.HandleAsync(Query);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task WhenUsernameRegistered_ItShouldReturnFalse()
        {
            var userManager = new Mock<IUserManager>();
            userManager.Setup(v => v.FindByNameAsync(Username.Value)).ReturnsAsync(new FifthweekUser());
            var handler = new IsUsernameAvailableQueryHandler(userManager.Object);
            var result = await handler.HandleAsync(Query);
            Assert.IsFalse(result);
        }

        private static readonly NormalizedUsername Username = NormalizedUsername.Parse("lawrence");
        private static readonly IsUsernameAvailableQuery Query = new IsUsernameAvailableQuery(Username);
    }
}