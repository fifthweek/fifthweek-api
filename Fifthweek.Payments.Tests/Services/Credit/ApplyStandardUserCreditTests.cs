namespace Fifthweek.Payments.Tests.Services.Credit
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ApplyStandardUserCreditTests
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly PositiveInt Amount = PositiveInt.Parse(10);
        private static readonly PositiveInt ExpectedTotalAmount = PositiveInt.Parse(12);

        private static readonly InitializeCreditRequestResult InitializeResult = new InitializeCreditRequestResult(
            new TaxamoTransactionResult("key", new AmountInUsCents(10), new AmountInUsCents(20), new AmountInUsCents(30), 0.2m, "VAT", "GB", "England"),
            new UserPaymentOriginResult("stripeCustomerId", "GB", "12345", "1.1.1.1", "ttk", BillingStatus.Retry1));

        private static readonly StripeTransactionResult StripeTransactionResult =
            new StripeTransactionResult(DateTime.UtcNow, Guid.NewGuid(), "stripeChargeId");

        private Mock<IInitializeCreditRequest> initializeCreditRequest;
        private Mock<IPerformCreditRequest> performCreditRequest;
        private Mock<ICommitCreditToDatabase> commitCreditToDatabase;
        private Mock<IFifthweekRetryOnTransientErrorHandler> retryOnTransientFailure;
        private Mock<ICommitTaxamoTransaction> commitTaxamoTransaction;

        private ApplyStandardUserCredit target;

        [TestInitialize]
        public void Intialize()
        {
            this.initializeCreditRequest = new Mock<IInitializeCreditRequest>(MockBehavior.Strict);
            this.performCreditRequest = new Mock<IPerformCreditRequest>(MockBehavior.Strict);
            this.commitCreditToDatabase = new Mock<ICommitCreditToDatabase>(MockBehavior.Strict);
            this.retryOnTransientFailure = new Mock<IFifthweekRetryOnTransientErrorHandler>(MockBehavior.Strict);
            this.commitTaxamoTransaction = new Mock<ICommitTaxamoTransaction>(MockBehavior.Strict);

            this.target = new ApplyStandardUserCredit(
                this.initializeCreditRequest.Object,
                this.performCreditRequest.Object,
                this.commitCreditToDatabase.Object,
                this.retryOnTransientFailure.Object,
                this.commitTaxamoTransaction.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, Amount, ExpectedTotalAmount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAmountIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(UserId, null, ExpectedTotalAmount);
        }

        [TestMethod]
        public async Task ItShouldCallEachPhaseUsingTransientErrorHandler()
        {
            var tasks = new List<Func<Task>>();

            this.SetupRetryOnTransientFailureTasks(tasks);

            await this.target.ExecuteAsync(UserId, Amount, ExpectedTotalAmount);

            Assert.AreEqual(4, tasks.Count);

            this.initializeCreditRequest.Setup(v => v.HandleAsync(UserId, Amount, ExpectedTotalAmount))
                .ReturnsAsync(InitializeResult).Verifiable();
            await tasks[0]();
            this.initializeCreditRequest.Verify();

            this.performCreditRequest.Setup(v => v.HandleAsync(UserId, InitializeResult.TaxamoTransaction, InitializeResult.Origin))
                .ReturnsAsync(StripeTransactionResult).Verifiable();
            await tasks[1]();
            this.performCreditRequest.Verify();

            this.commitCreditToDatabase.Setup(v => v.HandleAsync(
                UserId,
                InitializeResult.TaxamoTransaction,
                InitializeResult.Origin,
                StripeTransactionResult))
                .Returns(Task.FromResult(0))
                .Verifiable();
            await tasks[2]();
            this.commitCreditToDatabase.Verify();

            this.commitTaxamoTransaction.Setup(v => v.ExecuteAsync(InitializeResult.TaxamoTransaction.Key))
                .Returns(Task.FromResult(0))
                .Verifiable();
            await tasks[3]();
            this.commitTaxamoTransaction.Verify();
        }

        [TestMethod]
        public async Task WhenExpectedTotalAmountIsNull_ItShouldCallEachPhaseUsingTransientErrorHandler()
        {
            var tasks = new List<Func<Task>>();

            this.SetupRetryOnTransientFailureTasks(tasks);

            await this.target.ExecuteAsync(UserId, Amount, null);

            Assert.AreEqual(4, tasks.Count);

            this.initializeCreditRequest.Setup(v => v.HandleAsync(UserId, Amount, null))
                .ReturnsAsync(InitializeResult).Verifiable();
            await tasks[0]();
            this.initializeCreditRequest.Verify();

            this.performCreditRequest.Setup(v => v.HandleAsync(UserId, InitializeResult.TaxamoTransaction, InitializeResult.Origin))
                .ReturnsAsync(StripeTransactionResult).Verifiable();
            await tasks[1]();
            this.performCreditRequest.Verify();

            this.commitCreditToDatabase.Setup(v => v.HandleAsync(
                UserId,
                InitializeResult.TaxamoTransaction,
                InitializeResult.Origin,
                StripeTransactionResult))
                .Returns(Task.FromResult(0))
                .Verifiable();
            await tasks[2]();
            this.commitCreditToDatabase.Verify();

            this.commitTaxamoTransaction.Setup(v => v.ExecuteAsync(InitializeResult.TaxamoTransaction.Key))
                .Returns(Task.FromResult(0))
                .Verifiable();
            await tasks[3]();
            this.commitTaxamoTransaction.Verify();
        }

        [TestMethod]
        public async Task WhenInitiateThrowsAnException_ItShouldStopProcessingAndPropagate()
        {
            var tasks = new List<Func<Task>>();

            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task<InitializeCreditRequestResult>>>()))
                .Callback<Func<Task>>(tasks.Add)
                .ThrowsAsync(new DivideByZeroException());

            await ExpectedException.AssertExceptionAsync<DivideByZeroException>(
                () => this.target.ExecuteAsync(UserId, Amount, ExpectedTotalAmount));

            Assert.AreEqual(1, tasks.Count);
        }

        [TestMethod]
        public async Task WhenPerformThrowsAnException_ItShouldStopProcessingAndPropagate()
        {
            var tasks = new List<Func<Task>>();

            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task<InitializeCreditRequestResult>>>()))
                .Callback<Func<Task>>(tasks.Add)
                .ReturnsAsync(InitializeResult);

            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task<StripeTransactionResult>>>()))
                .Callback<Func<Task>>(tasks.Add)
                .ThrowsAsync(new DivideByZeroException());

            await ExpectedException.AssertExceptionAsync<DivideByZeroException>(
                () => this.target.ExecuteAsync(UserId, Amount, ExpectedTotalAmount));

            Assert.AreEqual(2, tasks.Count);
        }

        [TestMethod]
        public async Task WhenCommitToDatabaseThrowsAnException_ItShouldStillCommitToTaxamo_AndThrowWrappedException()
        {
            var tasks = new List<Func<Task>>();

            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task<InitializeCreditRequestResult>>>()))
                .Callback<Func<Task>>(tasks.Add)
                .ReturnsAsync(InitializeResult);

            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task<StripeTransactionResult>>>()))
                .Callback<Func<Task>>(tasks.Add)
                .ReturnsAsync(StripeTransactionResult);

            int callCount = 0;
            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task>>()))
                .Callback<Func<Task>>(tasks.Add)
                .Returns(() =>
                    {
                        if (callCount++ == 0)
                        {
                            var tcs = new TaskCompletionSource<int>();
                            tcs.SetException(new DivideByZeroException());
                            return tcs.Task;
                        }

                        return Task.FromResult(0);
                    });

            var exception = await ExpectedException.GetExceptionAsync<FailedToApplyCreditException>(
                () => this.target.ExecuteAsync(UserId, Amount, ExpectedTotalAmount));

            Assert.AreEqual(4, tasks.Count);

            Assert.IsNotNull(exception);
            Assert.IsTrue(
                exception.Message.Contains(StripeTransactionResult.StripeChargeId)
                && exception.Message.Contains(StripeTransactionResult.TransactionReference.ToString())
                && exception.Message.Contains(InitializeResult.TaxamoTransaction.Key)
                && exception.Message.Contains(UserId.Value.ToString()));
        }

        [TestMethod]
        public async Task WhenCommitToTaxamoThrowsAnException_ItShouldStillCommitToDatabase_AndThrowWrappedException()
        {
            var tasks = new List<Func<Task>>();

            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task<InitializeCreditRequestResult>>>()))
                .Callback<Func<Task>>(tasks.Add)
                .ReturnsAsync(InitializeResult);

            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task<StripeTransactionResult>>>()))
                .Callback<Func<Task>>(tasks.Add)
                .ReturnsAsync(StripeTransactionResult);

            int callCount = 0;
            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task>>()))
                .Callback<Func<Task>>(tasks.Add)
                .Returns(() =>
                    {
                        if (callCount++ == 1)
                        {
                            var tcs = new TaskCompletionSource<int>();
                            tcs.SetException(new DivideByZeroException());
                            return tcs.Task;
                        }

                        return Task.FromResult(0);
                    });

            var exception = await ExpectedException.GetExceptionAsync<FailedToApplyCreditException>(
                () => this.target.ExecuteAsync(UserId, Amount, ExpectedTotalAmount));

            Assert.AreEqual(4, tasks.Count);

            Assert.IsNotNull(exception);
            Assert.IsTrue(
                exception.Message.Contains(StripeTransactionResult.StripeChargeId)
                && exception.Message.Contains(StripeTransactionResult.TransactionReference.ToString())
                && exception.Message.Contains(InitializeResult.TaxamoTransaction.Key)
                && exception.Message.Contains(UserId.Value.ToString()));
        }

        private void SetupRetryOnTransientFailureTasks(List<Func<Task>> tasks)
        {
            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task<InitializeCreditRequestResult>>>()))
                .Callback<Func<Task>>(tasks.Add)
                .ReturnsAsync(InitializeResult);

            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task<StripeTransactionResult>>>()))
                .Callback<Func<Task>>(tasks.Add)
                .ReturnsAsync(StripeTransactionResult);

            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task>>()))
                .Callback<Func<Task>>(tasks.Add)
                .Returns(Task.FromResult(0));
        }
    }
}