namespace Fifthweek.Api.Subscriptions.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Subscriptions.Commands;

    using Microsoft.AspNet.Identity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PromoteNewUserToCreatorCommandHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly PromoteNewUserToCreatorCommand Command = new PromoteNewUserToCreatorCommand(UserId);
        private Mock<IUserManager> userManager;
        private PromoteNewUserToCreatorCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.userManager = new Mock<IUserManager>();
            this.target = new PromoteNewUserToCreatorCommandHandler(this.userManager.Object);
        }

        [TestMethod]
        public async Task ItShouldAddTheUserToCreatorRole()
        {
            this.userManager.Setup(_ => _.AddToRoleAsync(UserId.Value, FifthweekRole.Creator)).ReturnsAsync(new IdentityResult()).Verifiable();

            await this.target.HandleAsync(Command);

            this.userManager.Verify();
        }
    }
}