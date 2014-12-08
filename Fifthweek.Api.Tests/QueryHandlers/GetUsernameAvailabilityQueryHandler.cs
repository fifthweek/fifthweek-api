namespace Fifthweek.Api.Tests.QueryHandlers
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Queries;
    using Fifthweek.Api.QueryHandlers;
    using Fifthweek.Api.Repositories;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetUsernameAvailabilityQueryHandlerTests
    {
        private static readonly string username = "lawrence";
        private static readonly GetUsernameAvailabilityQuery query = new GetUsernameAvailabilityQuery(username);

        [TestMethod]
        public async Task ItShouldReturnTrueWhenUsernameNotRegistered()
        {
            var userManager = new Mock<IUserManager>();
            userManager.Setup(v => v.FindByNameAsync(username)).ReturnsAsync(null);
            var handler = new GetUsernameAvailabilityQueryHandler(userManager.Object);
            var result = await handler.HandleAsync(query);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task ItShouldReturnFalseWhenUsernameRegistered()
        {
            var userManager = new Mock<IUserManager>();
            userManager.Setup(v => v.FindByNameAsync(username)).ReturnsAsync(new ApplicationUser());
            var handler = new GetUsernameAvailabilityQueryHandler(userManager.Object);
            var result = await handler.HandleAsync(query);
            Assert.IsFalse(result);
        }
    }
}