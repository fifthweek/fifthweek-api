namespace Fifthweek.Api.Payments.Tests.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Payments.Queries;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Azure;
    using Fifthweek.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.WindowsAzure.Storage;

    using Moq;

    [TestClass]
    public class BlockPaymentProcessingCommandHandlerTests
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly Requester Requester = Requester.Authenticated(UserId);

        private static readonly BlockPaymentProcessingCommand CommandWithLeaseId =
            new BlockPaymentProcessingCommand(Requester, Guid.NewGuid().ToString(), null);

        private static readonly BlockPaymentProcessingCommand CommandWithProposedLeaseId =
            new BlockPaymentProcessingCommand(Requester, null, Guid.NewGuid().ToString());

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IBlobLeaseHelper> blobLeaseHelper;

        private BlockPaymentProcessingCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.blobLeaseHelper = new Mock<IBlobLeaseHelper>(MockBehavior.Strict);

            this.requesterSecurity.SetupFor(Requester);

            this.target = new BlockPaymentProcessingCommandHandler(
                this.requesterSecurity.Object,
                this.blobLeaseHelper.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCommandIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIsNotAdministrator_ItShouldThrowAnException()
        {
            this.requesterSecurity.Setup(v => v.AssertInRoleAsync(Requester, FifthweekRole.Administrator))
                .Throws(new UnauthorizedException());

            await this.target.HandleAsync(CommandWithLeaseId);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIsNotAuthenticated_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new BlockPaymentProcessingCommand(
                Requester.Unauthenticated,
                Guid.NewGuid().ToString(),
                null));
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenNoLeaseIds_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new BlockPaymentProcessingCommand(
                Requester,
                null,
                null));
        }

        [TestMethod]
        public async Task WhenNewLease_ItShouldAcquireNewLease()
        {
            var blob = new Mock<ICloudBlockBlob>(MockBehavior.Strict);

            this.blobLeaseHelper.Setup(v => v.GetLeaseBlob(Fifthweek.Payments.Shared.Constants.ProcessPaymentsLeaseObjectName))
                .Returns(blob.Object);

            blob.Setup(v => v.AcquireLeaseAsync(BlockPaymentProcessingCommandHandler.LeaseLength, CommandWithProposedLeaseId.ProposedLeaseId, CancellationToken.None))
                .ReturnsAsync(string.Empty).Verifiable();

            await this.target.HandleAsync(CommandWithProposedLeaseId);

            blob.Verify();
        }

        [TestMethod]
        public async Task WhenLeaseConflict_ItShouldThrowLeaseConflictException()
        {
            var blob = new Mock<ICloudBlockBlob>(MockBehavior.Strict);

            this.blobLeaseHelper.Setup(v => v.GetLeaseBlob(Fifthweek.Payments.Shared.Constants.ProcessPaymentsLeaseObjectName))
                .Returns(blob.Object);

            var exception = new DivideByZeroException();
            blob.Setup(v => v.AcquireLeaseAsync(BlockPaymentProcessingCommandHandler.LeaseLength, CommandWithProposedLeaseId.ProposedLeaseId, CancellationToken.None))
                .Throws(exception);

            this.blobLeaseHelper.Setup(v => v.IsLeaseConflictException(exception)).Returns(true);

            await ExpectedException.AssertExceptionAsync<LeaseConflictException>(
                () => this.target.HandleAsync(CommandWithProposedLeaseId));
        }

        [TestMethod]
        public async Task WhenNonLeaseConflictError_ItShouldPropagateException()
        {
            var blob = new Mock<ICloudBlockBlob>(MockBehavior.Strict);

            this.blobLeaseHelper.Setup(v => v.GetLeaseBlob(Fifthweek.Payments.Shared.Constants.ProcessPaymentsLeaseObjectName))
                .Returns(blob.Object);

            var exception = new DivideByZeroException();
            blob.Setup(v => v.AcquireLeaseAsync(BlockPaymentProcessingCommandHandler.LeaseLength, CommandWithProposedLeaseId.ProposedLeaseId, CancellationToken.None))
                .Throws(exception);

            this.blobLeaseHelper.Setup(v => v.IsLeaseConflictException(exception)).Returns(false);

            await ExpectedException.AssertExceptionAsync<DivideByZeroException>(
                () => this.target.HandleAsync(CommandWithProposedLeaseId));
        }

        [TestMethod]
        public async Task WhenExistingLease_ItShouldRenewLease()
        {
            var blob = new Mock<ICloudBlockBlob>(MockBehavior.Strict);

            this.blobLeaseHelper.Setup(v => v.GetLeaseBlob(Fifthweek.Payments.Shared.Constants.ProcessPaymentsLeaseObjectName))
                .Returns(blob.Object);

            blob.Setup(v => v.RenewLeaseAsync(It.Is<AccessCondition>(ac => ac.LeaseId == CommandWithLeaseId.LeaseId), CancellationToken.None))
                .Returns(Task.FromResult(0)).Verifiable();

            await this.target.HandleAsync(CommandWithLeaseId);

            blob.Verify();
        }
    }
}