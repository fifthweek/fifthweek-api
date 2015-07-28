namespace Fifthweek.Payments.Tests.Services.Refunds
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Services.Refunds.Stripe;
    using Fifthweek.Payments.Services.Refunds.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CreateCreditRefundTests
    {
        private static readonly UserId EnactingUserId = UserId.Random();
        private static readonly UserId UserId = UserId.Random();
        private static readonly TransactionReference TransactionReference = TransactionReference.Random();
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly PositiveInt RefundCreditAmount = PositiveInt.Parse(50);
        private static readonly RefundCreditReason Reason = RefundCreditReason.Fraudulent;
        private static readonly string Comment = "comment";

        private static readonly GetCreditTransactionDbStatement.GetCreditTransactionResult TransactionResult = new GetCreditTransactionDbStatement.GetCreditTransactionResult(
            UserId,
            "stripeChargeId",
            "taxamoTransactionKey",
            100,
            RefundCreditAmount.Value);

        private static readonly CreateTaxamoRefund.TaxamoRefundResult TaxamoResult = new CreateTaxamoRefund.TaxamoRefundResult(
            PositiveInt.Parse(60),
            NonNegativeInt.Parse(10));

        private Mock<IFifthweekRetryOnTransientErrorHandler> retryOnTransientFailure;
        private Mock<IGetCreditTransactionDbStatement> getCreditTransaction;
        private Mock<ICreateTaxamoRefund> createTaxamoRefund;
        private Mock<ICreateStripeRefund> createStripeRefund;
        private Mock<ICreateCreditRefundDbStatement> createCreditRefundDbStatement;

        private CreateCreditRefund target;

        [TestInitialize]
        public void Initialize()
        {
            this.retryOnTransientFailure = new Mock<IFifthweekRetryOnTransientErrorHandler>(MockBehavior.Strict);
            this.getCreditTransaction = new Mock<IGetCreditTransactionDbStatement>(MockBehavior.Strict);
            this.createTaxamoRefund = new Mock<ICreateTaxamoRefund>(MockBehavior.Strict);
            this.createStripeRefund = new Mock<ICreateStripeRefund>(MockBehavior.Strict);
            this.createCreditRefundDbStatement = new Mock<ICreateCreditRefundDbStatement>(MockBehavior.Strict);

            this.target = new CreateCreditRefund(
                this.retryOnTransientFailure.Object,
                this.getCreditTransaction.Object,
                this.createTaxamoRefund.Object,
                this.createStripeRefund.Object,
                this.createCreditRefundDbStatement.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, TransactionReference, Now, RefundCreditAmount, Reason, Comment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenTransactionReferenceIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(EnactingUserId, null, Now, RefundCreditAmount, Reason, Comment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenRefundCreditAmountIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(EnactingUserId, TransactionReference, Now, null, Reason, Comment);
        }

        [TestMethod]
        public async Task ItShouldCallEachPhaseUsingTransientErrorHandler()
        {
            var tasks = new List<Func<Task>>();

            this.SetupRetryOnTransientFailureTasks(tasks);

            var result = await this.target.ExecuteAsync(EnactingUserId, TransactionReference, Now, RefundCreditAmount, Reason, Comment);

            Assert.AreEqual(UserId, result);
            Assert.AreEqual(4, tasks.Count);

            this.getCreditTransaction.Setup(v => v.ExecuteAsync(TransactionReference))
                .ReturnsAsync(TransactionResult).Verifiable();
            await tasks[0]();
            this.getCreditTransaction.Verify();

            this.createTaxamoRefund.Setup(v => v.ExecuteAsync(TransactionResult.TaxamoTransactionKey, RefundCreditAmount, UserType.StandardUser))
                .ReturnsAsync(TaxamoResult).Verifiable();
            await tasks[1]();
            this.createTaxamoRefund.Verify();

            this.createCreditRefundDbStatement.Setup(v => v.ExecuteAsync(
                EnactingUserId,
                TransactionResult.UserId,
                Now,
                TaxamoResult.TotalRefundAmount,
                RefundCreditAmount,
                TransactionReference,
                TransactionResult.StripeChargeId,
                TransactionResult.TaxamoTransactionKey,
                Comment))
                .Returns(Task.FromResult(0))
                .Verifiable();
            await tasks[2]();
            this.createCreditRefundDbStatement.Verify();

            this.createStripeRefund.Setup(v => v.ExecuteAsync(
                EnactingUserId,
                TransactionResult.StripeChargeId,
                TaxamoResult.TotalRefundAmount,
                Reason,
                UserType.StandardUser))
                .Returns(Task.FromResult(0))
                .Verifiable();
            await tasks[3]();
            this.createStripeRefund.Verify();
        }

        [TestMethod]
        public async Task WhenCreateStripeRefundFails_ItShouldWrapException()
        {
            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task<GetCreditTransactionDbStatement.GetCreditTransactionResult>>>()))
                .ReturnsAsync(TransactionResult);

            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task<CreateTaxamoRefund.TaxamoRefundResult>>>()))
                .ReturnsAsync(TaxamoResult);

            this.retryOnTransientFailure.SetupSequence(v => v.HandleAsync(It.IsAny<Func<Task>>()))
                .Returns(Task.FromResult(0))
                .Throws(new DivideByZeroException());

            await ExpectedException.AssertExceptionAsync<FailedToRefundCreditException>(
                () => this.target.ExecuteAsync(EnactingUserId, TransactionReference, Now, RefundCreditAmount, Reason, Comment));

            this.retryOnTransientFailure.Verify(v => v.HandleAsync(It.IsAny<Func<Task>>()), Times.Exactly(2));
        }

        [TestMethod]
        public async Task WhenCreateCreditRefundFails_ItShouldWrapException()
        {
            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task<GetCreditTransactionDbStatement.GetCreditTransactionResult>>>()))
                .ReturnsAsync(TransactionResult);

            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task<CreateTaxamoRefund.TaxamoRefundResult>>>()))
                .ReturnsAsync(TaxamoResult);

            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task>>()))
                .Throws(new DivideByZeroException());

            await ExpectedException.AssertExceptionAsync<FailedToRefundCreditException>(
                () => this.target.ExecuteAsync(EnactingUserId, TransactionReference, Now, RefundCreditAmount, Reason, Comment));

            this.retryOnTransientFailure.Verify(v => v.HandleAsync(It.IsAny<Func<Task>>()), Times.Once());
        }

        [TestMethod]
        public async Task WhenTransactionNotFound_ItShouldThrowException()
        {
            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task<GetCreditTransactionDbStatement.GetCreditTransactionResult>>>()))
                .ReturnsAsync(null);

            await ExpectedException.AssertExceptionAsync<BadRequestException>(
                () => this.target.ExecuteAsync(EnactingUserId, TransactionReference, Now, RefundCreditAmount, Reason, Comment));
        }

        [TestMethod]
        public async Task WhenNotEnoughRemainingCredit_ItShouldThrowException()
        {
            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task<GetCreditTransactionDbStatement.GetCreditTransactionResult>>>()))
                .ReturnsAsync(new GetCreditTransactionDbStatement.GetCreditTransactionResult(
                    UserId.Random(),
                    "stripeChargeId",
                    "taxamoTransactionKey",
                    100,
                    RefundCreditAmount.Value - 1));

            await ExpectedException.AssertExceptionAsync<BadRequestException>(
                () => this.target.ExecuteAsync(EnactingUserId, TransactionReference, Now, RefundCreditAmount, Reason, Comment));
        }

        private void SetupRetryOnTransientFailureTasks(List<Func<Task>> tasks)
        {
            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task<GetCreditTransactionDbStatement.GetCreditTransactionResult>>>()))
                .Callback<Func<Task>>(tasks.Add)
                .ReturnsAsync(TransactionResult);

            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task<CreateTaxamoRefund.TaxamoRefundResult>>>()))
                .Callback<Func<Task>>(tasks.Add)
                .ReturnsAsync(TaxamoResult);

            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task>>()))
                .Callback<Func<Task>>(tasks.Add)
                .Returns(Task.FromResult(0));
        }
    }
}