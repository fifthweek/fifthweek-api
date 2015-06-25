namespace Fifthweek.Payments.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ProcessAllPaymentsTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly List<UserId> SubscriberIds = new List<UserId> 
        {
            UserId.Random(),
            UserId.Random()
        };

        private Mock<ITimestampCreator> timestampCreator;
        private Mock<IGetAllSubscribersDbStatement> getAllSubscribers;
        private Mock<IProcessPaymentsForSubscriber> processPaymentsForSubscriber;
        private Mock<IUpdateAccountBalancesDbStatement> updateAccountBalances;
        private Mock<IKeepAliveHandler> keepAliveHandler;

        private ProcessAllPayments target;

        [TestInitialize]
        public void Initialize()
        {
            this.timestampCreator = new Mock<ITimestampCreator>();
            this.getAllSubscribers = new Mock<IGetAllSubscribersDbStatement>(MockBehavior.Strict);
            this.processPaymentsForSubscriber = new Mock<IProcessPaymentsForSubscriber>(MockBehavior.Strict);
            this.updateAccountBalances = new Mock<IUpdateAccountBalancesDbStatement>(MockBehavior.Strict);
            this.keepAliveHandler = new Mock<IKeepAliveHandler>(MockBehavior.Strict);

            this.timestampCreator.Setup(v => v.Now()).Returns(Now);

            this.target = new ProcessAllPayments(
                this.timestampCreator.Object,
                this.getAllSubscribers.Object,
                this.processPaymentsForSubscriber.Object,
                this.updateAccountBalances.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenErrorsIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(this.keepAliveHandler.Object, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenKeepALiveHandlerIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, new List<PaymentProcessingException>());
        }

        [TestMethod]
        public async Task ItShouldProcessAllPayments()
        {
            var errors = new List<PaymentProcessingException>();

            this.getAllSubscribers.Setup(v => v.ExecuteAsync()).ReturnsAsync(SubscriberIds).Verifiable();

            this.processPaymentsForSubscriber.Setup(v => v.ExecuteAsync(SubscriberIds[0], Now, this.keepAliveHandler.Object, errors))
                .Returns(Task.FromResult(0)).Verifiable();
            this.processPaymentsForSubscriber.Setup(v => v.ExecuteAsync(SubscriberIds[1], Now, this.keepAliveHandler.Object, errors))
                .Returns(Task.FromResult(0)).Verifiable();

            this.updateAccountBalances.Setup(v => v.ExecuteAsync(null, Now)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(this.keepAliveHandler.Object, errors);

            this.getAllSubscribers.Verify();
            this.processPaymentsForSubscriber.Verify();
            this.updateAccountBalances.Verify();
        }

        [TestMethod]
        public async Task WhenFailsToProcessSubscriber_ItShouldLogErrorAndContinue()
        {
            var errors = new List<PaymentProcessingException>();

            this.getAllSubscribers.Setup(v => v.ExecuteAsync()).ReturnsAsync(SubscriberIds).Verifiable();

            var exception = new DivideByZeroException();
            this.processPaymentsForSubscriber.Setup(v => v.ExecuteAsync(SubscriberIds[0], Now, this.keepAliveHandler.Object, errors))
                .Throws(exception).Verifiable();
            this.processPaymentsForSubscriber.Setup(v => v.ExecuteAsync(SubscriberIds[1], Now, this.keepAliveHandler.Object, errors))
                .Returns(Task.FromResult(0)).Verifiable();

            this.updateAccountBalances.Setup(v => v.ExecuteAsync(null, Now)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(this.keepAliveHandler.Object, errors);

            this.getAllSubscribers.Verify();
            this.processPaymentsForSubscriber.Verify();
            this.updateAccountBalances.Verify();

            Assert.AreEqual(1, errors.Count);
            Assert.IsNull(errors[0].CreatorId);
            Assert.AreEqual(SubscriberIds[0], errors[0].SubscriberId);
            Assert.AreSame(exception, errors[0].InnerException);
        }
    }
}