namespace Fifthweek.WebJobs.Payments.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Tests.Shared;
    using Fifthweek.WebJobs.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PaymentProcessorTests
    {
        private static readonly CancellationToken CancellationToken = new CancellationTokenSource().Token;

        private Mock<IProcessAllPayments> processAllPayments;
        private Mock<IPaymentProcessingLeaseFactory> paymentProcessingLeaseFactory;
        private Mock<IRequestProcessPaymentsService> requestProcessPayments;

        private Mock<IPaymentProcessingLease> paymentProcessingLease;
        private Mock<ILogger> logger;

        private PaymentProcessor target;

        [TestInitialize]
        public void Initialize()
        {
            this.processAllPayments = new Mock<IProcessAllPayments>(MockBehavior.Strict);
            this.paymentProcessingLeaseFactory = new Mock<IPaymentProcessingLeaseFactory>(MockBehavior.Strict);
            this.requestProcessPayments = new Mock<IRequestProcessPaymentsService>(MockBehavior.Strict);

            this.paymentProcessingLease = new Mock<IPaymentProcessingLease>(MockBehavior.Strict);
            this.paymentProcessingLeaseFactory.Setup(v => v.Create(CancellationToken)).Returns(this.paymentProcessingLease.Object);

            this.logger = new Mock<ILogger>(MockBehavior.Strict);

            this.target = new PaymentProcessor(
                this.processAllPayments.Object,
                this.paymentProcessingLeaseFactory.Object,
                this.requestProcessPayments.Object);
        }

        [TestMethod]
        public async Task WhenSuccessfullyAcquiresLease_ItShouldProcessPayments()
        {
            this.paymentProcessingLease.Setup(v => v.TryAcquireLeaseAsync()).ReturnsAsync(true).Verifiable();
            this.paymentProcessingLease.Setup(v => v.GetIsAcquired()).Returns(true);

            this.processAllPayments.Setup(v => v.ExecuteAsync(this.paymentProcessingLease.Object, It.Is<List<PaymentProcessingException>>(l => l.Count == 0)))
                .Returns(Task.FromResult(0)).Verifiable();

            this.requestProcessPayments.Setup(v => v.ExecuteAsync()).Returns(Task.FromResult(0)).Verifiable();
            this.paymentProcessingLease.Setup(v => v.UpdateTimestampsAsync()).Returns(Task.FromResult(0)).Verifiable();
            this.paymentProcessingLease.Setup(v => v.ReleaseLeaseAsync()).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ProcessPaymentsAsync(new ProcessPaymentsMessage(), this.logger.Object, CancellationToken);

            this.paymentProcessingLease.Verify();
            this.requestProcessPayments.Verify();
            this.processAllPayments.Verify();
        }

        [TestMethod]
        public async Task WhenSuccessfullyAcquiresLease_AndProcessingCausesErrors_ItShouldLogErrors()
        {
            this.paymentProcessingLease.Setup(v => v.TryAcquireLeaseAsync()).ReturnsAsync(true).Verifiable();
            this.paymentProcessingLease.Setup(v => v.GetIsAcquired()).Returns(true);

            var error1 = new PaymentProcessingException(new DivideByZeroException(), UserId.Random(), UserId.Random());
            var error2 = new PaymentProcessingException(new DivideByZeroException(), UserId.Random(), UserId.Random());
            
            this.processAllPayments.Setup(v => v.ExecuteAsync(this.paymentProcessingLease.Object, It.Is<List<PaymentProcessingException>>(l => l.Count == 0)))
                .Callback<IKeepAliveHandler, List<PaymentProcessingException>>(
                    (lease, errors) => 
                    {
                        errors.Add(error1);
                        errors.Add(error2);
                    })
                    .Returns(Task.FromResult(0)).Verifiable();

            this.requestProcessPayments.Setup(v => v.ExecuteAsync()).Returns(Task.FromResult(0)).Verifiable();
            this.paymentProcessingLease.Setup(v => v.UpdateTimestampsAsync()).Returns(Task.FromResult(0)).Verifiable();
            this.paymentProcessingLease.Setup(v => v.ReleaseLeaseAsync()).Returns(Task.FromResult(0)).Verifiable();

            this.logger.Setup(v => v.Error(error1)).Verifiable();
            this.logger.Setup(v => v.Error(error2)).Verifiable();

            await this.target.ProcessPaymentsAsync(new ProcessPaymentsMessage(), this.logger.Object, CancellationToken);

            this.paymentProcessingLease.Verify();
            this.requestProcessPayments.Verify();
            this.processAllPayments.Verify();

            this.logger.Verify();
        }

        [TestMethod]
        public async Task WhenFailsToAcquireLease_ItShouldWarnAndExit()
        {
            this.paymentProcessingLease.Setup(v => v.TryAcquireLeaseAsync()).ReturnsAsync(false).Verifiable();
            this.paymentProcessingLease.Setup(v => v.GetIsAcquired()).Returns(false);

            this.logger.Setup(v => v.Warn("Failed to acquire lease to process payments due to conflict.")).Verifiable();

            await this.target.ProcessPaymentsAsync(new ProcessPaymentsMessage(), this.logger.Object, CancellationToken);

            this.paymentProcessingLease.Verify();
            this.logger.Verify();
        }

        [TestMethod]
        public async Task WhenErrorOccurs_ItShouldReleaseLeaseAndRethrow()
        {
            this.paymentProcessingLease.Setup(v => v.TryAcquireLeaseAsync()).ReturnsAsync(true).Verifiable();
            this.paymentProcessingLease.Setup(v => v.GetIsAcquired()).Returns(true);
            this.paymentProcessingLease.Setup(v => v.UpdateTimestampsAsync()).Returns(Task.FromResult(0)).Verifiable();
            this.paymentProcessingLease.Setup(v => v.ReleaseLeaseAsync()).Returns(Task.FromResult(0)).Verifiable();

            var exception = new DivideByZeroException();
            this.processAllPayments.Setup(v => v.ExecuteAsync(this.paymentProcessingLease.Object, It.Is<List<PaymentProcessingException>>(l => l.Count == 0)))
                .Throws(exception);

            this.logger.Setup(v => v.Error(exception)).Verifiable();

            await ExpectedException.AssertExceptionAsync<DivideByZeroException>(
                () => this.target.ProcessPaymentsAsync(new ProcessPaymentsMessage(), this.logger.Object, CancellationToken));


            this.paymentProcessingLease.Verify();
            this.processAllPayments.Verify();
            this.logger.Verify();
        }
    }
}