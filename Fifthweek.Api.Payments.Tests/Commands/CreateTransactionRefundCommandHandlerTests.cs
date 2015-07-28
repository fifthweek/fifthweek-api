namespace Fifthweek.Api.Payments.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CreateTransactionRefundCommandHandlerTests
    {
        private static readonly UserId EnactingUserId = UserId.Random();
        private static readonly UserId UserId1 = UserId.Random();
        private static readonly UserId UserId2 = UserId.Random();
        private static readonly Requester Requester = Requester.Authenticated(EnactingUserId);

        private static readonly CreateTransactionRefundCommand Command = new CreateTransactionRefundCommand(
            Requester,
            TransactionReference.Random(),
            DateTime.UtcNow,
            "comment");

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<ICreateTransactionRefund> createTransactionRefund;
        private Mock<IUpdateAccountBalancesDbStatement> updateAccountBalances;

        private CreateTransactionRefundCommandHandler target;

        [TestInitialize]
        public void Intialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.createTransactionRefund = new Mock<ICreateTransactionRefund>(MockBehavior.Strict);
            this.updateAccountBalances = new Mock<IUpdateAccountBalancesDbStatement>(MockBehavior.Strict);

            this.requesterSecurity.SetupFor(Requester);

            this.target = new CreateTransactionRefundCommandHandler(
                this.requesterSecurity.Object,
                this.createTransactionRefund.Object,
                this.updateAccountBalances.Object);
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

            await this.target.HandleAsync(Command);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIsNotAuthenticated_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new CreateTransactionRefundCommand(
                Requester.Unauthenticated,
                Command.TransactionReference,
                Command.Timestamp,
                Command.Comment));
        }

        [TestMethod]
        public async Task ItShouldDeleteUserPaymentInformation()
        {
            this.createTransactionRefund.Setup(v => v.ExecuteAsync(EnactingUserId, Command.TransactionReference, Command.Timestamp, Command.Comment))
                .ReturnsAsync(new CreateTransactionRefund.CreateTransactionRefundResult(UserId1, UserId2))
                .Verifiable();

            this.updateAccountBalances.Setup(v => v.ExecuteAsync(UserId1, Command.Timestamp))
                .ReturnsAsync(null)
                .Verifiable();
            this.updateAccountBalances.Setup(v => v.ExecuteAsync(UserId2, Command.Timestamp))
                .ReturnsAsync(null)
                .Verifiable();

            await this.target.HandleAsync(Command);

            this.createTransactionRefund.Verify();
            this.updateAccountBalances.Verify();
        }
    }
}