using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Accounts.Tests.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Accounts.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;

    using Moq;

    [TestClass]
    public class GetAccountSettingsQueryHandlerTests
    {
        private readonly UserId userId = new UserId(Guid.NewGuid());

        private readonly FileId fileId = new FileId(Guid.NewGuid());
        
        private readonly ValidEmail email = ValidEmail.Parse("test@testing.fifthweek.com");

        private GetAccountSettingsQueryHandler target;

        private Mock<IAccountRepository> accountRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            this.accountRepository = new Mock<IAccountRepository>();

            this.target = new GetAccountSettingsQueryHandler(this.accountRepository.Object);
        }

        [TestMethod]
        public async Task WhenCalled_ItShouldCallTheAccountRepository()
        {
            this.accountRepository.Setup(v => v.GetAccountSettingsAsync(this.userId))
                .ReturnsAsync(new GetAccountSettingsResult(this.email, this.fileId))
                .Verifiable();

            var result = await this.target.HandleAsync(new GetAccountSettingsQuery(this.userId, this.userId));

            this.accountRepository.Verify();

            Assert.IsNotNull(result);
            Assert.AreEqual(this.email, result.Email);
            Assert.AreEqual(this.fileId, result.ProfileImageFileId);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenCalledWithUnauthorizedUserId_ItShouldThrowAnException()
        {
            this.accountRepository.Setup(v => v.GetAccountSettingsAsync(this.userId))
                .Throws(new Exception("This should not be called"));

            await this.target.HandleAsync(new GetAccountSettingsQuery(this.userId, new UserId(Guid.NewGuid())));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCalledWithNullQuery_ItThrowAnException()
        {
            await this.target.HandleAsync(null);
        }
    }
}
