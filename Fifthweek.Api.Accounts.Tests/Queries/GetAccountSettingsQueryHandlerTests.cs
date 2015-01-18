namespace Fifthweek.Api.Accounts.Tests.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Accounts.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetAccountSettingsQueryHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly ValidEmail Email = ValidEmail.Parse("test@testing.fifthweek.com");

        private GetAccountSettingsQueryHandler target;
        private Mock<IAccountRepository> accountRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            // Give potentially side-effecting components strict mock behaviour.
            this.accountRepository = new Mock<IAccountRepository>(MockBehavior.Strict);

            this.target = new GetAccountSettingsQueryHandler(this.accountRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new GetAccountSettingsQuery(Requester.Unauthenticated, UserId));
        }

        [TestMethod]
        public async Task WhenCalled_ItShouldCallTheAccountRepository()
        {
            this.accountRepository.Setup(v => v.GetAccountSettingsAsync(UserId))
                .ReturnsAsync(new GetAccountSettingsResult(Email, FileId))
                .Verifiable();

            var result = await this.target.HandleAsync(new GetAccountSettingsQuery(Requester, UserId));

            this.accountRepository.Verify();

            Assert.IsNotNull(result);
            Assert.AreEqual(Email, result.Email);
            Assert.AreEqual(FileId, result.ProfileImageFileId);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenCalledWithUnauthorizedUserId_ItShouldThrowAnException()
        {
            this.accountRepository.Setup(v => v.GetAccountSettingsAsync(UserId))
                .Throws(new Exception("This should not be called"));

            await this.target.HandleAsync(new GetAccountSettingsQuery(Requester, new UserId(Guid.NewGuid())));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCalledWithNullQuery_ItThrowAnException()
        {
            await this.target.HandleAsync(null);
        }
    }
}
