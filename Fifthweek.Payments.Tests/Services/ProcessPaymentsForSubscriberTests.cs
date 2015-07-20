namespace Fifthweek.Payments.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ProcessPaymentsForSubscriberTests
    {
        private static readonly UserId SubscriberId = UserId.Random();
        private static readonly UserId CreatorId1 = UserId.Random();
        private static readonly UserId CreatorId2 = UserId.Random();

        private static readonly DateTime Now = DateTime.UtcNow;

        private static readonly DateTime FirstSubscribedDate1 = "2015-05-02T18:24:18Z".FromIso8601String();
        private static readonly DateTime FirstSubscribedDate2 = "2015-04-27T18:24:18Z".FromIso8601String();
        private static readonly DateTime StartDateCalculatedFromFirstSubscribedDate1 = "2015-04-27T00:00:00Z".FromIso8601String();
        private static readonly DateTime StartDateCalculatedFromFirstSubscribedDate2 = "2015-04-27T00:00:00Z".FromIso8601String();
        private static readonly DateTime LastCommittedLedgerDate1 = Now.AddDays(-1);
        private static readonly DateTime LastCommittedLedgerDate2 = Now.AddDays(-2);

        private static readonly DateTime StartTimeInclusive = Now;
        private static readonly DateTime EndTimeExclusive = StartTimeInclusive.AddDays(7);

        private Mock<IGetCreatorsAndFirstSubscribedDatesDbStatement> getCreatorsAndFirstSubscribedDates;
        private Mock<IGetCommittedAccountBalanceDbStatement> getCommittedAccountBalanceDbStatement;
        private Mock<IProcessPaymentsBetweenSubscriberAndCreator> processPaymentsBetweenSubscriberAndCreator;
        private Mock<IGetLatestCommittedLedgerDateDbStatement> getLatestCommittedLedgerDate;
        private Mock<IKeepAliveHandler> keepAliveHandler;

        private ProcessPaymentsForSubscriber target;

        [TestInitialize]
        public void Initialize()
        {
            this.getCreatorsAndFirstSubscribedDates = new Mock<IGetCreatorsAndFirstSubscribedDatesDbStatement>(MockBehavior.Strict);
            this.getCommittedAccountBalanceDbStatement = new Mock<IGetCommittedAccountBalanceDbStatement>(MockBehavior.Strict);
            this.processPaymentsBetweenSubscriberAndCreator = new Mock<IProcessPaymentsBetweenSubscriberAndCreator>(MockBehavior.Strict);
            this.getLatestCommittedLedgerDate = new Mock<IGetLatestCommittedLedgerDateDbStatement>(MockBehavior.Strict);
            this.keepAliveHandler = new Mock<IKeepAliveHandler>();

            this.keepAliveHandler.Setup(v => v.KeepAliveAsync()).Returns(Task.FromResult(0)).Verifiable();

            this.target = new ProcessPaymentsForSubscriber(
                this.getCreatorsAndFirstSubscribedDates.Object,
                this.getCommittedAccountBalanceDbStatement.Object,
                this.processPaymentsBetweenSubscriberAndCreator.Object,
                this.getLatestCommittedLedgerDate.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenSubscriberIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, EndTimeExclusive, this.keepAliveHandler.Object, new List<PaymentProcessingException>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenKeepALiveHandlerIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(SubscriberId, EndTimeExclusive, null, new List<PaymentProcessingException>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenErrorsIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(SubscriberId, EndTimeExclusive, this.keepAliveHandler.Object, null);
        }

        [TestMethod]
        public async Task WhenLatestCommittedLedgerDateExists_ItShouldUseTheLatestCommittedLedgerDateAsStartDate()
        {
            var creatorsAndFirstSubscribedDates = new List<CreatorIdAndFirstSubscribedDate>
            {
                new CreatorIdAndFirstSubscribedDate(CreatorId1, FirstSubscribedDate1), 
            };

            this.getCreatorsAndFirstSubscribedDates.Setup(v => v.ExecuteAsync(SubscriberId))
                .ReturnsAsync(creatorsAndFirstSubscribedDates);

            var committedAccountBalance = new CommittedAccountBalance(10m);
            this.getCommittedAccountBalanceDbStatement.Setup(v => v.ExecuteAsync(SubscriberId)).ReturnsAsync(committedAccountBalance.Amount);

            this.getLatestCommittedLedgerDate.Setup(v => v.ExecuteAsync(SubscriberId, CreatorId1))
                .ReturnsAsync(LastCommittedLedgerDate1);

            var resultCommittedAccountBalance = new CommittedAccountBalance(5m);
            this.processPaymentsBetweenSubscriberAndCreator.Setup(
                v => v.ExecuteAsync(
                    SubscriberId,
                    CreatorId1,
                    LastCommittedLedgerDate1,
                    EndTimeExclusive,
                    committedAccountBalance))
                .ReturnsAsync(resultCommittedAccountBalance).Verifiable();

            var errors = new List<PaymentProcessingException>();
            await this.target.ExecuteAsync(SubscriberId, EndTimeExclusive, this.keepAliveHandler.Object, errors);

            Assert.AreEqual(0, errors.Count);

            this.processPaymentsBetweenSubscriberAndCreator.Verify();

            this.keepAliveHandler.Verify(v => v.KeepAliveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task WhenCommittedAccountBalanceIsNegative_ItShouldLogAnErrorAndContinue()
        {
            var creatorsAndFirstSubscribedDates = new List<CreatorIdAndFirstSubscribedDate>
            {
                new CreatorIdAndFirstSubscribedDate(CreatorId1, FirstSubscribedDate1), 
            };

            this.getCreatorsAndFirstSubscribedDates.Setup(v => v.ExecuteAsync(SubscriberId))
                .ReturnsAsync(creatorsAndFirstSubscribedDates);

            var committedAccountBalance = new CommittedAccountBalance(10m);
            this.getCommittedAccountBalanceDbStatement.Setup(v => v.ExecuteAsync(SubscriberId)).ReturnsAsync(committedAccountBalance.Amount);

            this.getLatestCommittedLedgerDate.Setup(v => v.ExecuteAsync(SubscriberId, CreatorId1))
                .ReturnsAsync(LastCommittedLedgerDate1);

            var resultCommittedAccountBalance = new CommittedAccountBalance(5m);
            this.processPaymentsBetweenSubscriberAndCreator.Setup(
                v => v.ExecuteAsync(
                    SubscriberId,
                    CreatorId1,
                    LastCommittedLedgerDate1,
                    EndTimeExclusive,
                    committedAccountBalance))
                .ReturnsAsync(resultCommittedAccountBalance).Verifiable();

            var errors = new List<PaymentProcessingException>();
            await this.target.ExecuteAsync(SubscriberId, EndTimeExclusive, this.keepAliveHandler.Object, errors);

            Assert.AreEqual(0, errors.Count);

            this.processPaymentsBetweenSubscriberAndCreator.Verify();

            this.keepAliveHandler.Verify(v => v.KeepAliveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task WhenMultipleCreatorSubscriptions_ItShouldPassTheOutputCommittedAccountBalanceDownTheChain()
        {
            var creatorsAndFirstSubscribedDates = new List<CreatorIdAndFirstSubscribedDate>
            {
                new CreatorIdAndFirstSubscribedDate(CreatorId1, FirstSubscribedDate1), 
                new CreatorIdAndFirstSubscribedDate(CreatorId2, FirstSubscribedDate2), 
            };

            this.getCreatorsAndFirstSubscribedDates.Setup(v => v.ExecuteAsync(SubscriberId))
                .ReturnsAsync(creatorsAndFirstSubscribedDates);

            var committedAccountBalance = new CommittedAccountBalance(10m);
            this.getCommittedAccountBalanceDbStatement.Setup(v => v.ExecuteAsync(SubscriberId)).ReturnsAsync(committedAccountBalance.Amount);

            this.getLatestCommittedLedgerDate.Setup(v => v.ExecuteAsync(SubscriberId, CreatorId1))
                .ReturnsAsync(LastCommittedLedgerDate1);

            this.getLatestCommittedLedgerDate.Setup(v => v.ExecuteAsync(SubscriberId, CreatorId2))
                .ReturnsAsync(LastCommittedLedgerDate2);

            var resultCommittedAccountBalance1 = new CommittedAccountBalance(5m);
            this.processPaymentsBetweenSubscriberAndCreator.Setup(
                v => v.ExecuteAsync(
                    SubscriberId,
                    CreatorId1,
                    LastCommittedLedgerDate1,
                    EndTimeExclusive,
                    committedAccountBalance))
                .ReturnsAsync(resultCommittedAccountBalance1).Verifiable();

            var resultCommittedAccountBalance2 = new CommittedAccountBalance(2m);
            this.processPaymentsBetweenSubscriberAndCreator.Setup(
                v => v.ExecuteAsync(
                    SubscriberId,
                    CreatorId2,
                    LastCommittedLedgerDate2,
                    EndTimeExclusive,
                    resultCommittedAccountBalance1))
                .ReturnsAsync(resultCommittedAccountBalance2).Verifiable();

            var errors = new List<PaymentProcessingException>();
            await this.target.ExecuteAsync(SubscriberId, EndTimeExclusive, this.keepAliveHandler.Object, errors);

            Assert.AreEqual(0, errors.Count);

            this.processPaymentsBetweenSubscriberAndCreator.Verify();

            this.keepAliveHandler.Verify(v => v.KeepAliveAsync(), Times.Exactly(2));
        }

        [TestMethod]
        public async Task WhenLatestCommittedLedgerDateDoesNotExist_ItShouldUseFirstSubscribedDateToCalculateStartDate()
        {
            var creatorsAndFirstSubscribedDates = new List<CreatorIdAndFirstSubscribedDate>
            {
                new CreatorIdAndFirstSubscribedDate(CreatorId1, FirstSubscribedDate1), 
            };

            this.getCreatorsAndFirstSubscribedDates.Setup(v => v.ExecuteAsync(SubscriberId))
                .ReturnsAsync(creatorsAndFirstSubscribedDates);

            var committedAccountBalance = new CommittedAccountBalance(10m);
            this.getCommittedAccountBalanceDbStatement.Setup(v => v.ExecuteAsync(SubscriberId)).ReturnsAsync(committedAccountBalance.Amount);

            this.getLatestCommittedLedgerDate.Setup(v => v.ExecuteAsync(SubscriberId, CreatorId1))
                .ReturnsAsync(null);

            this.processPaymentsBetweenSubscriberAndCreator.Setup(
                v => v.ExecuteAsync(
                    SubscriberId,
                    CreatorId1,
                    StartDateCalculatedFromFirstSubscribedDate1,
                    EndTimeExclusive,
                    committedAccountBalance))
                .ReturnsAsync(new CommittedAccountBalance(5m)).Verifiable();

            var errors = new List<PaymentProcessingException>();
            await this.target.ExecuteAsync(SubscriberId, EndTimeExclusive, this.keepAliveHandler.Object, errors);

            Assert.AreEqual(0, errors.Count);

            this.processPaymentsBetweenSubscriberAndCreator.Verify();

            this.keepAliveHandler.Verify(v => v.KeepAliveAsync(), Times.Once);
        }

        [TestMethod]
        public async Task WhenAnErrorOccurs_ItShouldLogTheErrorAndContinueProcessing()
        {
            var creatorsAndFirstSubscribedDates = new List<CreatorIdAndFirstSubscribedDate>
            {
                new CreatorIdAndFirstSubscribedDate(CreatorId1, FirstSubscribedDate1), 
                new CreatorIdAndFirstSubscribedDate(CreatorId2, FirstSubscribedDate2), 
            };

            this.getCreatorsAndFirstSubscribedDates.Setup(v => v.ExecuteAsync(SubscriberId))
                .ReturnsAsync(creatorsAndFirstSubscribedDates);

            var committedAccountBalance = new CommittedAccountBalance(10m);
            this.getCommittedAccountBalanceDbStatement.Setup(v => v.ExecuteAsync(SubscriberId)).ReturnsAsync(committedAccountBalance.Amount);

            var exception = new DivideByZeroException();
            this.getLatestCommittedLedgerDate.Setup(v => v.ExecuteAsync(SubscriberId, CreatorId1))
                .Throws(exception);
            this.getLatestCommittedLedgerDate.Setup(v => v.ExecuteAsync(SubscriberId, CreatorId2))
                .ReturnsAsync(null);

            this.processPaymentsBetweenSubscriberAndCreator.Setup(
                v => v.ExecuteAsync(
                    SubscriberId,
                    CreatorId2,
                    StartDateCalculatedFromFirstSubscribedDate2,
                    EndTimeExclusive,
                    committedAccountBalance))
                .ReturnsAsync(new CommittedAccountBalance(5m)).Verifiable();

            var errors = new List<PaymentProcessingException>();
            await this.target.ExecuteAsync(SubscriberId, EndTimeExclusive, this.keepAliveHandler.Object, errors);

            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual(CreatorId1, errors[0].CreatorId);
            Assert.AreEqual(SubscriberId, errors[0].SubscriberId);
            Assert.AreSame(exception, errors[0].InnerException);

            this.processPaymentsBetweenSubscriberAndCreator.Verify();

            this.keepAliveHandler.Verify(v => v.KeepAliveAsync(), Times.Exactly(2));
        }

        [TestMethod]
        public async Task WhenProcessingTimePeriodIsTooShort_ItShouldSkipCreatorAndContinueProcessing()
        {
            var creatorsAndFirstSubscribedDates = new List<CreatorIdAndFirstSubscribedDate>
            {
                new CreatorIdAndFirstSubscribedDate(CreatorId1, FirstSubscribedDate1), 
                new CreatorIdAndFirstSubscribedDate(CreatorId2, FirstSubscribedDate2), 
            };

            this.getCreatorsAndFirstSubscribedDates.Setup(v => v.ExecuteAsync(SubscriberId))
                .ReturnsAsync(creatorsAndFirstSubscribedDates);

            var committedAccountBalance = new CommittedAccountBalance(10m);
            this.getCommittedAccountBalanceDbStatement.Setup(v => v.ExecuteAsync(SubscriberId)).ReturnsAsync(committedAccountBalance.Amount);

            this.getLatestCommittedLedgerDate.Setup(v => v.ExecuteAsync(SubscriberId, CreatorId1))
                .ReturnsAsync(EndTimeExclusive.Subtract(ProcessPaymentsForSubscriber.MinimumProcessingPeriod));
            
            this.getLatestCommittedLedgerDate.Setup(v => v.ExecuteAsync(SubscriberId, CreatorId2))
                .ReturnsAsync(null);

            this.processPaymentsBetweenSubscriberAndCreator.Setup(
                v => v.ExecuteAsync(
                    SubscriberId,
                    CreatorId2,
                    StartDateCalculatedFromFirstSubscribedDate2,
                    EndTimeExclusive,
                    committedAccountBalance))
                .ReturnsAsync(new CommittedAccountBalance(5m)).Verifiable();

            var errors = new List<PaymentProcessingException>();
            await this.target.ExecuteAsync(SubscriberId, EndTimeExclusive, this.keepAliveHandler.Object, errors);

            Assert.AreEqual(0, errors.Count);

            this.processPaymentsBetweenSubscriberAndCreator.Verify();

            this.keepAliveHandler.Verify(v => v.KeepAliveAsync(), Times.Exactly(2));
        }
    }
}