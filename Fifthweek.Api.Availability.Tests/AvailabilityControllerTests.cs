namespace Fifthweek.Api.Availability.Tests
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class AvailabilityControllerTests
    {
        private Mock<IQueryHandler<GetAvailabilityQuery, AvailabilityResult>> getAvailability;

        private AvailabilityController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            this.getAvailability = new Mock<IQueryHandler<GetAvailabilityQuery, AvailabilityResult>>();

            this.controller = new AvailabilityController(this.getAvailability.Object);
            this.controller.Request = new HttpRequestMessage();
            this.controller.Configuration = new System.Web.Http.HttpConfiguration();

        }

        [TestMethod]
        public async Task WhenAvailibilityIsOkResponseShouldBeOk()
        {
            this.getAvailability.Setup(v => v.HandleAsync(It.IsAny<GetAvailabilityQuery>()))
                .ReturnsAsync(new AvailabilityResult(true, true));

            var result = await this.controller.Get();

            var response = await result.Content.ReadAsStringAsync();
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(response.Contains("\"Api\":true"));
            Assert.IsTrue(response.Contains("\"Database\":true"));
        }

        [TestMethod]
        public async Task WhenAvailibilityIsNotOkResponseShouldBeServiceUnavailable()
        {
            this.getAvailability.Setup(v => v.HandleAsync(It.IsAny<GetAvailabilityQuery>()))
                .ReturnsAsync(new AvailabilityResult(true, false));

            var result = await this.controller.Get();

            Assert.AreEqual(result.StatusCode, HttpStatusCode.ServiceUnavailable);

            var response = await result.Content.ReadAsStringAsync();
            Assert.IsTrue(response.Contains("\"Api\":true"));
            Assert.IsTrue(response.Contains("\"Database\":false"));
        }
    }
}