namespace Fifthweek.Api.Availability.Tests.Queries
{
    using System;
    using System.Data.Entity;
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

        private Mock<IFifthweekDbContext> fifthweekDbContext;

        private Mock<IExceptionHandler> exceptionHandler;

        private Mock<ITransientErrorDetectionStrategy> transientErrorDetectionStrategy;
        
        private GetAvailabilityQueryHandler target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.fifthweekDbContext = new Mock<IFifthweekDbContext>();
            this.exceptionHandler = new Mock<IExceptionHandler>(MockBehavior.Strict);
            this.transientErrorDetectionStrategy = new Mock<ITransientErrorDetectionStrategy>(MockBehavior.Strict);

            this.target = new GetAvailabilityQueryHandler(
                this.fifthweekDbContext.Object,
                this.exceptionHandler.Object,
                this.transientErrorDetectionStrategy.Object);
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
            var mockDbSet = new Mock<IDbSet<FifthweekUser>>();
            mockDbSet.Setup(v => v.CountAsync()).ReturnsAsync(0);
            this.fifthweekDbContext.Setup(v => v.Users).Returns(mockDbSet.Object);

            var result = await this.target.HandleAsync(this.query);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Api);
            Assert.IsTrue(result.Database);
        }

        [TestMethod]
        public async Task WhenDatabaseIsNotAvailable_ItShouldReturnAnUnhealthyStatusAndReportTheError()
        {
            var exception = new Exception("Bad");
            var mockDbSet = new Mock<IDbSet<FifthweekUser>>();
            mockDbSet.Setup(v => v.CountAsync()).Throws(exception);
            this.fifthweekDbContext.Setup(v => v.Users).Returns(mockDbSet.Object);

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
            var exception = new TimeoutException("Bad");

            var mockDbSet = new Mock<IDbSet<FifthweekUser>>();
            mockDbSet.Setup(v => v.CountAsync()).Throws(exception);
            this.fifthweekDbContext.Setup(v => v.Users).Returns(mockDbSet.Object);

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
