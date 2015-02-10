namespace Fifthweek.Api.Availability.Tests.Queries
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.Availability.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetAvailabilityQueryHandlerTests
    {
        private readonly GetAvailabilityQuery query = new GetAvailabilityQuery();

        private Mock<ICountUsersDbStatement> countUsersDbStatement;
        private Mock<IExceptionHandler> exceptionHandler;
        private Mock<ITransientErrorDetectionStrategy> transientErrorDetectionStrategy;
        
        private GetAvailabilityQueryHandler target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.countUsersDbStatement = new Mock<ICountUsersDbStatement>();
            this.exceptionHandler = new Mock<IExceptionHandler>(MockBehavior.Strict);
            this.transientErrorDetectionStrategy = new Mock<ITransientErrorDetectionStrategy>(MockBehavior.Strict);

            this.target = new GetAvailabilityQueryHandler(
                this.exceptionHandler.Object,
                this.transientErrorDetectionStrategy.Object,
                this.countUsersDbStatement.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenQueryIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        public async Task WhenDatabaseIsAvailable_ItShouldReturnAHealthyStatus()
        {
            this.countUsersDbStatement.Setup(v => v.ExecuteAsync()).ReturnsAsync(2);

            var result = await this.target.HandleAsync(this.query);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Api);
            Assert.IsTrue(result.Database);
        }

        [TestMethod]
        public async Task WhenDatabaseIsNotAvailable_ItShouldReturnAnUnhealthyStatusAndReportTheError()
        {
            var exception = new Exception("Bad");
            this.countUsersDbStatement.Setup(v => v.ExecuteAsync()).Throws(exception);

            this.transientErrorDetectionStrategy.Setup(v => v.IsTransient(exception)).Returns(false);

            this.exceptionHandler.Setup(v => v.ReportExceptionAsync(exception)).Verifiable();

            var result = await this.target.HandleAsync(this.query);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Api);
            Assert.IsFalse(result.Database);

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

            var result = await this.target.HandleAsync(this.query);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Api);
            Assert.IsFalse(result.Database);

            Assert.IsNotNull(reportedException);
            Assert.IsInstanceOfType(reportedException, typeof(GetAvailabilityQueryHandler.TransientErrorException));
            Assert.AreSame(exception, reportedException.InnerException);
        }
    }
}
