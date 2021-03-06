﻿namespace Fifthweek.Payments.Tests.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Pipeline;
    using Fifthweek.Payments.Snapshots;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RollForwardSubscriptionsExecutorTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly DateTime EndTimeExclusive = Now.AddDays(7);
        private static readonly DateTime SubscriptionStartDate = Now.AddDays(-2);
        private static readonly DateTime SubscriptionFinalDate = SubscriptionStartDate.AddDays(7).AddTicks(-1);

        private static readonly UserId CreatorId1 = new UserId(Guid.NewGuid());
        private static readonly UserId SubscriberId1 = new UserId(Guid.NewGuid());

        private static readonly ChannelId ChannelId1 = new ChannelId(Guid.NewGuid());
        private static readonly ChannelId ChannelId2 = new ChannelId(Guid.NewGuid());

        private RollForwardSubscriptionsExecutor target;

        [TestInitialize]
        public virtual void Initialize()
        {
            this.target = new RollForwardSubscriptionsExecutor();
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
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, false),
            };

            var expectedResult = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, true),
                CreateSnapshot(SubscriptionStartDate, SubscriptionFinalDate, true, false, Now.AddDays(3)),
            };

            var result = this.target.Execute(EndTimeExclusive, input);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenSubscriptionEndsEarly_AndTheLastSnapshotIsAfterBillingEndDate_ItShouldBeExtended()
        {
            var input = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, false),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), true, false),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(6), true, false),
            };

            var expectedResult = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), true, true),
                CreateSnapshot(SubscriptionStartDate, SubscriptionFinalDate, true, false, Now.AddDays(4)),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(6), true, false),
            };

            var result = this.target.Execute(EndTimeExclusive, input);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenSubscriptionEndsEarly_AndTheLastSnapshotIsOnBillingEndDate_ItShouldBeExtended()
        {
            var input = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, false),
                CreateSnapshot(SubscriptionStartDate, SubscriptionFinalDate, true, false),
            };

            var expectedResult = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, true),
                CreateSnapshot(SubscriptionStartDate, SubscriptionFinalDate, true, false),
            };

            var result = this.target.Execute(EndTimeExclusive, input);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenSubscriptionEndsEarly_AndTheLastSnapshotIsBeforeBillingEndDate_ItShouldBeExtended()
        {
            var input = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, false),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3.5), true, false),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), true, false),
            };

            var expectedResult = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3.5), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), true, true),
                CreateSnapshot(SubscriptionStartDate, SubscriptionFinalDate, true, false, Now.AddDays(4)),
            };

            var result = this.target.Execute(EndTimeExclusive, input);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenSubscriptionEndsOnTime_AndIsLastSnapshot_ItShouldNotBeExtended()
        {
            var input = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, SubscriptionFinalDate, true, false),
            };

            var expectedResult = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, SubscriptionFinalDate, true, false),
            };

            var result = this.target.Execute(EndTimeExclusive, input);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenSubscriptionEndsOnTime_AndIsNotLastSnapshot_ItShouldNotBeExtended()
        {
            var input = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, SubscriptionFinalDate, true, false),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(6), true, false),
            };

            var expectedResult = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, SubscriptionFinalDate, true, false),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(6), true, false),
            };

            var result = this.target.Execute(EndTimeExclusive, input);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenSubscriptionEndsEarly_AndIsTheLastSnapshot_AndChannelNoLongerExists_ItShouldNotBeExtended()
        {
            var input = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), false, false),
            };

            var expectedResult = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), false, false),
            };

            var result = this.target.Execute(EndTimeExclusive, input);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenSubscriptionEndsEarly_AndTheLastSnapshotIsAfterBillingEndDate_AndChannelNoLongerExists_ItShouldNotBeExtended()
        {
            var input = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), false, false),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), false, false),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(6), false, false),
            };

            var expectedResult = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), false, false),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), false, false),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(6), false, false),
            };

            var result = this.target.Execute(EndTimeExclusive, input);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenSubscriptionEndsEarly_AndTheLastSnapshotIsAfterBillingEndDate_AndChannelNoLongerExistsButIsRecreatedWithinBillingWeek_ItShouldNotBeExtended()
        {
            var input = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), false, false),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), true, false),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(6), true, false),
            };

            var expectedResult = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), false, false),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), true, false),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(6), true, false),
            };

            var result = this.target.Execute(EndTimeExclusive, input);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenSubscriptionEndsEarly_AndTheLastSnapshotIsAfterBillingEndDate_AndChannelIsRemovedWithinBillingDate_ItShouldBeExtendedUntilChannelIsRemoved()
        {
            var input = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, false),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), false, false),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(6), false, false),
            };

            var expectedResult = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), false, false),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(6), false, false),
            };

            var result = this.target.Execute(EndTimeExclusive, input);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenSubscriptionEndsEarly_AndTheLastSnapshotIsAfterBillingEndDate_AndChannelIsRemovedAfterBillingDate_ItShouldBeExtended()
        {
            var input = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, false),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), true, false),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(6), false, false),
            };

            var expectedResult = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), true, true),
                CreateSnapshot(SubscriptionStartDate, SubscriptionFinalDate, true, false, Now.AddDays(4)),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(6), false, false),
            };

            var result = this.target.Execute(EndTimeExclusive, input);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenSubscriptionEndsEarly_AndTheUserBrieflyReSubscribes_ItShouldNotResetTheBillingEndDate()
        {
            var input = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, false),
                CreateSnapshot(Now.AddDays(3.5), Now.AddDays(3.5), true, true),
                CreateSnapshot(Now.AddDays(3.5), Now.AddDays(4), true, false),
                CreateSnapshot(Now.AddDays(3.5), Now.AddDays(6), true, false),
            };

            var expectedResult = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, true),
                CreateSnapshot(Now.AddDays(3.5), Now.AddDays(3.5), true, true),
                CreateSnapshot(Now.AddDays(3.5), Now.AddDays(4), true, true),
                CreateSnapshot(Now.AddDays(3.5), SubscriptionFinalDate, true, false, Now.AddDays(4)),
                CreateSnapshot(Now.AddDays(3.5), Now.AddDays(6), true, false),
            };

            var result = this.target.Execute(EndTimeExclusive, input);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenSubscriptionEndsEarly_AndTheUserBrieflyReSubscribesAfterBillingEndDate_ItShouldBillUntilEndOfNewBillingWeek()
        {
            var input = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, false),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), true, false),
                CreateSnapshot(Now.AddDays(6), Now.AddDays(6), true, true),
                CreateSnapshot(Now.AddDays(6), Now.AddDays(7), true, false),
            };

            var expectedResult = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), true, true),
                CreateSnapshot(SubscriptionStartDate, SubscriptionFinalDate, true, false, Now.AddDays(4)),
                CreateSnapshot(Now.AddDays(6), Now.AddDays(6), true, true),
                CreateSnapshot(Now.AddDays(6), Now.AddDays(7), true, true), // This takes us through to the end of the search period.
            };

            var result = this.target.Execute(EndTimeExclusive, input);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenSubscriptionEndsInSecondWeek_ItShouldNotBillUntilEndOfBillingWeek()
        {
            var input = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(6), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(7), true, false),
            };

            var expectedResult = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(6), true, true),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(7), true, false),
            };

            var result = this.target.Execute(EndTimeExclusive, input);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenUnsubscribingFromMultipleChannels_AndTheBillingWeekEndsAtTheSameTime_ItShouldExtendToSamePeriod()
        {
            var input = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true, BillingWeekEndTimeType.Same),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true, BillingWeekEndTimeType.Same),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, false, BillingWeekEndTimeType.Same),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), false, false, BillingWeekEndTimeType.Same),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(6), false, false, BillingWeekEndTimeType.Same),
            };

            var expectedResult = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true, BillingWeekEndTimeType.Same),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true, BillingWeekEndTimeType.Same),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, true, BillingWeekEndTimeType.Same),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), true, true, BillingWeekEndTimeType.Same),
                CreateSnapshot(SubscriptionStartDate, SubscriptionFinalDate, false, false, BillingWeekEndTimeType.Same, Now.AddDays(4)),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(6), false, false, BillingWeekEndTimeType.Same),
            };

            var result = this.target.Execute(EndTimeExclusive, input);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenUnsubscribingFromMultipleChannels_AndChannel1BillingWeekEndsFirst_ItShouldExtendToSamePeriod()
        {
            var input = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true, BillingWeekEndTimeType.Channel1First),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true, BillingWeekEndTimeType.Channel1First),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, false, BillingWeekEndTimeType.Channel1First),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), false, false, BillingWeekEndTimeType.Channel1First),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(6), false, false, BillingWeekEndTimeType.Channel1First),
            };

            var expectedResult = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true, BillingWeekEndTimeType.Channel1First),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true, BillingWeekEndTimeType.Channel1First),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, true, BillingWeekEndTimeType.Channel1First),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), true, true, BillingWeekEndTimeType.Channel1First),
                CreateSnapshot(SubscriptionStartDate, SubscriptionFinalDate, false, true, BillingWeekEndTimeType.Channel1First, Now.AddDays(4)),
                CreateSnapshot(SubscriptionStartDate, SubscriptionFinalDate.AddMinutes(10), false, false, BillingWeekEndTimeType.Channel1First, Now.AddDays(4)),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(6), false, false, BillingWeekEndTimeType.Channel1First),
            };

            var result = this.target.Execute(EndTimeExclusive, input);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenUnsubscribingFromMultipleChannels_AndChannel2BillingWeekEndsFirst_ItShouldExtendToSamePeriod()
        {
            var input = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true, BillingWeekEndTimeType.Channel2First),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true, BillingWeekEndTimeType.Channel2First),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, false, BillingWeekEndTimeType.Channel2First),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), false, false, BillingWeekEndTimeType.Channel2First),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(6), false, false, BillingWeekEndTimeType.Channel2First),
            };

            var expectedResult = new List<MergedSnapshot> 
            {
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(0), true, true, BillingWeekEndTimeType.Channel2First),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(1), true, true, BillingWeekEndTimeType.Channel2First),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(3), true, true, BillingWeekEndTimeType.Channel2First),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(4), true, true, BillingWeekEndTimeType.Channel2First),
                CreateSnapshot(SubscriptionStartDate, SubscriptionFinalDate, true, false, BillingWeekEndTimeType.Channel2First, Now.AddDays(4)),
                CreateSnapshot(SubscriptionStartDate, SubscriptionFinalDate.AddMinutes(10), false, false, BillingWeekEndTimeType.Channel2First, Now.AddDays(4)),
                CreateSnapshot(SubscriptionStartDate, Now.AddDays(6), false, false, BillingWeekEndTimeType.Channel2First),
            };

            var result = this.target.Execute(EndTimeExclusive, input);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        private static MergedSnapshot CreateSnapshot(DateTime subscriptionStartDate, DateTime timestamp, bool channelExists, bool isSubscribed, DateTime? innerTimestamp = null)
        {
            var creatorChannels = new List<CreatorChannelsSnapshotItem> { new CreatorChannelsSnapshotItem(ChannelId2, 100) };
            if (channelExists)
            {
                creatorChannels.Add(new CreatorChannelsSnapshotItem(ChannelId1, 100));
            }

            var subscriberChannels = new List<SubscriberChannelsSnapshotItem> { new SubscriberChannelsSnapshotItem(ChannelId2, 100, subscriptionStartDate) };
            if (isSubscribed)
            {
                subscriberChannels.Add(new SubscriberChannelsSnapshotItem(ChannelId1, 100, subscriptionStartDate));
            }

            return new MergedSnapshot(
                timestamp,
                new CreatorChannelsSnapshot(innerTimestamp ?? timestamp, CreatorId1, creatorChannels),
                CreatorFreeAccessUsersSnapshot.Default(innerTimestamp ?? timestamp, CreatorId1),
                new SubscriberChannelsSnapshot(innerTimestamp ?? timestamp, SubscriberId1, subscriberChannels),
                SubscriberSnapshot.Default(innerTimestamp ?? timestamp, SubscriberId1),
                CalculatedAccountBalanceSnapshot.DefaultFifthweekCreditAccount(innerTimestamp ?? timestamp, SubscriberId1));
        }

        private static MergedSnapshot CreateSnapshot(DateTime subscriptionStartDate, DateTime timestamp, bool channel1Subscribed, bool channel2Subscribed, BillingWeekEndTimeType endTimeType, DateTime? innerTimestamp = null)
        {
            var creatorChannels = new List<CreatorChannelsSnapshotItem> { new CreatorChannelsSnapshotItem(ChannelId1, 100), new CreatorChannelsSnapshotItem(ChannelId2, 100) };
            var subscriberChannels = new List<SubscriberChannelsSnapshotItem>();

            var channel1StartDate = subscriptionStartDate;
            var channel2StartDate = subscriptionStartDate;

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
                SubscriberSnapshot.Default(innerTimestamp ?? timestamp, SubscriberId1),
                CalculatedAccountBalanceSnapshot.DefaultFifthweekCreditAccount(innerTimestamp ?? timestamp, SubscriberId1));
        }

        private enum BillingWeekEndTimeType
        {
            Same,
            Channel1First,
            Channel2First,
        }
    }
}