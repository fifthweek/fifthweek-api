namespace Fifthweek.Api.Identity.Tests.Membership.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Payments.Services;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetAccountSettingsQueryHandlerTests
    {
        private static readonly Username Username = new Username("username");
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly ValidEmail Email = ValidEmail.Parse("test@testing.fifthweek.com");
        private static readonly int AccountBalance = 10;
        private static readonly PaymentStatus PaymentStatus = PaymentStatus.Retry2;
        private static readonly bool HasPaymentInformation = true;
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly DateTime FreePostTimestamp = DateTime.UtcNow.AddDays(-1);
        private static readonly int FreePostsRemaining = 3;

        private GetAccountSettingsQueryHandler target;
        private Mock<IGetAccountSettingsDbStatement> getAccountSettings;
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IFileInformationAggregator> fileInformationAggregator;
        private Mock<IGetFreePostTimestamp> getFreePostTimestamp;

        [TestInitialize]
        public void TestInitialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);

            this.getAccountSettings = new Mock<IGetAccountSettingsDbStatement>();
            this.fileInformationAggregator = new Mock<IFileInformationAggregator>();
            this.getFreePostTimestamp = new Mock<IGetFreePostTimestamp>();

            this.getFreePostTimestamp.Setup(v => v.Execute(Now)).Returns(FreePostTimestamp);

            this.target = new GetAccountSettingsQueryHandler(this.requesterSecurity.Object, this.getAccountSettings.Object, this.fileInformationAggregator.Object, this.getFreePostTimestamp.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new GetAccountSettingsQuery(Requester.Unauthenticated, UserId, Now));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenCalledWithUnauthorizedUserId_ItShouldThrowAnException()
        {
            this.getAccountSettings.Setup(v => v.ExecuteAsync(UserId, FreePostTimestamp, PostConstants.MaximumFreePostsPerInterval))
                .Throws(new Exception("This should not be called"));

            await this.target.HandleAsync(new GetAccountSettingsQuery(Requester, new UserId(Guid.NewGuid()), Now));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCalledWithNullQuery_ItThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        public async Task WhenCalled_ItShouldCallTheAccountRepository()
        {
            this.getAccountSettings.Setup(v => v.ExecuteAsync(UserId, FreePostTimestamp, PostConstants.MaximumFreePostsPerInterval))
                .ReturnsAsync(new GetAccountSettingsDbResult(Username, Email, null, AccountBalance, PaymentStatus, HasPaymentInformation, null, FreePostsRemaining))
                .Verifiable();

            var result = await this.target.HandleAsync(new GetAccountSettingsQuery(Requester, UserId, Now));

            this.getAccountSettings.Verify();

            var expectedResult = new GetAccountSettingsResult(Username, Email, null, AccountBalance, PaymentStatus, HasPaymentInformation, Payments.Constants.DefaultCreatorPercentage, null, FreePostsRemaining);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task WhenAccountSettingsHasAProfileImageFileId_ItShouldReturnTheBlobInformation()
        {
            const string ContainerName = "containerName";

            this.getAccountSettings.Setup(v => v.ExecuteAsync(UserId, FreePostTimestamp, PostConstants.MaximumFreePostsPerInterval))
                .ReturnsAsync(new GetAccountSettingsDbResult(Username, Email, FileId, AccountBalance, PaymentStatus, HasPaymentInformation, null, FreePostsRemaining));

            this.fileInformationAggregator.Setup(v => v.GetFileInformationAsync(null, FileId, FilePurposes.ProfileImage))
                .ReturnsAsync(new FileInformation(FileId, ContainerName));

            var result = await this.target.HandleAsync(new GetAccountSettingsQuery(Requester, UserId, Now));

            var expectedResult = new GetAccountSettingsResult(
                Username, 
                Email, 
                new FileInformation(FileId, ContainerName), 
                AccountBalance, 
                PaymentStatus,
                HasPaymentInformation, 
                Payments.Constants.DefaultCreatorPercentage, 
                null,
                FreePostsRemaining);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task WhenCreatorPercentageIsSetWithNoExpiry_ItShouldReturnTheCreatorPercentage()
        {
            var creatorPercentageOverride = new CreatorPercentageOverrideData(0.9m, null);

            this.getAccountSettings.Setup(v => v.ExecuteAsync(UserId, FreePostTimestamp, PostConstants.MaximumFreePostsPerInterval))
                .ReturnsAsync(new GetAccountSettingsDbResult(
                    Username, Email, null, AccountBalance, PaymentStatus, HasPaymentInformation, creatorPercentageOverride, FreePostsRemaining))
                .Verifiable();

            var result = await this.target.HandleAsync(new GetAccountSettingsQuery(Requester, UserId, Now));

            this.getAccountSettings.Verify();

            var expectedResult = new GetAccountSettingsResult(
                Username,
                Email,
                null,
                AccountBalance,
                PaymentStatus,
                HasPaymentInformation,
                creatorPercentageOverride.Percentage,
                null,
                FreePostsRemaining);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task WhenCreatorPercentageIsSetWithNonExpiredExpiry_ItShouldReturnTheCreatorPercentageWithNumberOfWeeks1()
        {
            var paymentProcessingStartDateInclusive = PaymentProcessingUtilities.GetPaymentProcessingStartDate(Now);
            var creatorPercentageOverride = new CreatorPercentageOverrideData(0.9m, paymentProcessingStartDateInclusive.AddDays(7));

            this.getAccountSettings.Setup(v => v.ExecuteAsync(UserId, FreePostTimestamp, PostConstants.MaximumFreePostsPerInterval))
                .ReturnsAsync(new GetAccountSettingsDbResult(
                    Username, Email, null, AccountBalance, PaymentStatus, HasPaymentInformation, creatorPercentageOverride, FreePostsRemaining))
                .Verifiable();

            var result = await this.target.HandleAsync(new GetAccountSettingsQuery(Requester, UserId, Now));

            this.getAccountSettings.Verify();

            var expectedResult = new GetAccountSettingsResult(
                Username,
                Email,
                null,
                AccountBalance,
                PaymentStatus,
                HasPaymentInformation,
                creatorPercentageOverride.Percentage,
                0,
                FreePostsRemaining);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task WhenCreatorPercentageIsSetWithNonExpiredExpiry_ItShouldReturnTheCreatorPercentageWithNumberOfWeeks2()
        {
            var paymentProcessingStartDateInclusive = PaymentProcessingUtilities.GetPaymentProcessingStartDate(Now);
            var creatorPercentageOverride = new CreatorPercentageOverrideData(0.9m, paymentProcessingStartDateInclusive.AddDays(14));

            this.getAccountSettings.Setup(v => v.ExecuteAsync(UserId, FreePostTimestamp, PostConstants.MaximumFreePostsPerInterval))
                .ReturnsAsync(new GetAccountSettingsDbResult(
                    Username, Email, null, AccountBalance, PaymentStatus, HasPaymentInformation, creatorPercentageOverride, FreePostsRemaining))
                .Verifiable();

            var result = await this.target.HandleAsync(new GetAccountSettingsQuery(Requester, UserId, Now));

            this.getAccountSettings.Verify();

            var expectedResult = new GetAccountSettingsResult(
                Username,
                Email,
                null,
                AccountBalance,
                PaymentStatus,
                HasPaymentInformation,
                creatorPercentageOverride.Percentage,
                1,
                FreePostsRemaining);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task WhenCreatorPercentageIsSetWithNonExpiredExpiry_ItShouldReturnTheCreatorPercentageWithNumberOfWeeks3()
        {
            var paymentProcessingStartDateInclusive = PaymentProcessingUtilities.GetPaymentProcessingStartDate(Now);
            var creatorPercentageOverride = new CreatorPercentageOverrideData(0.9m, paymentProcessingStartDateInclusive.AddDays(25));

            this.getAccountSettings.Setup(v => v.ExecuteAsync(UserId, FreePostTimestamp, PostConstants.MaximumFreePostsPerInterval))
                .ReturnsAsync(new GetAccountSettingsDbResult(
                    Username, Email, null, AccountBalance, PaymentStatus, HasPaymentInformation, creatorPercentageOverride, FreePostsRemaining))
                .Verifiable();

            var result = await this.target.HandleAsync(new GetAccountSettingsQuery(Requester, UserId, Now));

            this.getAccountSettings.Verify();

            var expectedResult = new GetAccountSettingsResult(
                Username, 
                Email,
                null,
                AccountBalance,
                PaymentStatus,
                HasPaymentInformation, 
                creatorPercentageOverride.Percentage,
                2,
                FreePostsRemaining);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task WhenCreatorPercentageIsSetWithExpiredExpiry_ItShouldReturnTheCreatorPercentageWithNumberOfWeeks1()
        {
            var paymentProcessingStartDateInclusive = PaymentProcessingUtilities.GetPaymentProcessingStartDate(Now);
            var creatorPercentageOverride = new CreatorPercentageOverrideData(0.9m, paymentProcessingStartDateInclusive.AddDays(7).AddTicks(-1));

            this.getAccountSettings.Setup(v => v.ExecuteAsync(UserId, FreePostTimestamp, PostConstants.MaximumFreePostsPerInterval))
                .ReturnsAsync(new GetAccountSettingsDbResult(
                    Username, Email, null, AccountBalance, PaymentStatus, HasPaymentInformation, creatorPercentageOverride, FreePostsRemaining))
                .Verifiable();

            var result = await this.target.HandleAsync(new GetAccountSettingsQuery(Requester, UserId, Now));

            this.getAccountSettings.Verify();

            var expectedResult = new GetAccountSettingsResult(
                Username, 
                Email, 
                null,
                AccountBalance,
                PaymentStatus,
                HasPaymentInformation, 
                Payments.Constants.DefaultCreatorPercentage,
                null,
                FreePostsRemaining);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
