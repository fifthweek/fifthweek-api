using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Payments.Tests.Services
{
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RequestProcessPaymentsServiceTests
    {
        private Mock<IQueueService> queueService;
        private RequestProcessPaymentsService target;

        [TestInitialize]
        public void Initialize()
        {
            this.queueService = new Mock<IQueueService>(MockBehavior.Strict);
            this.target = new RequestProcessPaymentsService(this.queueService.Object);
        }

        [TestMethod]
        public async Task WhenCalled_ItShouldCallQueueService()
        {
            this.queueService.Setup(
                v =>
                v.AddMessageToQueueAsync(
                    Shared.Constants.RequestProcessPaymentsQueueName,
                    ProcessPaymentsMessage.Default,
                    null,
                    Shared.Constants.PaymentProcessingDefaultMessageDelay)).Returns(Task.FromResult(0));

            await this.target.ExecuteAsync();
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public async Task WhenQueueServiceFails_ItShouldThrowAnException()
        {
            this.queueService.Setup(
                v =>
                v.AddMessageToQueueAsync(
                    Shared.Constants.RequestProcessPaymentsQueueName,
                    ProcessPaymentsMessage.Default,
                    null,
                    Shared.Constants.PaymentProcessingDefaultMessageDelay)).Throws(new DivideByZeroException());

            await this.target.ExecuteAsync();
        }

        [TestMethod]
        public async Task WhenCalledImmdediately_ItShouldCallQueueServiceWithNoDelay()
        {
            this.queueService.Setup(
                v =>
                v.AddMessageToQueueAsync(
                    Shared.Constants.RequestProcessPaymentsQueueName,
                    ProcessPaymentsMessage.Default,
                    null,
                    TimeSpan.Zero)).Returns(Task.FromResult(0));

            await this.target.ExecuteImmediatelyAsync();
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public async Task WhenQueueCalledImmediatelyAndServiceFails_ItShouldThrowAnException()
        {
            this.queueService.Setup(
                v =>
                v.AddMessageToQueueAsync(
                    Shared.Constants.RequestProcessPaymentsQueueName,
                    ProcessPaymentsMessage.Default,
                    null,
                    TimeSpan.Zero)).Throws(new DivideByZeroException());

            await this.target.ExecuteImmediatelyAsync();
        }
    }
}