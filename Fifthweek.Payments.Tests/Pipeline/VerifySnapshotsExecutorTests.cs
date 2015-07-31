namespace Fifthweek.Payments.Tests.Pipeline
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Pipeline;
    using Fifthweek.Payments.Snapshots;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class VerifySnapshotsExecutorTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly DateTime StartTimeInclusive = Now;
        private static readonly DateTime EndTimeExclusive = Now.AddDays(10);
        private static readonly UserId CreatorId1 = new UserId(Guid.NewGuid());
        private static readonly UserId SubscriberId1 = new UserId(Guid.NewGuid());

        private VerifySnapshotsExecutor target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new VerifySnapshotsExecutor();
        }

        [TestMethod]
        public void WhenSnapshotsAreOrderedAndValid_ItShouldReturn()
        {
            this.target.Execute(
                StartTimeInclusive,
                EndTimeExclusive,
                SubscriberId1,
                CreatorId1,
                new List<ISnapshot> 
                {
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CreatorFreeAccessUsersSnapshot.Default(Now.AddSeconds(2), CreatorId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekCreditAccount(Now.AddSeconds(2), SubscriberId1),
                });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenSnapshotsAreUnorderedAndValid_ItShouldThrowAnException()
        {
            this.target.Execute(
                StartTimeInclusive,
                EndTimeExclusive,
                SubscriberId1,
                CreatorId1,
                new List<ISnapshot> 
                {
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    CreatorFreeAccessUsersSnapshot.Default(Now.AddSeconds(2), CreatorId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekCreditAccount(Now.AddSeconds(2), SubscriberId1),
                });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenCreatorChannelSnapshotHasInvalidCreatorId_ItShouldThrowAnException()
        {
            this.target.Execute(
                StartTimeInclusive,
                EndTimeExclusive,
                SubscriberId1,
                CreatorId1,
                new List<ISnapshot> 
                {
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now, new UserId(Guid.NewGuid())),
                    CreatorChannelsSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CreatorFreeAccessUsersSnapshot.Default(Now.AddSeconds(2), CreatorId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekCreditAccount(Now.AddSeconds(2), SubscriberId1),
                });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenCreatorGuestListSnapshotHasInvalidCreatorId_ItShouldThrowAnException()
        {
            this.target.Execute(
                StartTimeInclusive,
                EndTimeExclusive,
                SubscriberId1,
                CreatorId1,
                new List<ISnapshot> 
                {
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CreatorFreeAccessUsersSnapshot.Default(Now.AddSeconds(2), new UserId(Guid.NewGuid())),
                    SubscriberSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekCreditAccount(Now.AddSeconds(2), SubscriberId1),
                });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenSubscriberChannelSnapshotHasInvalidSubscriberId_ItShouldThrowAnException()
        {
            this.target.Execute(
                StartTimeInclusive,
                EndTimeExclusive,
                SubscriberId1,
                CreatorId1,
                new List<ISnapshot> 
                {
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(1), new UserId(Guid.NewGuid())),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CreatorFreeAccessUsersSnapshot.Default(Now.AddSeconds(2), CreatorId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekCreditAccount(Now.AddSeconds(2), SubscriberId1),
                });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenSubscriberSnapshotHasInvalidSubscriberId_ItShouldThrowAnException()
        {
            this.target.Execute(
                StartTimeInclusive,
                EndTimeExclusive,
                SubscriberId1,
                CreatorId1,
                new List<ISnapshot> 
                {
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CreatorFreeAccessUsersSnapshot.Default(Now.AddSeconds(2), CreatorId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(2), new UserId(Guid.NewGuid())),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekCreditAccount(Now.AddSeconds(2), SubscriberId1),
                });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenCalculatedAccountBalanceSnapshotHasInvalidSubscriberId_ItShouldThrowAnException()
        {
            this.target.Execute(
                StartTimeInclusive,
                EndTimeExclusive,
                SubscriberId1,
                CreatorId1,
                new List<ISnapshot> 
                {
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CreatorFreeAccessUsersSnapshot.Default(Now.AddSeconds(2), CreatorId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekCreditAccount(Now.AddSeconds(2), new UserId(Guid.NewGuid())),
                });
        }

        [TestMethod]
        public void WhenSnapshotsAreLessThanEndDate_ItShouldReturn()
        {
            this.target.Execute(
                StartTimeInclusive,
                EndTimeExclusive,
                SubscriberId1,
                CreatorId1,
                new List<ISnapshot> 
                {
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CreatorFreeAccessUsersSnapshot.Default(Now.AddSeconds(2), CreatorId1),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekCreditAccount(Now.AddSeconds(2), SubscriberId1),
                    SubscriberSnapshot.Default(EndTimeExclusive.AddTicks(-1), SubscriberId1),
                });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenSnapshotIsEqualToEndDate_ItShouldThrowAnException()
        {
            this.target.Execute(
                StartTimeInclusive,
                EndTimeExclusive,
                SubscriberId1,
                CreatorId1,
                new List<ISnapshot> 
                {
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CreatorFreeAccessUsersSnapshot.Default(Now.AddSeconds(2), CreatorId1),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekCreditAccount(Now.AddSeconds(2), SubscriberId1),
                    SubscriberSnapshot.Default(EndTimeExclusive, SubscriberId1),
                });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenSnapshotIsGreaterThanEndDate_ItShouldThrowAnException()
        {
            this.target.Execute(
                StartTimeInclusive,
                EndTimeExclusive,
                SubscriberId1,
                CreatorId1,
                new List<ISnapshot> 
                {
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CreatorFreeAccessUsersSnapshot.Default(Now.AddSeconds(2), CreatorId1),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekCreditAccount(Now.AddSeconds(2), SubscriberId1),
                    SubscriberSnapshot.Default(EndTimeExclusive.AddTicks(1), SubscriberId1),
                });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenTimestampIsNotUtc_ItShouldThrowAnException()
        {
            this.target.Execute(
                StartTimeInclusive,
                EndTimeExclusive,
                SubscriberId1,
                CreatorId1,
                new List<ISnapshot> 
                {
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CreatorFreeAccessUsersSnapshot.Default(Now.AddSeconds(2), CreatorId1),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekCreditAccount(Now.AddSeconds(2), SubscriberId1),
                    SubscriberSnapshot.Default(DateTime.Now.AddSeconds(2), SubscriberId1),
                });
        }

        [TestMethod]
        public void WhenSubscriptionTimeStampIsUtc_ItShouldReturn()
        {
            this.target.Execute(
                StartTimeInclusive,
                EndTimeExclusive,
                SubscriberId1,
                CreatorId1,
                new List<ISnapshot> 
                {
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    new SubscriberChannelsSnapshot(Now.AddSeconds(2), SubscriberId1, new List<SubscriberChannelsSnapshotItem> { new SubscriberChannelsSnapshotItem(new ChannelId(Guid.NewGuid()), 100, Now) }),
                    CreatorFreeAccessUsersSnapshot.Default(Now.AddSeconds(2), CreatorId1),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekCreditAccount(Now.AddSeconds(2), SubscriberId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenSubscriptionTimeStampIsNotUtc_ItShouldThrowAnException()
        {
            this.target.Execute(
                StartTimeInclusive,
                EndTimeExclusive,
                SubscriberId1,
                CreatorId1,
                new List<ISnapshot> 
                {
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorChannelsSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberChannelsSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    new SubscriberChannelsSnapshot(Now.AddSeconds(2), SubscriberId1, new List<SubscriberChannelsSnapshotItem> { new SubscriberChannelsSnapshotItem(new ChannelId(Guid.NewGuid()), 100, DateTime.Now) }),
                    CreatorFreeAccessUsersSnapshot.Default(Now.AddSeconds(2), CreatorId1),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekCreditAccount(Now.AddSeconds(2), SubscriberId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                });
        }
    }
}