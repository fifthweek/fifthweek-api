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
    public class CreateCreditRefundCommandHandlerTests
    {
        private static readonly UserId EnactingUserId = UserId.Random();
        private static readonly UserId UserId = UserId.Random();
        private static readonly Requester Requester = Requester.Authenticated(EnactingUserId);

        private static readonly CreateCreditRefundCommand Command = new CreateCreditRefundCommand(
            Requester, 
            TransactionReference.Random(),
            DateTime.UtcNow,
            PositiveInt.Parse(123),
            RefundCreditReason.RequestedByCustomer,
            "comment");

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<ICreateCreditRefund> createCreditRefund;
        private Mock<IUpdateAccountBalancesDbStatement> updateAccountBalances;

        private CreateCreditRefundCommandHandler target;

        [TestInitialize]
        public void Intialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.createCreditRefund = new Mock<ICreateCreditRefund>(MockBehavior.Strict);
            this.updateAccountBalances = new Mock<IUpdateAccountBalancesDbStatement>(MockBehavior.Strict);

            this.requesterSecurity.SetupFor(Requester);

            this.target = new CreateCreditRefundCommandHandler(
                this.requesterSecurity.Object,
                this.createCreditRefund.Object,
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
            await this.target.HandleAsync(new CreateCreditRefundCommand(
                Requester.Unauthenticated,
                Command.TransactionReference,
                Command.Timestamp,
                Command.RefundCreditAmount,
                Command.Reason,
                Command.Comment));
        }

        [TestMethod]
        public async Task ItShouldDeleteUserPaymentInformation()
        {
            this.createCreditRefund.Setup(v => v.ExecuteAsync(EnactingUserId, Command.TransactionReference, Command.Timestamp, Command.RefundCreditAmount, Command.Reason, Command.Comment))
                .ReturnsAsync(UserId)
                .Verifiable();

            this.updateAccountBalances.Setup(v => v.ExecuteAsync(UserId, Command.Timestamp))
                .ReturnsAsync(null)
                .Verifiable();
            
            await this.target.HandleAsync(Command);

            this.createCreditRefund.Verify();
            this.updateAccountBalances.Verify();
        }
    }
}