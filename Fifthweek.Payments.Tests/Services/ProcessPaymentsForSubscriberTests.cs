namespace Fifthweek.Payments.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ProcessPaymentsForSubscriberTests
    {
        private static readonly UserId SubscriberId = UserId.Random();
        private static readonly UserId CreatorId1 = UserId.Random();
        private static readonly UserId CreatorId2 = UserId.Random();

        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly DateTime FirstSubscribedDate1 = DateTime.Parse("2015-05-2T18:24:18Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
        private static readonly DateTime FirstSubscribedDate2 = DateTime.Parse("2015-04-27T18:24:18Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
        private static readonly DateTime StartDateCalculatedFromFirstSubscribedDate1 = DateTime.Parse("2015-04-27T00:00:00Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
        private static readonly DateTime StartDateCalculatedFromFirstSubscribedDate2 = DateTime.Parse("2015-04-27T00:00:00Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
        private static readonly DateTime LastCommittedLedgerDate1 = Now.AddDays(-1);

        private static readonly DateTime StartTimeInclusive = Now;
        private static readonly DateTime EndTimeExclusive = StartTimeInclusive.AddDays(7);

        private Mock<IGetCreatorsAndFirstSubscribedDatesDbStatement> getCreatorsAndFirstSubscribedDates;
        private Mock<IProcessPaymentsBetweenSubscriberAndCreator> processPaymentsBetweenSubscriberAndCreator;
        private Mock<IGetLatestCommittedLedgerDateDbStatement> getLatestCommittedLedgerDate;

        private ProcessPaymentsForSubscriber target;

        [TestInitialize]
        public void Initialize()
        {
            this.getCreatorsAndFirstSubscribedDates = new Mock<IGetCreatorsAndFirstSubscribedDatesDbStatement>(MockBehavior.Strict);
            this.processPaymentsBetweenSubscriberAndCreator = new Mock<IProcessPaymentsBetweenSubscriberAndCreator>(MockBehavior.Strict);
            this.getLatestCommittedLedgerDate = new Mock<IGetLatestCommittedLedgerDateDbStatement>(MockBehavior.Strict);

            this.target = new ProcessPaymentsForSubscriber(
                this.getCreatorsAndFirstSubscribedDates.Object,
                this.processPaymentsBetweenSubscriberAndCreator.Object,
                this.getLatestCommittedLedgerDate.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenSubscriberIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, EndTimeExclusive, new List<PaymentProcessingException>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenErrorsIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(SubscriberId, EndTimeExclusive, null);
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

            this.getLatestCommittedLedgerDate.Setup(v => v.ExecuteAsync(SubscriberId, CreatorId1))
                .ReturnsAsync(LastCommittedLedgerDate1);

            this.processPaymentsBetweenSubscriberAndCreator.Setup(
                v => v.ExecuteAsync(
                    SubscriberId,
                    CreatorId1,
                    LastCommittedLedgerDate1,
                    EndTimeExclusive))
                .Returns(Task.FromResult(0)).Verifiable();

            var errors = new List<PaymentProcessingException>();
            await this.target.ExecuteAsync(SubscriberId, EndTimeExclusive, errors);

            Assert.AreEqual(0, errors.Count);

            this.processPaymentsBetweenSubscriberAndCreator.Verify();
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

            this.getLatestCommittedLedgerDate.Setup(v => v.ExecuteAsync(SubscriberId, CreatorId1))
                .ReturnsAsync(null);

            this.processPaymentsBetweenSubscriberAndCreator.Setup(
                v => v.ExecuteAsync(
                    SubscriberId,
                    CreatorId1,
                    StartDateCalculatedFromFirstSubscribedDate1,
                    EndTimeExclusive))
                .Returns(Task.FromResult(0)).Verifiable();

            var errors = new List<PaymentProcessingException>();
            await this.target.ExecuteAsync(SubscriberId, EndTimeExclusive, errors);

            Assert.AreEqual(0, errors.Count);

            this.processPaymentsBetweenSubscriberAndCreator.Verify();
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
                    EndTimeExclusive))
                .Returns(Task.FromResult(0)).Verifiable();

            var errors = new List<PaymentProcessingException>();
            await this.target.ExecuteAsync(SubscriberId, EndTimeExclusive, errors);

            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual(CreatorId1, errors[0].CreatorId);
            Assert.AreEqual(SubscriberId, errors[0].SubscriberId);
            Assert.AreSame(exception, errors[0].InnerException);

            this.processPaymentsBetweenSubscriberAndCreator.Verify();
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

            var exception = new DivideByZeroException();
            this.getLatestCommittedLedgerDate.Setup(v => v.ExecuteAsync(SubscriberId, CreatorId1))
                .ReturnsAsync(EndTimeExclusive.Subtract(ProcessPaymentsForSubscriber.MinimumProcessingPeriod));
            
            this.getLatestCommittedLedgerDate.Setup(v => v.ExecuteAsync(SubscriberId, CreatorId2))
                .ReturnsAsync(null);

            this.processPaymentsBetweenSubscriberAndCreator.Setup(
                v => v.ExecuteAsync(
                    SubscriberId,
                    CreatorId2,
                    StartDateCalculatedFromFirstSubscribedDate2,
                    EndTimeExclusive))
                .Returns(Task.FromResult(0)).Verifiable();

            var errors = new List<PaymentProcessingException>();
            await this.target.ExecuteAsync(SubscriberId, EndTimeExclusive, errors);

            Assert.AreEqual(0, errors.Count);

            this.processPaymentsBetweenSubscriberAndCreator.Verify();
        }
    }
}