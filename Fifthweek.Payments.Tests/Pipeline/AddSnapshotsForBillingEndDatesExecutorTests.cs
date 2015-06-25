namespace Fifthweek.Payments.Tests.Pipeline
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
    public class AddSnapshotsForBillingEndDatesExecutorTests
    {
        private static readonly DateTime SubscriptionStartDate = DateTime.UtcNow.AddDays(-28);
        private static readonly DateTime Now = DateTime.UtcNow;

        private readonly SubscriberChannelsSnapshot subscriberChannels = new SubscriberChannelsSnapshot(
            Now,
            UserId.Random(),
            new List<SubscriberChannelsSnapshotItem> { new SubscriberChannelsSnapshotItem(ChannelId.Random(), 10, SubscriptionStartDate) });

        private readonly SubscriberChannelsSnapshot subscriberChannels2 = new SubscriberChannelsSnapshot(
            Now,
            UserId.Random(),
            new List<SubscriberChannelsSnapshotItem> { new SubscriberChannelsSnapshotItem(ChannelId.Random(), 10, SubscriptionStartDate.AddDays(2)) });

        private AddSnapshotsForBillingEndDatesExecutor target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new AddSnapshotsForBillingEndDatesExecutor();
        }

        [TestMethod]
        public void WhenNoSnapshotsItShouldReturnEmptyListOfSnapshots()
        {
            var input = new List<MergedSnapshot>();
            var output = this.target.Execute(input);
            CollectionAssert.AreEqual(input, output.ToList());
        }

        [TestMethod]
        public void WhenSingleSnapshot_WhenDateIsEndDate_ItShouldNotModifyCollection()
        {
            var input = new List<MergedSnapshot>
            {
                new MergedSnapshot(
                    Now,
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    this.subscriberChannels,
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random()))
            };

            var output = this.target.Execute(input);

            CollectionAssert.AreEqual(input, output.ToList());
        }

        [TestMethod]
        public void WhenSingleSnapshot_WhenDateIsNotEndDate_ItShouldNotModifyCollection()
        {
            var input = new List<MergedSnapshot>
            {
                new MergedSnapshot(
                    Now.AddDays(1),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    this.subscriberChannels,
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random()))
            };

            var output = this.target.Execute(input);

            CollectionAssert.AreEqual(input, output.ToList());
        }

        [TestMethod]
        public void WhenTwoSnapshots_WhenFirstDatesIsEndDate_ItShouldNotModifyCollection()
        {
            var input = new List<MergedSnapshot>
            {
                new MergedSnapshot(
                    Now,
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    this.subscriberChannels,
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random())),
                new MergedSnapshot(
                    Now.AddDays(1),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    SubscriberChannelsSnapshot.Default(Now, UserId.Random()),
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random()))
            };

            var output = this.target.Execute(input);

            CollectionAssert.AreEqual(input, output.ToList());
        }

        [TestMethod]
        public void WhenTwoSnapshots_WhenSecondDateIsEndDate_ItShouldNotModifyCollection()
        {
            var input = new List<MergedSnapshot>
            {
                new MergedSnapshot(
                    Now.AddDays(1),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    this.subscriberChannels,
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random())),
                new MergedSnapshot(
                    Now.AddDays(7),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    SubscriberChannelsSnapshot.Default(Now, UserId.Random()),
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random()))
            };

            var output = this.target.Execute(input);

            CollectionAssert.AreEqual(input, output.ToList());
        }

        [TestMethod]
        public void WhenTwoSnapshots_WhenBothDatesWithinBillingWeek_ItShouldNotModifyCollection()
        {
            var input = new List<MergedSnapshot>
            {
                new MergedSnapshot(
                    Now.AddDays(1),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    this.subscriberChannels,
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random())),
                new MergedSnapshot(
                    Now.AddDays(5),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    SubscriberChannelsSnapshot.Default(Now, UserId.Random()),
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random()))
            };

            var output = this.target.Execute(input);

            CollectionAssert.AreEqual(input, output.ToList());
        }

        [TestMethod]
        public void WhenTwoSnapshots_WhenDatesStraddleEndDate_ItShouldModifyCollection()
        {
            var input = new List<MergedSnapshot>
            {
                new MergedSnapshot(
                    Now.AddDays(6),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    this.subscriberChannels,
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random())),
                new MergedSnapshot(
                    Now.AddDays(8),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    SubscriberChannelsSnapshot.Default(Now, UserId.Random()),
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random()))
            };

            var expected = new List<MergedSnapshot>
            {
                input[0],
                new MergedSnapshot(
                    Now.AddDays(7),
                    input[0].CreatorChannels,
                    input[0].CreatorFreeAccessUsers,
                    input[0].SubscriberChannels,
                    input[0].Subscriber,
                    input[0].CalculatedAccountBalance),
                input[1],
            };

            var output = this.target.Execute(input);

            CollectionAssert.AreEqual(expected, output.ToList());
        }

        [TestMethod]
        public void WhenTwoSnapshotsAndTwoChannels_WhenDatesStraddleEndDate_ItShouldModifyCollection()
        {
            var input = new List<MergedSnapshot>
            {
                new MergedSnapshot(
                    Now.AddDays(6),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    this.subscriberChannels,
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random())),
                new MergedSnapshot(
                    Now.AddDays(10),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    this.subscriberChannels2,
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random()))
            };

            var expected = new List<MergedSnapshot>
            {
                input[0],
                new MergedSnapshot(
                    Now.AddDays(7),
                    input[0].CreatorChannels,
                    input[0].CreatorFreeAccessUsers,
                    input[0].SubscriberChannels,
                    input[0].Subscriber,
                    input[0].CalculatedAccountBalance),
                new MergedSnapshot(
                    Now.AddDays(7).AddDays(2),
                    input[0].CreatorChannels,
                    input[0].CreatorFreeAccessUsers,
                    input[0].SubscriberChannels,
                    input[0].Subscriber,
                    input[0].CalculatedAccountBalance),
                input[1],
            };

            var output = this.target.Execute(input);

            CollectionAssert.AreEqual(expected, output.ToList());
        }

        [TestMethod]
        public void WhenTwoSnapshots_WhenDatesOnEndDatesButStraddleEndDates_ItShouldModifyCollection()
        {
            var input = new List<MergedSnapshot>
            {
                new MergedSnapshot(
                    Now,
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    this.subscriberChannels,
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random())),
                new MergedSnapshot(
                    Now.AddDays(21),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    SubscriberChannelsSnapshot.Default(Now, UserId.Random()),
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random()))
            };

            var expected = new List<MergedSnapshot>
            {
                input[0],
                new MergedSnapshot(
                    Now.AddDays(7),
                    input[0].CreatorChannels,
                    input[0].CreatorFreeAccessUsers,
                    input[0].SubscriberChannels,
                    input[0].Subscriber,
                    input[0].CalculatedAccountBalance),
                new MergedSnapshot(
                    Now.AddDays(14),
                    input[0].CreatorChannels,
                    input[0].CreatorFreeAccessUsers,
                    input[0].SubscriberChannels,
                    input[0].Subscriber,
                    input[0].CalculatedAccountBalance),
                input[1],
            };

            var output = this.target.Execute(input);

            CollectionAssert.AreEqual(expected, output.ToList());
        }

        [TestMethod]
        public void WhenMultipleSnapshots_WhenDatesOnEndDatesButStraddleEndDates_ItShouldModifyCollection()
        {
            var input = new List<MergedSnapshot>
            {
                new MergedSnapshot(
                    Now,
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    this.subscriberChannels,
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random())),
                new MergedSnapshot(
                    Now.AddDays(4),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    SubscriberChannelsSnapshot.Default(Now, UserId.Random()),
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random())),
                new MergedSnapshot(
                    Now.AddDays(9),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    SubscriberChannelsSnapshot.Default(Now, UserId.Random()),
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random())),
                new MergedSnapshot(
                    Now.AddDays(14),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    SubscriberChannelsSnapshot.Default(Now, UserId.Random()),
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random())),
                new MergedSnapshot(
                    Now.AddDays(19),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    SubscriberChannelsSnapshot.Default(Now, UserId.Random()),
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random())),
                new MergedSnapshot(
                    Now.AddDays(25),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    SubscriberChannelsSnapshot.Default(Now, UserId.Random()),
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random()))
            };

            var expected = new List<MergedSnapshot>
            {
                input[0],
                input[1],
                new MergedSnapshot(
                    Now.AddDays(7),
                    input[1].CreatorChannels,
                    input[1].CreatorFreeAccessUsers,
                    input[1].SubscriberChannels,
                    input[1].Subscriber,
                    input[1].CalculatedAccountBalance),
                input[2],
                input[3],
                input[4],
                new MergedSnapshot(
                    Now.AddDays(21),
                    input[4].CreatorChannels,
                    input[4].CreatorFreeAccessUsers,
                    input[4].SubscriberChannels,
                    input[4].Subscriber,
                    input[4].CalculatedAccountBalance),
                input[5],
            };

            var output = this.target.Execute(input);

            CollectionAssert.AreEqual(expected, output.ToList());
        }

        [TestMethod]
        public void WhenMultipleSnapshotsAndMultipleChannels_WhenDatesOnEndDatesButStraddleEndDates_ItShouldModifyCollection()
        {
            var input = new List<MergedSnapshot>
            {
                new MergedSnapshot(
                    Now,
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    this.subscriberChannels,
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random())),
                new MergedSnapshot(
                    Now.AddDays(4),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    SubscriberChannelsSnapshot.Default(Now, UserId.Random()),
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random())),
                new MergedSnapshot(
                    Now.AddDays(9),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    this.subscriberChannels2,
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random())),
                new MergedSnapshot(
                    Now.AddDays(14),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    SubscriberChannelsSnapshot.Default(Now, UserId.Random()),
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random())),
                new MergedSnapshot(
                    Now.AddDays(19),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    SubscriberChannelsSnapshot.Default(Now, UserId.Random()),
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random())),
                new MergedSnapshot(
                    Now.AddDays(25),
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    SubscriberChannelsSnapshot.Default(Now, UserId.Random()),
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random()))
            };

            var expected = new List<MergedSnapshot>
            {
                input[0],
                new MergedSnapshot(
                    Now.AddDays(2),
                    input[0].CreatorChannels,
                    input[0].CreatorFreeAccessUsers,
                    input[0].SubscriberChannels,
                    input[0].Subscriber,
                    input[0].CalculatedAccountBalance),
                input[1],
                new MergedSnapshot(
                    Now.AddDays(7),
                    input[1].CreatorChannels,
                    input[1].CreatorFreeAccessUsers,
                    input[1].SubscriberChannels,
                    input[1].Subscriber,
                    input[1].CalculatedAccountBalance),
                input[2],
                input[3],
                new MergedSnapshot(
                    Now.AddDays(16),
                    input[3].CreatorChannels,
                    input[3].CreatorFreeAccessUsers,
                    input[3].SubscriberChannels,
                    input[3].Subscriber,
                    input[3].CalculatedAccountBalance),
                input[4],
                new MergedSnapshot(
                    Now.AddDays(21),
                    input[4].CreatorChannels,
                    input[4].CreatorFreeAccessUsers,
                    input[4].SubscriberChannels,
                    input[4].Subscriber,
                    input[4].CalculatedAccountBalance),
                new MergedSnapshot(
                    Now.AddDays(23),
                    input[4].CreatorChannels,
                    input[4].CreatorFreeAccessUsers,
                    input[4].SubscriberChannels,
                    input[4].Subscriber,
                    input[4].CalculatedAccountBalance),
                input[5],
            };

            var output = this.target.Execute(input);

            CollectionAssert.AreEqual(expected, output.ToList());
        }
    }
}