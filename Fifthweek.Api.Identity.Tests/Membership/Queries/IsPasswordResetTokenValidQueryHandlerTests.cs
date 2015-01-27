using System;

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
    public class IsPasswordResetTokenValidQueryHandlerTests
    {
        [TestMethod]
        public async Task WhenUserExistsAndTokenValid_ItShouldReturnTrue()
        {
            var userManager = new Mock<IUserManager>();
            var handler = new IsPasswordResetTokenValidQueryHandler(userManager.Object);

            userManager.Setup(v => v.FindByIdAsync(UserId)).ReturnsAsync(User);
            userManager.Setup(v => v.ValidatePasswordResetTokenAsync(User, Token)).ReturnsAsync(true);
            
            var result = await handler.HandleAsync(Query);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task WhenUserDoesNotExist_ItShouldReturnFalse()
        {
            var userManager = new Mock<IUserManager>();
            var handler = new IsPasswordResetTokenValidQueryHandler(userManager.Object);

            userManager.Setup(v => v.FindByIdAsync(UserId)).ReturnsAsync(null);

            var result = await handler.HandleAsync(Query);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task WhenUserExistsAndTokenInvalid_ItShouldReturnFalse()
        {
            var userManager = new Mock<IUserManager>();
            var handler = new IsPasswordResetTokenValidQueryHandler(userManager.Object);

            userManager.Setup(v => v.FindByIdAsync(UserId)).ReturnsAsync(User);
            userManager.Setup(v => v.ValidatePasswordResetTokenAsync(User, Token)).ReturnsAsync(false);

            var result = await handler.HandleAsync(Query);

            Assert.IsFalse(result);
        }

        private const string Token = "Token";
        private static readonly Guid UserId = Guid.Parse("7265bc4f-555e-4386-ad57-701dbdbc78bb");
        private static readonly IsPasswordResetTokenValidQuery Query = new IsPasswordResetTokenValidQuery(
            new UserId(UserId),
            Token);
        private static readonly FifthweekUser User = new FifthweekUser
        {
            Id = UserId
        };
    }
}