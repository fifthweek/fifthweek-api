namespace Fifthweek.Api.Payments.Tests.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Payments.Taxamo;
    using Fifthweek.Api.Persistence.Identity;
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
            new UserPaymentOriginResult("stripeCustomerId", "GB", "12345", "1.1.1.1", "ttk"));

        private static readonly StripeTransactionResult StripeTransactionResult =
            new StripeTransactionResult(DateTime.UtcNow, Guid.NewGuid(), "stripeChargeId");

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IInitializeCreditRequest> initializeCreditRequest;
        private Mock<IPerformCreditRequest> performCreditRequest;
        private Mock<ICommitCreditToDatabase> commitCreditToDatabase;
        private Mock<IFifthweekRetryOnTransientErrorHandler> retryOnTransientFailure;
        private Mock<ICommitTaxamoTransaction> commitTaxamoTransaction;
        private Mock<ICommitTestUserCreditToDatabase> commitTestUserCreditToDatabase;

        private ApplyCreditRequestCommandHandler target;

        [TestInitialize]
        public void Intialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.initializeCreditRequest = new Mock<IInitializeCreditRequest>(MockBehavior.Strict);
            this.performCreditRequest = new Mock<IPerformCreditRequest>(MockBehavior.Strict);
            this.commitCreditToDatabase = new Mock<ICommitCreditToDatabase>(MockBehavior.Strict);
            this.retryOnTransientFailure = new Mock<IFifthweekRetryOnTransientErrorHandler>(MockBehavior.Strict);
            this.commitTaxamoTransaction = new Mock<ICommitTaxamoTransaction>(MockBehavior.Strict);
            this.commitTestUserCreditToDatabase = new Mock<ICommitTestUserCreditToDatabase>(MockBehavior.Strict);

            this.requesterSecurity.SetupFor(Requester);

            this.target = new ApplyCreditRequestCommandHandler(
                this.requesterSecurity.Object,
                this.initializeCreditRequest.Object,
                this.performCreditRequest.Object,
                this.commitCreditToDatabase.Object,
                this.retryOnTransientFailure.Object,
                this.commitTaxamoTransaction.Object,
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
        public async Task ItShouldCallEachPhaseUsingTransientErrorHandler()
        {
            var tasks = new List<Func<Task>>();

            this.SetupRetryOnTransientFailureTasks(tasks);

            await this.target.HandleAsync(Command);

            Assert.AreEqual(4, tasks.Count);

            this.initializeCreditRequest.Setup(v => v.HandleAsync(Command))
                .ReturnsAsync(InitializeResult).Verifiable();
            await tasks[0]();
            this.initializeCreditRequest.Verify();

            this.performCreditRequest.Setup(v => v.HandleAsync(Command, InitializeResult.TaxamoTransaction, InitializeResult.Origin))
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
        public async Task WhenUserIsTestUser_ItShouldCallCommitTestUserCreditToDatabaseWithTransientErrorHandler()
        {
            var tasks = new List<Func<Task>>();

            this.requesterSecurity.Setup(v => v.IsInRoleAsync(Requester, FifthweekRole.TestUser)).ReturnsAsync(true);

            this.SetupRetryOnTransientFailureTasks(tasks);

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

        [TestMethod]
        public async Task WhenInitiateThrowsAnException_ItShouldStopProcessingAndPropagate()
        {
            var tasks = new List<Func<Task>>();

            this.retryOnTransientFailure.Setup(v => v.HandleAsync(It.IsAny<Func<Task<InitializeCreditRequestResult>>>()))
                .Callback<Func<Task>>(tasks.Add)
                .ThrowsAsync(new DivideByZeroException());

            await ExpectedException.AssertExceptionAsync<DivideByZeroException>(
                () => this.target.HandleAsync(Command));

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
                () => this.target.HandleAsync(Command));

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
                () => this.target.HandleAsync(Command));

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
                () => this.target.HandleAsync(Command));

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