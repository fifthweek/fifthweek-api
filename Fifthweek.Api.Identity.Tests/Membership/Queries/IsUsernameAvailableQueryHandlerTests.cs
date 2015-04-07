namespace Fifthweek.Api.Identity.Tests.Membership.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class IsUsernameAvailableQueryHandlerTests
    {
        private static readonly ValidUsername Username = ValidUsername.Parse("lawrence");
        private static readonly IsUsernameAvailableQuery Query = new IsUsernameAvailableQuery(Username);

        private Mock<IUserManager> userManager;
        private Mock<IReservedUsernameService> reservedUsernames;

        private IsUsernameAvailableQueryHandler target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.userManager = new Mock<IUserManager>();
            this.reservedUsernames = new Mock<IReservedUsernameService>();
            this.target = new IsUsernameAvailableQueryHandler(this.userManager.Object, this.reservedUsernames.Object);
        }


        [TestMethod]
        public async Task WhenUsernameNotRegisteredOrReservedItShouldReturnTrue()
        {
            this.reservedUsernames.Setup(v => v.IsReserved(Username)).Returns(false);
            this.userManager.Setup(v => v.FindByNameAsync(Username.Value)).ReturnsAsync(null);
            var result = await this.target.HandleAsync(Query);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task WhenUsernameReserved_ItShouldReturnFalse()
        {
            this.reservedUsernames.Setup(v => v.IsReserved(Username)).Returns(true);
            this.userManager.Setup(v => v.FindByNameAsync(Username.Value)).ReturnsAsync(null);
            var result = await this.target.HandleAsync(Query);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task WhenUsernameRegistered_ItShouldReturnFalse()
        {
            this.reservedUsernames.Setup(v => v.IsReserved(Username)).Returns(false);
            this.userManager.Setup(v => v.FindByNameAsync(Username.Value)).ReturnsAsync(new FifthweekUser());
            var result = await this.target.HandleAsync(Query);
            Assert.IsFalse(result);
        }
    }
}