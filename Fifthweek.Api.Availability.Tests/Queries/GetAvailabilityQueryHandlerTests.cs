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

        private Mock<ITestSqlAzureAvailabilityStatement> testSqlAzureAvailability;
        private Mock<ITestPaymentsAvailabilityStatement> testPaymentsAvailability;
        
        private GetAvailabilityQueryHandler target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.testSqlAzureAvailability = new Mock<ITestSqlAzureAvailabilityStatement>(MockBehavior.Strict);
            this.testPaymentsAvailability = new Mock<ITestPaymentsAvailabilityStatement>(MockBehavior.Strict);

            this.target = new GetAvailabilityQueryHandler(
                this.testSqlAzureAvailability.Object,
                this.testPaymentsAvailability.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenQueryIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        public async Task WhenAllServicesAreAvailable_ItShouldReturnAHealthyStatus()
        {
            this.testSqlAzureAvailability.Setup(v => v.ExecuteAsync()).ReturnsAsync(true);
            this.testPaymentsAvailability.Setup(v => v.ExecuteAsync()).ReturnsAsync(true);

            var result = await this.target.HandleAsync(this.query);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Api);
            Assert.IsTrue(result.Database);
            Assert.IsTrue(result.Payments);

            Assert.IsTrue(result.IsOk());
        }

        [TestMethod]
        public async Task WhenSqlAzureUnavailable_ItShouldReturnUnhealthyStatus()
        {
            this.testSqlAzureAvailability.Setup(v => v.ExecuteAsync()).ReturnsAsync(false);
            this.testPaymentsAvailability.Setup(v => v.ExecuteAsync()).ReturnsAsync(true);

            var result = await this.target.HandleAsync(this.query);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Api);
            Assert.IsFalse(result.Database);
            Assert.IsTrue(result.Payments);

            Assert.IsFalse(result.IsOk());
        }

        [TestMethod]
        public async Task WhenPaymentsUnavailable_ItShouldReturnUnhealthyStatus()
        {
            this.testSqlAzureAvailability.Setup(v => v.ExecuteAsync()).ReturnsAsync(true);
            this.testPaymentsAvailability.Setup(v => v.ExecuteAsync()).ReturnsAsync(false);

            var result = await this.target.HandleAsync(this.query);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Api);
            Assert.IsTrue(result.Database);
            Assert.IsFalse(result.Payments);

            Assert.IsTrue(result.IsOk());
        }
    }
}
