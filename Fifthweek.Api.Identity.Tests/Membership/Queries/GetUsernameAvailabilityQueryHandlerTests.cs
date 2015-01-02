namespace Fifthweek.Api.Identity.Tests.Membership.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Persistence;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetUsernameAvailabilityQueryHandlerTests
    {
        private static readonly string username = "lawrence";
        private static readonly GetUsernameAvailabilityQuery query = new GetUsernameAvailabilityQuery(username);

        [TestMethod]
        public async Task WhenUsernameNotRegistered_ItShouldReturnTrue()
        {
            var userManager = new Mock<IUserManager>();
            userManager.Setup(v => v.FindByNameAsync(username)).ReturnsAsync(null);
            var handler = new GetUsernameAvailabilityQueryHandler(userManager.Object);
            var result = await handler.HandleAsync(query);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task WhenUsernameRegistered_ItShouldReturnFalse()
        {
            var userManager = new Mock<IUserManager>();
            userManager.Setup(v => v.FindByNameAsync(username)).ReturnsAsync(new ApplicationUser());
            var handler = new GetUsernameAvailabilityQueryHandler(userManager.Object);
            var result = await handler.HandleAsync(query);
            Assert.IsFalse(result);
        }
    }
}