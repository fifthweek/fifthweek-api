namespace Fifthweek.Payments.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Snapshots;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ProcessPaymentProcessingDataTests
    {
        private static readonly UserId SubscriberId = UserId.Random();
        private static readonly UserId CreatorId = UserId.Random();

        private static readonly DateTime StartTimeInclusive = DateTime.UtcNow;

        private static readonly CommittedAccountBalance InitialCommittedAccountBalance =
            new CommittedAccountBalance(100m);

        private Mock<ISubscriberPaymentPipeline> subscriberPaymentPipeline;

        private ProcessPaymentProcessingData target;

        [TestInitialize]
        public void Initialize()
        {
            this.subscriberPaymentPipeline = new Mock<ISubscriberPaymentPipeline>(MockBehavior.Strict);

            this.target = new ProcessPaymentProcessingData(this.subscriberPaymentPipeline.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenDataIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task WhenDataRepresentsAPartialWeek_ItShouldReturnUncommittedResult()
        {
            var data = this.CreateData(StartTimeInclusive, StartTimeInclusive.AddDays(14).AddTicks(-1));

            var cost = new AggregateCostSummary(10);
            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                data.GetOrderedSnapshots(),
                null,
                SubscriberId,
                CreatorId,
                data.StartTimeInclusive,
                data.EndTimeExclusive)).Returns(cost);

            var result = await this.target.ExecuteAsync(data);

            Assert.AreEqual(
                new PaymentProcessingResults(
                    InitialCommittedAccountBalance,
                    new List<PaymentProcessingResult>
                    {
                        new PaymentProcessingResult(
                            data.StartTimeInclusive,
                            data.EndTimeExclusive,
                            new AggregateCostSummary(10),
                            null,
                            false),
                    }),
                    result);
        }

        [TestMethod]
        public async Task WhenCreatorPercentageOverrideSpecifiedAndNotExpired_ItShouldIncludeInResults()
        {
            var creatorPercentageOverride = new CreatorPercentageOverrideData(
                0.9m, 
                StartTimeInclusive.AddDays(14).AddTicks(-1));

            var data = this.CreateData(
                StartTimeInclusive,
                StartTimeInclusive.AddDays(14).AddTicks(-1),
                creatorPercentageOverride);

            var cost = new AggregateCostSummary(10);
            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                data.GetOrderedSnapshots(),
                null,
                SubscriberId,
                CreatorId,
                data.StartTimeInclusive,
                data.EndTimeExclusive)).Returns(cost);

            var result = await this.target.ExecuteAsync(data);

            Assert.AreEqual(
                new PaymentProcessingResults(
                    InitialCommittedAccountBalance,
                    new List<PaymentProcessingResult>
                    {
                        new PaymentProcessingResult(
                            data.StartTimeInclusive,
                            data.EndTimeExclusive,
                            new AggregateCostSummary(10),
                            creatorPercentageOverride,
                            false),
                    }),
                    result);
        }

        [TestMethod]
        public async Task WhenCreatorPercentageOverrideSpecifiedAndNoExpiry_ItShouldIncludeInResults()
        {
            var creatorPercentageOverride = new CreatorPercentageOverrideData(
                0.9m, 
                null);

            var data = this.CreateData(
                StartTimeInclusive,
                StartTimeInclusive.AddDays(14).AddTicks(-1),
                creatorPercentageOverride);

            var cost = new AggregateCostSummary(10);
            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                data.GetOrderedSnapshots(),
                null,
                SubscriberId,
                CreatorId,
                data.StartTimeInclusive,
                data.EndTimeExclusive)).Returns(cost);

            var result = await this.target.ExecuteAsync(data);

            Assert.AreEqual(
                new PaymentProcessingResults(
                    InitialCommittedAccountBalance,
                    new List<PaymentProcessingResult>
                    {
                        new PaymentProcessingResult(
                            data.StartTimeInclusive,
                            data.EndTimeExclusive,
                            new AggregateCostSummary(10),
                            creatorPercentageOverride,
                            false),
                    }),
                    result);
        }

        [TestMethod]
        public async Task WhenCreatorPercentageOverrideSpecifiedAndExpired_ItShouldNotIncludeInResults()
        {
            var creatorPercentageOverride = new CreatorPercentageOverrideData(
                0.9m,
                StartTimeInclusive.AddDays(14).AddTicks(-2));

            var data = this.CreateData(
                StartTimeInclusive,
                StartTimeInclusive.AddDays(14).AddTicks(-1),
                creatorPercentageOverride);

            var cost = new AggregateCostSummary(10);
            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                data.GetOrderedSnapshots(),
                null,
                SubscriberId,
                CreatorId,
                data.StartTimeInclusive,
                data.EndTimeExclusive)).Returns(cost);

            var result = await this.target.ExecuteAsync(data);

            Assert.AreEqual(
                new PaymentProcessingResults(
                    InitialCommittedAccountBalance,
                    new List<PaymentProcessingResult>
                    {
                        new PaymentProcessingResult(
                            data.StartTimeInclusive,
                            data.EndTimeExclusive,
                            new AggregateCostSummary(10),
                            null,
                            false),
                    }),
                    result);
        }

        [TestMethod]
        public async Task WhenDataRepresentsExactlyTwoWeeks_ItShouldReturnCommittedAndUncommittedResult()
        {
            var data = this.CreateData(StartTimeInclusive, StartTimeInclusive.AddDays(14));

            var cost1 = new AggregateCostSummary(10);
            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                data.GetOrderedSnapshots(),
                data.CreatorPosts,
                SubscriberId,
                CreatorId,
                data.StartTimeInclusive,
                data.StartTimeInclusive.AddDays(7))).Returns(cost1);

            var cost2 = new AggregateCostSummary(20);
            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                data.GetOrderedSnapshots(),
                null,
                SubscriberId,
                CreatorId,
                data.StartTimeInclusive.AddDays(7),
                data.EndTimeExclusive)).Returns(cost2);

            var result = await this.target.ExecuteAsync(data);

            Assert.AreEqual(
                new PaymentProcessingResults(
                    InitialCommittedAccountBalance.Subtract(cost1.Cost),
                    new List<PaymentProcessingResult>
                    {
                        new PaymentProcessingResult(
                            data.StartTimeInclusive,
                            data.StartTimeInclusive.AddDays(7),
                            cost1,
                            null,
                            true),
                        new PaymentProcessingResult(
                            data.StartTimeInclusive.AddDays(7),
                            data.EndTimeExclusive,
                            cost2,
                            null,
                            false),
                    }),
                    result);
        }

        [TestMethod]
        public async Task WhenExaclyTwoWeeksAndCreatorPercentageOverrideSpecifiedAndNotExpired_ItShouldIncludeInResults()
        {
            var creatorPercentageOverride = new CreatorPercentageOverrideData(
                0.9m,
                StartTimeInclusive.AddDays(14));

            var data = this.CreateData(
                StartTimeInclusive,
                StartTimeInclusive.AddDays(14),
                creatorPercentageOverride);

            var cost1 = new AggregateCostSummary(10);
            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                data.GetOrderedSnapshots(),
                data.CreatorPosts,
                SubscriberId,
                CreatorId,
                data.StartTimeInclusive,
                data.StartTimeInclusive.AddDays(7))).Returns(cost1);

            var cost2 = new AggregateCostSummary(20);
            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                data.GetOrderedSnapshots(),
                null,
                SubscriberId,
                CreatorId,
                data.StartTimeInclusive.AddDays(7),
                data.EndTimeExclusive)).Returns(cost2);

            var result = await this.target.ExecuteAsync(data);

            Assert.AreEqual(
                new PaymentProcessingResults(
                    InitialCommittedAccountBalance.Subtract(cost1.Cost),
                    new List<PaymentProcessingResult>
                    {
                        new PaymentProcessingResult(
                            data.StartTimeInclusive,
                            data.StartTimeInclusive.AddDays(7),
                            cost1,
                            creatorPercentageOverride,
                            true),
                        new PaymentProcessingResult(
                            data.StartTimeInclusive.AddDays(7),
                            data.EndTimeExclusive,
                            cost2,
                            creatorPercentageOverride,
                            false),
                    }),
                    result);
        }

        [TestMethod]
        public async Task WhenExaclyTwoWeeksAndCreatorPercentageOverrideSpecifiedAndNoExpiry_ItShouldIncludeInResults()
        {
            var creatorPercentageOverride = new CreatorPercentageOverrideData(
                0.9m,
                null);

            var data = this.CreateData(
                StartTimeInclusive,
                StartTimeInclusive.AddDays(14),
                creatorPercentageOverride);

            var cost1 = new AggregateCostSummary(10);
            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                data.GetOrderedSnapshots(),
                data.CreatorPosts,
                SubscriberId,
                CreatorId,
                data.StartTimeInclusive,
                data.StartTimeInclusive.AddDays(7))).Returns(cost1);

            var cost2 = new AggregateCostSummary(20);
            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                data.GetOrderedSnapshots(),
                null,
                SubscriberId,
                CreatorId,
                data.StartTimeInclusive.AddDays(7),
                data.EndTimeExclusive)).Returns(cost2);

            var result = await this.target.ExecuteAsync(data);

            Assert.AreEqual(
                new PaymentProcessingResults(
                    InitialCommittedAccountBalance.Subtract(cost1.Cost),
                    new List<PaymentProcessingResult>
                    {
                        new PaymentProcessingResult(
                            data.StartTimeInclusive,
                            data.StartTimeInclusive.AddDays(7),
                            cost1,
                            creatorPercentageOverride,
                            true),
                        new PaymentProcessingResult(
                            data.StartTimeInclusive.AddDays(7),
                            data.EndTimeExclusive,
                            cost2,
                            creatorPercentageOverride,
                            false),
                    }),
                    result);
        }

        [TestMethod]
        public async Task WhenExaclyTwoWeeksAndCreatorPercentageOverrideSpecifiedAndExpiredInUncommittedWeek_ItShouldNotIncludeInUncommittedResults()
        {
            var creatorPercentageOverride = new CreatorPercentageOverrideData(
                0.9m,
                StartTimeInclusive.AddDays(14).AddTicks(-1));

            var data = this.CreateData(
                StartTimeInclusive,
                StartTimeInclusive.AddDays(14),
                creatorPercentageOverride);

            var cost1 = new AggregateCostSummary(10);
            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                data.GetOrderedSnapshots(),
                data.CreatorPosts,
                SubscriberId,
                CreatorId,
                data.StartTimeInclusive,
                data.StartTimeInclusive.AddDays(7))).Returns(cost1);

            var cost2 = new AggregateCostSummary(20);
            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                data.GetOrderedSnapshots(),
                null,
                SubscriberId,
                CreatorId,
                data.StartTimeInclusive.AddDays(7),
                data.EndTimeExclusive)).Returns(cost2);

            var result = await this.target.ExecuteAsync(data);

            Assert.AreEqual(
                new PaymentProcessingResults(
                    InitialCommittedAccountBalance.Subtract(cost1.Cost),
                    new List<PaymentProcessingResult>
                    {
                        new PaymentProcessingResult(
                            data.StartTimeInclusive,
                            data.StartTimeInclusive.AddDays(7),
                            cost1,
                            creatorPercentageOverride,
                            true),
                        new PaymentProcessingResult(
                            data.StartTimeInclusive.AddDays(7),
                            data.EndTimeExclusive,
                            cost2,
                            null,
                            false),
                    }),
                    result);
        }

        [TestMethod]
        public async Task WhenExaclyTwoWeeksAndCreatorPercentageOverrideSpecifiedAndExpiredInCommittedWeek_ItShouldNotIncludeInResults()
        {
            var creatorPercentageOverride = new CreatorPercentageOverrideData(
                0.9m,
                StartTimeInclusive.AddDays(7).AddTicks(-1));

            var data = this.CreateData(
                StartTimeInclusive,
                StartTimeInclusive.AddDays(14),
                creatorPercentageOverride);

            var cost1 = new AggregateCostSummary(10);
            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                data.GetOrderedSnapshots(),
                data.CreatorPosts,
                SubscriberId,
                CreatorId,
                data.StartTimeInclusive,
                data.StartTimeInclusive.AddDays(7))).Returns(cost1);

            var cost2 = new AggregateCostSummary(20);
            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                data.GetOrderedSnapshots(),
                null,
                SubscriberId,
                CreatorId,
                data.StartTimeInclusive.AddDays(7),
                data.EndTimeExclusive)).Returns(cost2);

            var result = await this.target.ExecuteAsync(data);

            Assert.AreEqual(
                new PaymentProcessingResults(
                    InitialCommittedAccountBalance.Subtract(cost1.Cost),
                    new List<PaymentProcessingResult>
                    {
                        new PaymentProcessingResult(
                            data.StartTimeInclusive,
                            data.StartTimeInclusive.AddDays(7),
                            cost1,
                            null,
                            true),
                        new PaymentProcessingResult(
                            data.StartTimeInclusive.AddDays(7),
                            data.EndTimeExclusive,
                            cost2,
                            null,
                            false),
                    }),
                    result);
        }

        [TestMethod]
        public async Task WhenDataRepresentsMultipleWeeks_ItShouldReturnResults()
        {
            var creatorPercentageOverride = new CreatorPercentageOverrideData(
                0.9m,
                StartTimeInclusive.AddDays(9));

            var data = this.CreateData(
                StartTimeInclusive,
                StartTimeInclusive.AddDays(23),
                creatorPercentageOverride);

            var cost1 = new AggregateCostSummary(10);
            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                data.GetOrderedSnapshots(),
                data.CreatorPosts,
                SubscriberId,
                CreatorId,
                data.StartTimeInclusive,
                data.StartTimeInclusive.AddDays(7))).Returns(cost1);

            var cost2 = new AggregateCostSummary(20);
            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                data.GetOrderedSnapshots(),
                data.CreatorPosts,
                SubscriberId,
                CreatorId,
                data.StartTimeInclusive.AddDays(7),
                data.StartTimeInclusive.AddDays(14))).Returns(cost2);

            var cost3 = new AggregateCostSummary(30);
            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                data.GetOrderedSnapshots(),
                null,
                SubscriberId,
                CreatorId,
                data.StartTimeInclusive.AddDays(14),
                data.EndTimeExclusive)).Returns(cost3);

            var result = await this.target.ExecuteAsync(data);

            Assert.AreEqual(
                new PaymentProcessingResults(
                    InitialCommittedAccountBalance.Subtract(cost1.Cost + cost2.Cost),
                    new List<PaymentProcessingResult>
                    {
                        new PaymentProcessingResult(
                            data.StartTimeInclusive,
                            data.StartTimeInclusive.AddDays(7),
                            new AggregateCostSummary(10),
                            creatorPercentageOverride,
                            true),
                        new PaymentProcessingResult(
                            data.StartTimeInclusive.AddDays(7),
                            data.StartTimeInclusive.AddDays(14),
                            new AggregateCostSummary(20),
                            null,
                            true),
                        new PaymentProcessingResult(
                            data.StartTimeInclusive.AddDays(14),
                            data.EndTimeExclusive,
                            new AggregateCostSummary(30),
                            null,
                            false),
                    }),
                    result);
        }

        [TestMethod]
        public async Task WhenDataRepresentsMultipleWeeks_AndExceedsCommittedCredit_ItShouldReturnAdjustedResults()
        {
            var creatorPercentageOverride = new CreatorPercentageOverrideData(
                0.9m,
                StartTimeInclusive.AddDays(9));

            var data = this.CreateData(
                StartTimeInclusive,
                StartTimeInclusive.AddDays(23),
                creatorPercentageOverride);

            var cost1 = new AggregateCostSummary(60);
            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                data.GetOrderedSnapshots(),
                data.CreatorPosts,
                SubscriberId,
                CreatorId,
                data.StartTimeInclusive,
                data.StartTimeInclusive.AddDays(7))).Returns(cost1);

            var cost2 = new AggregateCostSummary(60);
            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                data.GetOrderedSnapshots(),
                data.CreatorPosts,
                SubscriberId,
                CreatorId,
                data.StartTimeInclusive.AddDays(7),
                data.StartTimeInclusive.AddDays(14))).Returns(cost2);

            var cost3 = new AggregateCostSummary(60);
            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                data.GetOrderedSnapshots(),
                null,
                SubscriberId,
                CreatorId,
                data.StartTimeInclusive.AddDays(14),
                data.EndTimeExclusive)).Returns(cost3);

            var result = await this.target.ExecuteAsync(data);

            Assert.AreEqual(
                new PaymentProcessingResults(
                    new CommittedAccountBalance(0),
                    new List<PaymentProcessingResult>
                    {
                        new PaymentProcessingResult(
                            data.StartTimeInclusive,
                            data.StartTimeInclusive.AddDays(7),
                            new AggregateCostSummary(60),
                            creatorPercentageOverride,
                            true),
                        new PaymentProcessingResult(
                            data.StartTimeInclusive.AddDays(7),
                            data.StartTimeInclusive.AddDays(14),
                            new AggregateCostSummary(40),
                            null,
                            true),
                        new PaymentProcessingResult(
                            data.StartTimeInclusive.AddDays(14),
                            data.EndTimeExclusive,
                            new AggregateCostSummary(60),
                            null,
                            false),
                    }),
                    result);
        }

        private PaymentProcessingData CreateData(DateTime startTimeInclusive, DateTime endTimeExclusive, CreatorPercentageOverrideData creatorPercentageOverride = null)
        {
            return new PaymentProcessingData(
                SubscriberId,
                CreatorId,
                startTimeInclusive,
                endTimeExclusive,
                InitialCommittedAccountBalance,
                new List<SubscriberChannelsSnapshot> 
                {
                    new SubscriberChannelsSnapshot(
                        DateTime.UtcNow, 
                        UserId.Random(),
                        new List<SubscriberChannelsSnapshotItem>
                        {
                            new SubscriberChannelsSnapshotItem(ChannelId.Random(), 100, DateTime.UtcNow),
                            new SubscriberChannelsSnapshotItem(ChannelId.Random(), 110, DateTime.UtcNow),
                        }),
                },
                new List<SubscriberSnapshot>
                {
                    new SubscriberSnapshot(DateTime.UtcNow, UserId.Random(), "a@b.com"),
                    new SubscriberSnapshot(DateTime.UtcNow, UserId.Random(), "x@y.com"),
                },
                new List<CalculatedAccountBalanceSnapshot>
                {
                    new CalculatedAccountBalanceSnapshot(DateTime.UtcNow, UserId.Random(), LedgerAccountType.FifthweekCredit, 10),
                },
                new List<CreatorChannelsSnapshot>
                {
                    new CreatorChannelsSnapshot(
                        DateTime.UtcNow, 
                        UserId.Random(),
                        new List<CreatorChannelsSnapshotItem>
                        {
                            new CreatorChannelsSnapshotItem(ChannelId.Random(), 200),
                            new CreatorChannelsSnapshotItem(ChannelId.Random(), 300),
                        }),
                },
                new List<CreatorFreeAccessUsersSnapshot>
                {
                    new CreatorFreeAccessUsersSnapshot(
                        DateTime.UtcNow, 
                        UserId.Random(), 
                        new List<string> { "b@c.com", "c@d.com" }),
                },
                new List<CreatorPost>
                {
                    new CreatorPost(ChannelId.Random(), DateTime.UtcNow),
                    new CreatorPost(ChannelId.Random(), DateTime.UtcNow),
                },
                creatorPercentageOverride);
        }
    }
}