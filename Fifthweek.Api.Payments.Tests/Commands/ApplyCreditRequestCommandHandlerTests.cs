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
            Requester, UserId, PositiveInt.Parse(10), PositiveInt.Parse(12));

        private static readonly InitializeCreditRequestResult InitializeResult = new InitializeCreditRequestResult(
            new TaxamoTransactionResult("key", new AmountInUsCents(10), new AmountInUsCents(20), new AmountInUsCents(30), 0.2m, "VAT", "GB", "England"),
            new UserPaymentOriginResult("stripeCustomerId", "GB", "12345", "1.1.1.1", "ttk", BillingStatus.Retry1));

        private static readonly StripeTransactionResult StripeTransactionResult =
            new StripeTransactionResult(DateTime.UtcNow, Guid.NewGuid(), "stripeChargeId");

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IFifthweekRetryOnTransientErrorHandler> retryOnTransientFailure;
        private Mock<IApplyStandardUserCredit> applyStandardUserCredit;
        private Mock<ICommitTestUserCreditToDatabase> commitTestUserCreditToDatabase;

        private ApplyCreditRequestCommandHandler target;

        [TestInitialize]
        public void Intialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.retryOnTransientFailure = new Mock<IFifthweekRetryOnTransientErrorHandler>(MockBehavior.Strict);
            this.applyStandardUserCredit = new Mock<IApplyStandardUserCredit>(MockBehavior.Strict);
            this.commitTestUserCreditToDatabase = new Mock<ICommitTestUserCreditToDatabase>(MockBehavior.Strict);

            this.requesterSecurity.SetupFor(Requester);

            this.target = new ApplyCreditRequestCommandHandler(
                this.requesterSecurity.Object,
                this.retryOnTransientFailure.Object,
                this.applyStandardUserCredit.Object,
                this.commitTestUserCreditToDatabase.Object);
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
                PositiveInt.Parse(3),
                PositiveInt.Parse(5)));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIsNotAuthenticated_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new ApplyCreditRequestCommand(
                Requester.Unauthenticated,
                UserId,
                PositiveInt.Parse(3),
                PositiveInt.Parse(5)));
        }

        [TestMethod]
        public async Task ItShouldCallApplyStandardUserCredit()
        {
            this.applyStandardUserCredit.Setup(v => v.ExecuteAsync(Command.UserId, Command.Amount, Command.ExpectedTotalAmount))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(Command);

            this.applyStandardUserCredit.Verify();
        }

        [TestMethod]
        public async Task WhenUserIsTestUser_ItShouldCallCommitTestUserCreditToDatabaseWithTransientErrorHandler()
        {
            var tasks = new List<Func<Task>>();

            this.requesterSecurity.Setup(v => v.IsInRoleAsync(Requester, FifthweekRole.TestUser)).ReturnsAsync(true);

            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task>>()))
                .Callback<Func<Task>>(tasks.Add)
                .Returns(Task.FromResult(0));

            await this.target.HandleAsync(Command);

            Assert.AreEqual(1, tasks.Count);

            this.commitTestUserCreditToDatabase.Setup(v => v.HandleAsync(
                UserId,
                Command.Amount))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await tasks[0]();
            this.commitTestUserCreditToDatabase.Verify();
        }
    }
}