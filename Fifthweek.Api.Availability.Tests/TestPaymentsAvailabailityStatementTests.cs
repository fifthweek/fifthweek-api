namespace Fifthweek.Api.Availability.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Availability.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Azure;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Constants = Fifthweek.Azure.Constants;

    [TestClass]
    public class TestPaymentsAvailabailityStatementTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;

        private Mock<IExceptionHandler> exceptionHandler;
        private Mock<ITransientErrorDetectionStrategy> transientErrorDetectionStrategy;
        private Mock<ICloudStorageAccount> cloudStorageAccount;
        private Mock<ITimestampCreator> timestampCreator;
        private Mock<IRequestProcessPaymentsService> requestProcessPayments;

        private TestPaymentsAvailabilityStatement target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.exceptionHandler = new Mock<IExceptionHandler>(MockBehavior.Strict);
            this.transientErrorDetectionStrategy = new Mock<ITransientErrorDetectionStrategy>(MockBehavior.Strict);
            this.cloudStorageAccount = new Mock<ICloudStorageAccount>(MockBehavior.Strict);
            this.timestampCreator = new Mock<ITimestampCreator>(MockBehavior.Strict);
            this.requestProcessPayments = new Mock<IRequestProcessPaymentsService>(MockBehavior.Strict);

            this.timestampCreator.Setup(v => v.Now()).Returns(Now);

            this.target = new TestPaymentsAvailabilityStatement(
                this.exceptionHandler.Object,
                this.transientErrorDetectionStrategy.Object,
                this.cloudStorageAccount.Object,
                this.timestampCreator.Object,
                this.requestProcessPayments.Object,
                new LastPaymentsRestartTimeContainer());
        }

        [TestMethod]
        public async Task WhenOccurredRecently_ItShouldReturnAHealthyStatus()
        {
            var blob = this.SetupBlob();

            SetupMetadata(Now.AddMinutes(-10), Now.AddMinutes(-5), 1000, blob);

            var result = await this.target.ExecuteAsync();

            Assert.IsTrue(result);

            blob.Verify();
        }

        [TestMethod]
        public async Task WhenNotAllMetadataIsAvailable_ItShouldAttemptPaymentProcessingRestart()
        {
            var blob = this.SetupBlob();
            blob.SetupGet(v => v.Metadata).Returns(new Dictionary<string, string>());

            this.SetupAttemptPaymentProcessingRestart();

            var result = await this.target.ExecuteAsync();

            Assert.IsFalse(result);

            blob.Verify();
            this.requestProcessPayments.Verify();
        }

        [TestMethod]
        public async Task WhenNotAllMetadataIsAvailable_AndRestartAttemptedRecently_ItShouldNotAttemptPaymentProcessingRestart()
        {
            var blob = this.SetupBlob();
            blob.SetupGet(v => v.Metadata).Returns(new Dictionary<string, string>());

            this.SetupAttemptPaymentProcessingRestart();

            await this.target.ExecuteAsync();
            this.timestampCreator.Setup(v => v.Now())
                .Returns(Now.Add(TestPaymentsAvailabilityStatement.RepeatRestartAttemptTimeSpan).AddTicks(-1));
            var result = await this.target.ExecuteAsync();

            Assert.IsFalse(result);

            blob.Verify();
            this.requestProcessPayments.Verify(v => v.ExecuteImmediatelyAsync(), Times.Once);
        }

        [TestMethod]
        public async Task WhenNotAllMetadataIsAvailable_AndRestartNotAttemptedRecently_ItShouldAttemptPaymentProcessingRestart()
        {
            var blob = this.SetupBlob();
            blob.SetupGet(v => v.Metadata).Returns(new Dictionary<string, string>());

            this.SetupAttemptPaymentProcessingRestart();

            await this.target.ExecuteAsync();
            this.timestampCreator.Setup(v => v.Now())
                .Returns(Now.Add(TestPaymentsAvailabilityStatement.RepeatRestartAttemptTimeSpan));
            var result = await this.target.ExecuteAsync();

            Assert.IsFalse(result);

            blob.Verify();
            this.requestProcessPayments.Verify(v => v.ExecuteImmediatelyAsync(), Times.Exactly(2));
        }

        [TestMethod]
        public async Task WhenNotOccurredRecently_ItShouldAttemptPaymentProcessingRestart()
        {
            var blob = this.SetupBlob();
            SetupMetadata(Now.Subtract(TestPaymentsAvailabilityStatement.PaymentsUnavailableTimeSpan), Now.AddMinutes(-5), 1000, blob);
            this.SetupAttemptPaymentProcessingRestart();

            var result = await this.target.ExecuteAsync();

            Assert.IsFalse(result);
            blob.Verify();
            this.requestProcessPayments.Verify();
            this.exceptionHandler.Verify();
        }

        [TestMethod]
        public async Task WhenNotOccurredRecently_AndRestartAttemptedRecently_ItShouldNotAttemptPaymentProcessingRestart()
        {
            var blob = this.SetupBlob();
            SetupMetadata(Now.Subtract(TestPaymentsAvailabilityStatement.PaymentsUnavailableTimeSpan), Now.AddMinutes(-5), 1000, blob);
            this.SetupAttemptPaymentProcessingRestart();

            await this.target.ExecuteAsync();
            this.timestampCreator.Setup(v => v.Now())
                .Returns(Now.Add(TestPaymentsAvailabilityStatement.RepeatRestartAttemptTimeSpan).AddTicks(-1));
            var result = await this.target.ExecuteAsync();

            Assert.IsFalse(result);
            blob.Verify();
            this.requestProcessPayments.Verify(v => v.ExecuteImmediatelyAsync(), Times.Once);
            this.exceptionHandler.Verify(v => v.ReportExceptionAsync(It.IsAny<WarningException>()), Times.Once);
        }

        [TestMethod]
        public async Task WhenNotOccurredRecently_AndRestartNotAttemptedRecently_ItShouldAttemptPaymentProcessingRestart()
        {
            var blob = this.SetupBlob();
            SetupMetadata(Now.Subtract(TestPaymentsAvailabilityStatement.PaymentsUnavailableTimeSpan), Now.AddMinutes(-5), 1000, blob);
            this.SetupAttemptPaymentProcessingRestart();

            await this.target.ExecuteAsync();
            this.timestampCreator.Setup(v => v.Now())
                .Returns(Now.Add(TestPaymentsAvailabilityStatement.RepeatRestartAttemptTimeSpan));
            var result = await this.target.ExecuteAsync();

            Assert.IsFalse(result);
            blob.Verify();
            this.requestProcessPayments.Verify(v => v.ExecuteImmediatelyAsync(), Times.Exactly(2));
            this.exceptionHandler.Verify(v => v.ReportExceptionAsync(It.IsAny<WarningException>()), Times.Exactly(2));
        }

        [TestMethod]
        public async Task WhenNonTransientErrorOccurs_ItShouldReturnAnUnhealthyStatusAndReportTheError()
        {
            var exception = new Exception("Bad");
            this.cloudStorageAccount.Setup(v => v.CreateCloudBlobClient()).Throws(exception);

            this.transientErrorDetectionStrategy.Setup(v => v.IsTransient(exception)).Returns(false);

            this.exceptionHandler.Setup(v => v.ReportExceptionAsync(exception)).Verifiable();

            var result = await this.target.ExecuteAsync();

            Assert.IsFalse(result);

            this.exceptionHandler.Verify();
        }

        [TestMethod]
        public async Task WhenTransientErrorOccurs_ItShouldReturnAnUnhealthyStatusAndReportTheTransientError()
        {
            var exception = new Exception("Bad");
            this.cloudStorageAccount.Setup(v => v.CreateCloudBlobClient()).Throws(exception);

            this.transientErrorDetectionStrategy.Setup(v => v.IsTransient(exception)).Returns(true);

            Exception reportedException = null;
            this.exceptionHandler
                .Setup(v => v.ReportExceptionAsync(It.IsAny<Exception>()))
                .Callback<Exception>(v => reportedException = v);

            var result = await this.target.ExecuteAsync();

            Assert.IsFalse(result);

            Assert.IsNotNull(reportedException);
            Assert.IsInstanceOfType(reportedException, typeof(TransientErrorException));
            Assert.AreSame(exception, reportedException.InnerException);
        }

        private void SetupAttemptPaymentProcessingRestart()
        {
            this.requestProcessPayments.Setup(v => v.ExecuteImmediatelyAsync()).Returns(Task.FromResult(0)).Verifiable();
            this.exceptionHandler.Setup(v => v.ReportExceptionAsync(It.IsAny<WarningException>())).Verifiable();
        }

        private Mock<ICloudBlockBlob> SetupBlob()
        {
            var blobClient = new Mock<ICloudBlobClient>();
            this.cloudStorageAccount.Setup(v => v.CreateCloudBlobClient()).Returns(blobClient.Object);

            var leaseContainer = new Mock<ICloudBlobContainer>();
            blobClient.Setup(v => v.GetContainerReference(Constants.AzureLeaseObjectsContainerName))
                .Returns(leaseContainer.Object);

            var blob = new Mock<ICloudBlockBlob>();
            leaseContainer.Setup(
                v => v.GetBlockBlobReference(Fifthweek.Payments.Shared.Constants.ProcessPaymentsLeaseObjectName))
                .Returns(blob.Object);

            blob.Setup(v => v.FetchAttributesAsync()).Returns(Task.FromResult(0)).Verifiable();
            return blob;
        }

        private static void SetupMetadata(DateTime startTime, DateTime endTime, int renewCount, Mock<ICloudBlockBlob> blob)
        {
            var metadata = new Dictionary<string, string>
            {
                {
                    Constants.LeaseStartTimestampMetadataKey,
                    startTime.ToIso8601String()
                },
                {
                    Constants.LeaseEndTimestampMetadataKey,
                    endTime.ToIso8601String()
                },
                {
                    Constants.LeaseRenewCountMetadataKey,
                    renewCount.ToString(System.Globalization.CultureInfo.InvariantCulture)
                },
            };

            blob.SetupGet(v => v.Metadata).Returns(metadata);
        }
    }
}