namespace Fifthweek.Payments.Tests.Services.Credit
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ApplyUserCreditTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly TransactionReference TransactionReference = TransactionReference.Random();
        private static readonly UserId UserId = UserId.Random();
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly PositiveInt Amount = PositiveInt.Parse(10);
        private static readonly PositiveInt ExpectedTotalAmount = PositiveInt.Parse(12);

        private static readonly InitializeCreditRequestResult InitializeResult = new InitializeCreditRequestResult(
            new TaxamoTransactionResult("key", new AmountInMinorDenomination(10), new AmountInMinorDenomination(20), new AmountInMinorDenomination(30), 0.2m, "VAT", "GB", "England"),
            new UserPaymentOriginResult("stripeCustomerId", PaymentOriginKeyType.Stripe, "GB", "12345", "1.1.1.1", "ttk", PaymentStatus.Retry1));

        private static readonly StripeTransactionResult StripeTransactionResult =
            new StripeTransactionResult(DateTime.UtcNow, TransactionReference.Random(), "stripeChargeId");

        private Mock<IInitializeCreditRequest> initializeCreditRequest;
        private Mock<IPerformCreditRequest> performCreditRequest;
        private Mock<ICommitCreditToDatabase> commitCreditToDatabase;
        private Mock<ICommitTestUserCreditToDatabase> commitTestUserCreditToDatabase;
        private Mock<IFifthweekRetryOnTransientErrorHandler> retryOnTransientFailure;
        private Mock<ICommitTaxamoTransaction> commitTaxamoTransaction;

        private ApplyUserCredit target;

        [TestInitialize]
        public void Intialize()
        {
            this.initializeCreditRequest = new Mock<IInitializeCreditRequest>(MockBehavior.Strict);
            this.performCreditRequest = new Mock<IPerformCreditRequest>(MockBehavior.Strict);
            this.commitCreditToDatabase = new Mock<ICommitCreditToDatabase>(MockBehavior.Strict);
            this.commitTestUserCreditToDatabase = new Mock<ICommitTestUserCreditToDatabase>(MockBehavior.Strict);
            this.retryOnTransientFailure = new Mock<IFifthweekRetryOnTransientErrorHandler>(MockBehavior.Strict);
            this.commitTaxamoTransaction = new Mock<ICommitTaxamoTransaction>(MockBehavior.Strict);

            this.target = new ApplyUserCredit(
                this.initializeCreditRequest.Object,
                this.performCreditRequest.Object,
                this.commitCreditToDatabase.Object,
                this.commitTestUserCreditToDatabase.Object,
                this.retryOnTransientFailure.Object,
                this.commitTaxamoTransaction.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, Now, TransactionReference, Amount, ExpectedTotalAmount, default(UserType));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenTransactionReferenceIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(UserId, Now, null, Amount, ExpectedTotalAmount, default(UserType));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAmountIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(UserId, Now, TransactionReference, null, ExpectedTotalAmount, default(UserType));
        }

        [TestMethod]
        public async Task ItShouldCallEachPhaseUsingTransientErrorHandler()
        {
            var tasks = new List<Func<Task>>();

            this.SetupRetryOnTransientFailureTasks(tasks);

            await this.target.ExecuteAsync(UserId, Now, TransactionReference, Amount, ExpectedTotalAmount, UserType.StandardUser);

            Assert.AreEqual(4, tasks.Count);

            this.initializeCreditRequest.Setup(v => v.HandleAsync(UserId, Amount, ExpectedTotalAmount, UserType.StandardUser))
                .ReturnsAsync(InitializeResult).Verifiable();
            await tasks[0]();
            this.initializeCreditRequest.Verify();

            this.performCreditRequest.Setup(v => v.HandleAsync(UserId, Now, TransactionReference, InitializeResult.TaxamoTransaction, InitializeResult.Origin, UserType.StandardUser))
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

            this.commitTaxamoTransaction.Setup(v => v.ExecuteAsync(InitializeResult.TaxamoTransaction, StripeTransactionResult, UserType.StandardUser))
                .Returns(Task.FromResult(0))
                .Verifiable();
            await tasks[3]();
            this.commitTaxamoTransaction.Verify();
        }

        [TestMethod]
        public async Task WhenUserIsTestUser_ItShouldCallEachPhaseUsingTransientErrorHandler()
        {
            var tasks = new List<Func<Task>>();

            this.SetupRetryOnTransientFailureTasks(tasks);

            await this.target.ExecuteAsync(UserId, Now, TransactionReference, Amount, ExpectedTotalAmount, UserType.TestUser);

            Assert.AreEqual(4, tasks.Count);

            this.initializeCreditRequest.Setup(v => v.HandleAsync(UserId, Amount, ExpectedTotalAmount, UserType.TestUser))
                .ReturnsAsync(InitializeResult).Verifiable();
            await tasks[0]();
            this.initializeCreditRequest.Verify();

            this.performCreditRequest.Setup(v => v.HandleAsync(UserId, Now, TransactionReference, InitializeResult.TaxamoTransaction, InitializeResult.Origin, UserType.TestUser))
                .ReturnsAsync(StripeTransactionResult).Verifiable();
            await tasks[1]();
            this.performCreditRequest.Verify();

            this.commitTestUserCreditToDatabase.Setup(v => v.HandleAsync(
                UserId,
                Amount))
                .Returns(Task.FromResult(0))
                .Verifiable();
            await tasks[2]();
            this.commitCreditToDatabase.Verify();

            this.commitTaxamoTransaction.Setup(v => v.ExecuteAsync(InitializeResult.TaxamoTransaction, StripeTransactionResult, UserType.TestUser))
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

            await this.target.ExecuteAsync(UserId, Now, TransactionReference, Amount, null, UserType.StandardUser);

            Assert.AreEqual(4, tasks.Count);

            this.initializeCreditRequest.Setup(v => v.HandleAsync(UserId, Amount, null, UserType.StandardUser))
                .ReturnsAsync(InitializeResult).Verifiable();
            await tasks[0]();
            this.initializeCreditRequest.Verify();

            this.performCreditRequest.Setup(v => v.HandleAsync(UserId, Now, TransactionReference, InitializeResult.TaxamoTransaction, InitializeResult.Origin, UserType.StandardUser))
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

            this.commitTaxamoTransaction.Setup(v => v.ExecuteAsync(InitializeResult.TaxamoTransaction, StripeTransactionResult, UserType.StandardUser))
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
                () => this.target.ExecuteAsync(UserId, Now, TransactionReference, Amount, ExpectedTotalAmount, UserType.StandardUser));

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
                () => this.target.ExecuteAsync(UserId, Now, TransactionReference, Amount, ExpectedTotalAmount, UserType.StandardUser));

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
                () => this.target.ExecuteAsync(UserId, Now, TransactionReference, Amount, ExpectedTotalAmount, UserType.StandardUser));

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
                () => this.target.ExecuteAsync(UserId, Now, TransactionReference, Amount, ExpectedTotalAmount, UserType.StandardUser));

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