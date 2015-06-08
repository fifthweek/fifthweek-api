namespace Fifthweek.Payments.Tests.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Payments.Pipeline;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RollForwardSubscriptionsExecutorTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly DateTime EndTimeExclusive = Now.AddDays(7);
        private static readonly DateTime SubscriptionStartDate = Now.AddDays(-2);
        private static readonly DateTime SubscriptionFinalDate = SubscriptionStartDate.AddDays(7).AddTicks(-1);

        private static readonly Guid CreatorId1 = Guid.NewGuid();
        private static readonly Guid SubscriberId1 = Guid.NewGuid();

        private static readonly Guid ChannelId1 = Guid.NewGuid();
        private static readonly Guid ChannelId2 = Guid.NewGuid();

        protected RollForwardSubscriptionsExecutor target;

        public virtual void Initialize()
        {
            this.target = new RollForwardSubscriptionsExecutor();
        }

        [TestClass]
        public class Execute : RollForwardSubscriptionsExecutorTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            public void WhenNoSnapshots_ItShouldReturnAnEmptyList()
            {
                var result = this.target.Execute(EndTimeExclusive, new List<MergedSnapshot>());
                CollectionAssert.AreEqual(new List<MergedSnapshot>(), result.ToList());
            }

            [TestMethod]
            public void WhenSubscriptionEndsEarly_AndIsTheLastSnapshot_ItShouldBeExtended()
            {
                var input = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), true, false),
                };

                var expectedResult = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), true, true),
                    CreateSnapshot(SubscriptionFinalDate, true, false, Now.AddDays(3)),
                };

                var result = this.target.Execute(EndTimeExclusive, input);

                CollectionAssert.AreEqual(expectedResult, result.ToList());
            }

            [TestMethod]
            public void WhenSubscriptionEndsEarly_AndTheLastSnapshotIsAfterBillingEndDate_ItShouldBeExtended()
            {
                var input = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), true, false),
                    CreateSnapshot(Now.AddDays(4), true, false),
                    CreateSnapshot(Now.AddDays(6), true, false),
                };

                var expectedResult = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), true, true),
                    CreateSnapshot(Now.AddDays(4), true, true),
                    CreateSnapshot(SubscriptionFinalDate, true, false, Now.AddDays(4)),
                    CreateSnapshot(Now.AddDays(6), true, false),
                };

                var result = this.target.Execute(EndTimeExclusive, input);

                CollectionAssert.AreEqual(expectedResult, result.ToList());
            }

            [TestMethod]
            public void WhenSubscriptionEndsEarly_AndTheLastSnapshotIsOnBillingEndDate_ItShouldBeExtended()
            {
                var input = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), true, false),
                    CreateSnapshot(SubscriptionFinalDate, true, false),
                };

                var expectedResult = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), true, true),
                    CreateSnapshot(SubscriptionFinalDate, true, false),
                };

                var result = this.target.Execute(EndTimeExclusive, input);

                CollectionAssert.AreEqual(expectedResult, result.ToList());
            }

            [TestMethod]
            public void WhenSubscriptionEndsEarly_AndTheLastSnapshotIsBeforeBillingEndDate_ItShouldBeExtended()
            {
                var input = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), true, false),
                    CreateSnapshot(Now.AddDays(3.5), true, false),
                    CreateSnapshot(Now.AddDays(4), true, false),
                };

                var expectedResult = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), true, true),
                    CreateSnapshot(Now.AddDays(3.5), true, true),
                    CreateSnapshot(Now.AddDays(4), true, true),
                    CreateSnapshot(SubscriptionFinalDate, true, false, Now.AddDays(4)),
                };

                var result = this.target.Execute(EndTimeExclusive, input);

                CollectionAssert.AreEqual(expectedResult, result.ToList());
            }

            [TestMethod]
            public void WhenSubscriptionEndsOnTime_AndIsLastSnapshot_ItShouldNotBeExtended()
            {
                var input = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(SubscriptionFinalDate, true, false),
                };

                var expectedResult = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(SubscriptionFinalDate, true, false),
                };

                var result = this.target.Execute(EndTimeExclusive, input);

                CollectionAssert.AreEqual(expectedResult, result.ToList());
            }

            [TestMethod]
            public void WhenSubscriptionEndsOnTime_AndIsNotLastSnapshot_ItShouldNotBeExtended()
            {
                var input = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(SubscriptionFinalDate, true, false),
                    CreateSnapshot(Now.AddDays(6), true, false),
                };

                var expectedResult = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(SubscriptionFinalDate, true, false),
                    CreateSnapshot(Now.AddDays(6), true, false),
                };

                var result = this.target.Execute(EndTimeExclusive, input);

                CollectionAssert.AreEqual(expectedResult, result.ToList());
            }

            [TestMethod]
            public void WhenSubscriptionEndsEarly_AndIsTheLastSnapshot_AndChannelNoLongerExists_ItShouldNotBeExtended()
            {
                var input = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), false, false),
                };

                var expectedResult = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), false, false),
                };

                var result = this.target.Execute(EndTimeExclusive, input);

                CollectionAssert.AreEqual(expectedResult, result.ToList());
            }

            [TestMethod]
            public void WhenSubscriptionEndsEarly_AndTheLastSnapshotIsAfterBillingEndDate_AndChannelNoLongerExists_ItShouldNotBeExtended()
            {
                var input = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), false, false),
                    CreateSnapshot(Now.AddDays(4), false, false),
                    CreateSnapshot(Now.AddDays(6), false, false),
                };

                var expectedResult = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), false, false),
                    CreateSnapshot(Now.AddDays(4), false, false),
                    CreateSnapshot(Now.AddDays(6), false, false),
                };

                var result = this.target.Execute(EndTimeExclusive, input);

                CollectionAssert.AreEqual(expectedResult, result.ToList());
            }

            [TestMethod]
            public void WhenSubscriptionEndsEarly_AndTheLastSnapshotIsAfterBillingEndDate_AndChannelNoLongerExistsButIsRecreatedWithinBillingWeek_ItShouldNotBeExtended()
            {
                var input = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), false, false),
                    CreateSnapshot(Now.AddDays(4), true, false),
                    CreateSnapshot(Now.AddDays(6), true, false),
                };

                var expectedResult = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), false, false),
                    CreateSnapshot(Now.AddDays(4), true, false),
                    CreateSnapshot(Now.AddDays(6), true, false),
                };

                var result = this.target.Execute(EndTimeExclusive, input);

                CollectionAssert.AreEqual(expectedResult, result.ToList());
            }

            [TestMethod]
            public void WhenSubscriptionEndsEarly_AndTheLastSnapshotIsAfterBillingEndDate_AndChannelIsRemovedWithinBillingDate_ItShouldBeExtendedUntilChannelIsRemoved()
            {
                var input = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), true, false),
                    CreateSnapshot(Now.AddDays(4), false, false),
                    CreateSnapshot(Now.AddDays(6), false, false),
                };

                var expectedResult = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), true, true),
                    CreateSnapshot(Now.AddDays(4), false, false),
                    CreateSnapshot(Now.AddDays(6), false, false),
                };

                var result = this.target.Execute(EndTimeExclusive, input);

                CollectionAssert.AreEqual(expectedResult, result.ToList());
            }

            [TestMethod]
            public void WhenSubscriptionEndsEarly_AndTheLastSnapshotIsAfterBillingEndDate_AndChannelIsRemovedAfterBillingDate_ItShouldBeExtended()
            {
                var input = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), true, false),
                    CreateSnapshot(Now.AddDays(4), true, false),
                    CreateSnapshot(Now.AddDays(6), false, false),
                };

                var expectedResult = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), true, true),
                    CreateSnapshot(Now.AddDays(4), true, true),
                    CreateSnapshot(SubscriptionFinalDate, true, false, Now.AddDays(4)),
                    CreateSnapshot(Now.AddDays(6), false, false),
                };

                var result = this.target.Execute(EndTimeExclusive, input);

                CollectionAssert.AreEqual(expectedResult, result.ToList());
            }

            [TestMethod]
            public void WhenSubscriptionEndsEarly_AndTheUserBrieflyReSubscribes_ItShouldNotResetTheBillingEndDate()
            {
                var input = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), true, false),
                    CreateSnapshot(Now.AddDays(3.5), true, true),
                    CreateSnapshot(Now.AddDays(4), true, false),
                    CreateSnapshot(Now.AddDays(6), true, false),
                };

                var expectedResult = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), true, true),
                    CreateSnapshot(Now.AddDays(3.5), true, true),
                    CreateSnapshot(Now.AddDays(4), true, true),
                    CreateSnapshot(SubscriptionFinalDate, true, false, Now.AddDays(4)),
                    CreateSnapshot(Now.AddDays(6), true, false),
                };

                var result = this.target.Execute(EndTimeExclusive, input);

                CollectionAssert.AreEqual(expectedResult, result.ToList());
            }

            [TestMethod]
            public void WhenSubscriptionEndsEarly_AndTheUserBrieflyReSubscribesAfterBillingEndDate_ItShouldBillForAnotherWeek()
            {
                var input = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), true, false),
                    CreateSnapshot(Now.AddDays(4), true, false),
                    CreateSnapshot(Now.AddDays(6), true, true),
                    CreateSnapshot(Now.AddDays(7), true, false),
                };

                var expectedResult = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true),
                    CreateSnapshot(Now.AddDays(1), true, true),
                    CreateSnapshot(Now.AddDays(3), true, true),
                    CreateSnapshot(Now.AddDays(4), true, true),
                    CreateSnapshot(SubscriptionFinalDate, true, false, Now.AddDays(4)),
                    CreateSnapshot(Now.AddDays(6), true, true),
                    CreateSnapshot(Now.AddDays(7), true, true), // This takes us through to the end of the search period.
                };

                var result = this.target.Execute(EndTimeExclusive, input);

                CollectionAssert.AreEqual(expectedResult, result.ToList());
            }

            [TestMethod]
            public void WhenUnsubscribingFromMultipleChannels_AndTheBillingWeekEndsAtTheSameTime_ItShouldExtendToSamePeriod()
            {
                var input = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true, BillingWeekEndTimeType.Same),
                    CreateSnapshot(Now.AddDays(1), true, true, BillingWeekEndTimeType.Same),
                    CreateSnapshot(Now.AddDays(3), true, false, BillingWeekEndTimeType.Same),
                    CreateSnapshot(Now.AddDays(4), false, false, BillingWeekEndTimeType.Same),
                    CreateSnapshot(Now.AddDays(6), false, false, BillingWeekEndTimeType.Same),
                };

                var expectedResult = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true, BillingWeekEndTimeType.Same),
                    CreateSnapshot(Now.AddDays(1), true, true, BillingWeekEndTimeType.Same),
                    CreateSnapshot(Now.AddDays(3), true, true, BillingWeekEndTimeType.Same),
                    CreateSnapshot(Now.AddDays(4), true, true, BillingWeekEndTimeType.Same),
                    CreateSnapshot(SubscriptionFinalDate, false, false, BillingWeekEndTimeType.Same, Now.AddDays(4)),
                    CreateSnapshot(Now.AddDays(6), false, false, BillingWeekEndTimeType.Same),
                };

                var result = this.target.Execute(EndTimeExclusive, input);

                CollectionAssert.AreEqual(expectedResult, result.ToList());
            }

            [TestMethod]
            public void WhenUnsubscribingFromMultipleChannels_AndChannel1BillingWeekEndsFirst_ItShouldExtendToSamePeriod()
            {
                var input = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true, BillingWeekEndTimeType.Channel1First),
                    CreateSnapshot(Now.AddDays(1), true, true, BillingWeekEndTimeType.Channel1First),
                    CreateSnapshot(Now.AddDays(3), true, false, BillingWeekEndTimeType.Channel1First),
                    CreateSnapshot(Now.AddDays(4), false, false, BillingWeekEndTimeType.Channel1First),
                    CreateSnapshot(Now.AddDays(6), false, false, BillingWeekEndTimeType.Channel1First),
                };

                var expectedResult = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true, BillingWeekEndTimeType.Channel1First),
                    CreateSnapshot(Now.AddDays(1), true, true, BillingWeekEndTimeType.Channel1First),
                    CreateSnapshot(Now.AddDays(3), true, true, BillingWeekEndTimeType.Channel1First),
                    CreateSnapshot(Now.AddDays(4), true, true, BillingWeekEndTimeType.Channel1First),
                    CreateSnapshot(SubscriptionFinalDate, false, true, BillingWeekEndTimeType.Channel1First, Now.AddDays(4)),
                    CreateSnapshot(SubscriptionFinalDate.AddMinutes(10), false, false, BillingWeekEndTimeType.Channel1First, Now.AddDays(4)),
                    CreateSnapshot(Now.AddDays(6), false, false, BillingWeekEndTimeType.Channel1First),
                };

                var result = this.target.Execute(EndTimeExclusive, input);

                CollectionAssert.AreEqual(expectedResult, result.ToList());
            }

            [TestMethod]
            public void WhenUnsubscribingFromMultipleChannels_AndChannel2BillingWeekEndsFirst_ItShouldExtendToSamePeriod()
            {
                var input = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true, BillingWeekEndTimeType.Channel2First),
                    CreateSnapshot(Now.AddDays(1), true, true, BillingWeekEndTimeType.Channel2First),
                    CreateSnapshot(Now.AddDays(3), true, false, BillingWeekEndTimeType.Channel2First),
                    CreateSnapshot(Now.AddDays(4), false, false, BillingWeekEndTimeType.Channel2First),
                    CreateSnapshot(Now.AddDays(6), false, false, BillingWeekEndTimeType.Channel2First),
                };

                var expectedResult = new List<MergedSnapshot> 
                {
                    CreateSnapshot(Now.AddDays(0), true, true, BillingWeekEndTimeType.Channel2First),
                    CreateSnapshot(Now.AddDays(1), true, true, BillingWeekEndTimeType.Channel2First),
                    CreateSnapshot(Now.AddDays(3), true, true, BillingWeekEndTimeType.Channel2First),
                    CreateSnapshot(Now.AddDays(4), true, true, BillingWeekEndTimeType.Channel2First),
                    CreateSnapshot(SubscriptionFinalDate, true, false, BillingWeekEndTimeType.Channel2First, Now.AddDays(4)),
                    CreateSnapshot(SubscriptionFinalDate.AddMinutes(10), false, false, BillingWeekEndTimeType.Channel2First, Now.AddDays(4)),
                    CreateSnapshot(Now.AddDays(6), false, false, BillingWeekEndTimeType.Channel2First),
                };

                var result = this.target.Execute(EndTimeExclusive, input);

                CollectionAssert.AreEqual(expectedResult, result.ToList());
            }

            private static MergedSnapshot CreateSnapshot(DateTime timestamp, bool channelExists, bool isSubscribed, DateTime? innerTimestamp = null)
            {
                var creatorChannels = new List<CreatorChannelsSnapshotItem> { new CreatorChannelsSnapshotItem(ChannelId2, 100) };
                if (channelExists)
                {
                    creatorChannels.Add(new CreatorChannelsSnapshotItem(ChannelId1, 100));
                }

                var subscriberChannels = new List<SubscriberChannelsSnapshotItem> { new SubscriberChannelsSnapshotItem(ChannelId2, 100, SubscriptionStartDate) };
                if (isSubscribed)
                {
                    subscriberChannels.Add(new SubscriberChannelsSnapshotItem(ChannelId1, 100, SubscriptionStartDate));
                }

                return new MergedSnapshot(
                    timestamp,
                    new CreatorChannelsSnapshot(innerTimestamp ?? timestamp, CreatorId1, creatorChannels),
                    CreatorFreeAccessUsersSnapshot.Default(innerTimestamp ?? timestamp, CreatorId1),
                    new SubscriberChannelsSnapshot(innerTimestamp ?? timestamp, SubscriberId1, subscriberChannels),
                    SubscriberSnapshot.Default(innerTimestamp ?? timestamp, SubscriberId1));
            }

            private static MergedSnapshot CreateSnapshot(DateTime timestamp, bool channel1Subscribed, bool channel2Subscribed, BillingWeekEndTimeType endTimeType, DateTime? innerTimestamp = null)
            {
                var creatorChannels = new List<CreatorChannelsSnapshotItem> { new CreatorChannelsSnapshotItem(ChannelId1, 100), new CreatorChannelsSnapshotItem(ChannelId2, 100) };
                var subscriberChannels = new List<SubscriberChannelsSnapshotItem>();

                var channel1StartDate = SubscriptionStartDate;
                var channel2StartDate = SubscriptionStartDate;

                if (endTimeType == BillingWeekEndTimeType.Channel1First)
                {
                    channel2StartDate = channel2StartDate.AddMinutes(10);
                }
                else if (endTimeType == BillingWeekEndTimeType.Channel2First)
                {
                    channel1StartDate = channel2StartDate.AddMinutes(10);
                }

                if (channel1Subscribed)
                {
                    subscriberChannels.Add(new SubscriberChannelsSnapshotItem(ChannelId1, 100, channel1StartDate));
                }

                if (channel2Subscribed)
                {
                    subscriberChannels.Add(new SubscriberChannelsSnapshotItem(ChannelId2, 100, channel2StartDate));
                }

                return new MergedSnapshot(
                    timestamp,
                    new CreatorChannelsSnapshot(innerTimestamp ?? timestamp, CreatorId1, creatorChannels),
                    CreatorFreeAccessUsersSnapshot.Default(innerTimestamp ?? timestamp, CreatorId1),
                    new SubscriberChannelsSnapshot(innerTimestamp ?? timestamp, SubscriberId1, subscriberChannels),
                    SubscriberSnapshot.Default(innerTimestamp ?? timestamp, SubscriberId1));
            }

            private enum BillingWeekEndTimeType
            {
                Same,
                Channel1First,
                Channel2First,
            }
        }

        [TestClass]
        public class CalculateBillingWeekEndDateExclusive : RollForwardSubscriptionsExecutorTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            public void ItShouldCalculateBillingWeekEndDate1()
            {
                var result = this.target.CalculateBillingWeekEndDateExclusive(Now, Now.AddDays(3));
                Assert.AreEqual(Now.AddDays(7), result);
            }

            [TestMethod]
            public void ItShouldCalculateBillingWeekEndDate2()
            {
                var result = this.target.CalculateBillingWeekEndDateExclusive(Now, Now.AddDays(7).AddTicks(-1));
                Assert.AreEqual(Now.AddDays(7), result);
            }

            [TestMethod]
            public void ItShouldCalculateBillingWeekEndDate3()
            {
                var result = this.target.CalculateBillingWeekEndDateExclusive(Now, Now.AddDays(7));
                Assert.AreEqual(Now.AddDays(14), result);
            }

            [TestMethod]
            public void ItShouldCalculateBillingWeekEndDate4()
            {
                var result = this.target.CalculateBillingWeekEndDateExclusive(Now, Now.AddDays(7).AddTicks(1));
                Assert.AreEqual(Now.AddDays(14), result);
            }

            [TestMethod]
            public void ItShouldCalculateBillingWeekEndDate5()
            {
                var result = this.target.CalculateBillingWeekEndDateExclusive(Now, Now.AddDays(29).AddTicks(1));
                Assert.AreEqual(Now.AddDays(35), result);
            }
        }
    }
}