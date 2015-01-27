namespace Fifthweek.Api.FileManagement.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class FileSecurityTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());

        private Mock<IFileOwnership> fileOwnership;
        private FileSecurity target;

        [TestInitialize]
        public void Initialize()
        {
            this.fileOwnership = new Mock<IFileOwnership>();
            this.target = new FileSecurity(this.fileOwnership.Object);
        }

        [TestMethod]
        public async Task WhenAuthorizingUpdate_ItShouldAllowIfUserOwnsSubscription()
        {
            this.fileOwnership.Setup(_ => _.IsOwnerAsync(UserId, FileId)).ReturnsAsync(true);

            var result = await this.target.IsUsageAllowedAsync(UserId, FileId);

            Assert.IsTrue(result);

            await this.target.AssertUsageAllowedAsync(UserId, FileId);
        }

        [TestMethod]
        public async Task WhenAuthorizingUpdate_ItShouldForbidIfUserDoesNotOwnSubscription()
        {
            this.fileOwnership.Setup(_ => _.IsOwnerAsync(UserId, FileId)).ReturnsAsync(false);

            var result = await this.target.IsUsageAllowedAsync(UserId, FileId);

            Assert.IsFalse(result);

            Func<Task> badMethodCall = () => this.target.AssertUsageAllowedAsync(UserId, FileId);

            await badMethodCall.AssertExceptionAsync<UnauthorizedException>();
        }
    }
}
