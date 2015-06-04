namespace Fifthweek.Payments.Tests.Pipeline
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Payments.Pipeline;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class VerifySnapshotsExecutorTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly DateTime StartTimeInclusive = Now;
        private static readonly DateTime EndTimeExclusive = Now.AddDays(10);
        private static readonly Guid CreatorId1 = Guid.NewGuid();
        private static readonly Guid SubscriberId1 = Guid.NewGuid();

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
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CreatorGuestListSnapshot.Default(Now.AddSeconds(2), CreatorId1),
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
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    CreatorGuestListSnapshot.Default(Now.AddSeconds(2), CreatorId1),
                });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenCreatorSnapshotHasInvalidCreatorId_ItShouldThrowAnException()
        {
            this.target.Execute(
                StartTimeInclusive,
                EndTimeExclusive,
                SubscriberId1,
                CreatorId1,
                new List<ISnapshot> 
                {
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now, Guid.NewGuid()),
                    CreatorSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CreatorGuestListSnapshot.Default(Now.AddSeconds(2), CreatorId1),
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
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CreatorGuestListSnapshot.Default(Now.AddSeconds(2), Guid.NewGuid()),
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
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(1), Guid.NewGuid()),
                    SubscriberSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CreatorGuestListSnapshot.Default(Now.AddSeconds(2), CreatorId1),
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
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CreatorGuestListSnapshot.Default(EndTimeExclusive.AddTicks(-1), CreatorId1),
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
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CreatorGuestListSnapshot.Default(EndTimeExclusive, CreatorId1),
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
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CreatorGuestListSnapshot.Default(EndTimeExclusive.AddTicks(1), CreatorId1),
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
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(2), SubscriberId1),
                    CreatorGuestListSnapshot.Default(DateTime.Now.AddDays(2), CreatorId1),
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
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    new SubscriberSnapshot(Now.AddSeconds(2), SubscriberId1, "email", new List<SubscriberChannelSnapshot> { new SubscriberChannelSnapshot(Guid.NewGuid(), 100, Now) }),
                    CreatorGuestListSnapshot.Default(DateTime.Now.AddDays(2), CreatorId1),
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
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorSnapshot.Default(Now.AddSeconds(1), CreatorId1),
                    SubscriberSnapshot.Default(Now.AddSeconds(1), SubscriberId1),
                    new SubscriberSnapshot(Now.AddSeconds(2), SubscriberId1, "email", new List<SubscriberChannelSnapshot> { new SubscriberChannelSnapshot(Guid.NewGuid(), 100, DateTime.Now) }),
                    CreatorGuestListSnapshot.Default(DateTime.Now.AddDays(2), CreatorId1),
                });
        }
    }
}