namespace Fifthweek.Api.Payments.Tests.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ApplyCreditRequestCommandHandlerTests
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly Requester Requester = Requester.Authenticated(UserId);

        private static readonly ApplyCreditRequestCommand Command = new ApplyCreditRequestCommand(
            Requester, UserId, PositiveInt.Parse(TopUpUserAccountsWithCredit.MinimumPaymentAmount), PositiveInt.Parse(TopUpUserAccountsWithCredit.MinimumPaymentAmount + 100));

        private static readonly InitializeCreditRequestResult InitializeResult = new InitializeCreditRequestResult(
            new TaxamoTransactionResult("key", new AmountInMinorDenomination(10), new AmountInMinorDenomination(20), new AmountInMinorDenomination(30), 0.2m, "VAT", "GB", "England"),
            new UserPaymentOriginResult("stripeCustomerId", PaymentOriginKeyType.Stripe, "GB", "12345", "1.1.1.1", "ttk", PaymentStatus.Retry1));

        private static readonly StripeTransactionResult StripeTransactionResult =
            new StripeTransactionResult(DateTime.UtcNow, Guid.NewGuid(), "stripeChargeId");

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IFifthweekRetryOnTransientErrorHandler> retryOnTransientFailure;
        private Mock<IApplyUserCredit> applyUserCredit;
        private Mock<IFailPaymentStatusDbStatement> failPaymentStatus;

        private ApplyCreditRequestCommandHandler target;

        [TestInitialize]
        public void Intialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.retryOnTransientFailure = new Mock<IFifthweekRetryOnTransientErrorHandler>(MockBehavior.Strict);
            this.applyUserCredit = new Mock<IApplyUserCredit>(MockBehavior.Strict);
            this.failPaymentStatus = new Mock<IFailPaymentStatusDbStatement>(MockBehavior.Strict);

            this.requesterSecurity.SetupFor(Requester);

            this.target = new ApplyCreditRequestCommandHandler(
                this.requesterSecurity.Object,
                this.retryOnTransientFailure.Object,
                this.applyUserCredit.Object,
                this.failPaymentStatus.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCommandIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIsNotAuthorized_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new ApplyCreditRequestCommand(
                Requester,
                UserId.Random(),
                Command.Amount,
                Command.ExpectedTotalAmount));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIsNotAuthenticated_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new ApplyCreditRequestCommand(
                Requester.Unauthenticated,
                UserId,
                Command.Amount,
                Command.ExpectedTotalAmount));
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenAmountIsLessThanMinimumPaymentAmount_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new ApplyCreditRequestCommand(
                Requester,
                UserId,
                PositiveInt.Parse(TopUpUserAccountsWithCredit.MinimumPaymentAmount - 1),
                Command.ExpectedTotalAmount));
        }

        [TestMethod]
        public async Task ItShouldCallApplyStandardUserCredit()
        {
            this.applyUserCredit.Setup(v => v.ExecuteAsync(Command.UserId, Command.Amount, Command.ExpectedTotalAmount, UserType.StandardUser))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(Command);

            this.applyUserCredit.Verify();
        }

        [TestMethod]
        public async Task WhenCreditCardFailedExceptionOccurs_ItShouldSetPaymentStatusToFailed()
        {
            var tasks = new List<Func<Task>>();

            this.applyUserCredit.Setup(v => v.ExecuteAsync(Command.UserId, Command.Amount, Command.ExpectedTotalAmount, UserType.StandardUser))
                .Throws(new StripeChargeFailedException(new DivideByZeroException()));

            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task>>()))
                .Callback<Func<Task>>(tasks.Add)
                .Returns(Task.FromResult(0));

            await ExpectedException.AssertExceptionAsync<StripeChargeFailedException>(
                () => this.target.HandleAsync(Command));

            Assert.AreEqual(1, tasks.Count);

            this.failPaymentStatus.Setup(v => v.ExecuteAsync(UserId))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await tasks[0]();
            this.failPaymentStatus.Verify();
        }

        [TestMethod]
        public async Task WhenExceptionOccurs_ItShouldPropagate()
        {
            var tasks = new List<Func<Task>>();

            this.applyUserCredit.Setup(v => v.ExecuteAsync(Command.UserId, Command.Amount, Command.ExpectedTotalAmount, UserType.StandardUser))
                .Throws(new DivideByZeroException());

            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task>>()))
                .Callback<Func<Task>>(tasks.Add)
                .Returns(Task.FromResult(0));

            await ExpectedException.AssertExceptionAsync<DivideByZeroException>(
                () => this.target.HandleAsync(Command));

            Assert.AreEqual(0, tasks.Count);
        }

        [TestMethod]
        public async Task WhenUserIsTestUser_ItShouldCallApplyUserCreditAsTestUser()
        {
            this.requesterSecurity.Setup(v => v.IsInRoleAsync(Requester, FifthweekRole.TestUser)).ReturnsAsync(true);

            this.applyUserCredit.Setup(v => v.ExecuteAsync(Command.UserId, Command.Amount, Command.ExpectedTotalAmount, UserType.TestUser))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(Command);

            this.applyUserCredit.Verify();
        }
    }
}