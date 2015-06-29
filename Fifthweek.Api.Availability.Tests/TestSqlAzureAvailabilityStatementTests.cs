namespace Fifthweek.Api.Availability.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Availability.Queries;
    using Fifthweek.Api.Core;

    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class TestSqlAzureAvailabilityStatementTests
    {
        private Mock<ICountUsersDbStatement> countUsersDbStatement;
        private Mock<IExceptionHandler> exceptionHandler;
        private Mock<ITransientErrorDetectionStrategy> transientErrorDetectionStrategy;

        private TestSqlAzureAvailabilityStatement target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.countUsersDbStatement = new Mock<ICountUsersDbStatement>();
            this.exceptionHandler = new Mock<IExceptionHandler>(MockBehavior.Strict);
            this.transientErrorDetectionStrategy = new Mock<ITransientErrorDetectionStrategy>(MockBehavior.Strict);

            this.target = new TestSqlAzureAvailabilityStatement(
                this.exceptionHandler.Object,
                this.transientErrorDetectionStrategy.Object,
                this.countUsersDbStatement.Object);
        }

        [TestMethod]
        public async Task WhenDatabaseIsAvailable_ItShouldReturnAHealthyStatus()
        {
            this.countUsersDbStatement.Setup(v => v.ExecuteAsync()).ReturnsAsync(2);

            var result = await this.target.ExecuteAsync();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task WhenDatabaseIsNotAvailable_ItShouldReturnAnUnhealthyStatusAndReportTheError()
        {
            var exception = new Exception("Bad");
            this.countUsersDbStatement.Setup(v => v.ExecuteAsync()).Throws(exception);

            this.transientErrorDetectionStrategy.Setup(v => v.IsTransient(exception)).Returns(false);

            this.exceptionHandler.Setup(v => v.ReportExceptionAsync(exception)).Verifiable();

            var result = await this.target.ExecuteAsync();

            Assert.IsFalse(result);

            this.exceptionHandler.Verify();
        }

        [TestMethod]
        public async Task WhenDatabaseIsNotAvailableWithTransientError_ItShouldReturnAnUnhealthyStatusAndReportTheTransientError()
        {
            var exception = new Exception("Bad");
            this.countUsersDbStatement.Setup(v => v.ExecuteAsync()).Throws(exception);

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
    }
}