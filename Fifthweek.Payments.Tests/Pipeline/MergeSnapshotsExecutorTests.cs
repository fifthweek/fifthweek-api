namespace Fifthweek.Payments.Tests.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Payments.Pipeline;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MergeSnapshotsExecutorTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;

        private static readonly Guid CreatorId1 = Guid.NewGuid();
        private static readonly Guid SubscriberId1 = Guid.NewGuid();

        private MergeSnapshotsExecutor target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new MergeSnapshotsExecutor();
        }

        [TestMethod]
        public void WhenNoSnapshots_ItShouldReturnEmptyList()
        {
            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, new List<ISnapshot>());
            CollectionAssert.AreEqual(new List<MergedSnapshot>(), result.ToList());
        }

        [TestMethod]
        public void WhenOnlyCreatorChannelSnapshot_ShouldReturnMergedSnapshot()
        {
            var creatorChannelSnapshot = new CreatorChannelSnapshot(Now, CreatorId1, new List<CreatorChannelSnapshotItem> { new CreatorChannelSnapshotItem(Guid.NewGuid(), 100) });
            var expectedResult = new MergedSnapshot(
                Now,
                creatorChannelSnapshot,
                CreatorGuestListSnapshot.Default(Now, CreatorId1),
                SubscriberChannelSnapshot.Default(Now, SubscriberId1),
                SubscriberSnapshot.Default(Now, SubscriberId1));

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, new List<ISnapshot> { creatorChannelSnapshot });

            CollectionAssert.AreEqual(new List<MergedSnapshot> { expectedResult }, result.ToList());
        }

        [TestMethod]
        public void WhenOnlyCreatorGuestListSnapshot_ShouldReturnMergedSnapshot()
        {
            var creatorGuestListSnapshot = new CreatorGuestListSnapshot(Now, CreatorId1, new List<string> { "a", "b" });
            var expectedResult = new MergedSnapshot(
                Now,
                CreatorChannelSnapshot.Default(Now, CreatorId1),
                creatorGuestListSnapshot,
                SubscriberChannelSnapshot.Default(Now, SubscriberId1),
                SubscriberSnapshot.Default(Now, SubscriberId1));

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, new List<ISnapshot> { creatorGuestListSnapshot });

            CollectionAssert.AreEqual(new List<MergedSnapshot> { expectedResult }, result.ToList());
        }

        [TestMethod]
        public void WhenOnlySubscriberChannelSnapshot_ShouldReturnMergedSnapshot()
        {
            var subscriberChannelSnapshot = new SubscriberChannelSnapshot(Now, SubscriberId1, new List<SubscriberChannelSnapshotItem> { new SubscriberChannelSnapshotItem(Guid.NewGuid(), 100, Now) });

            var expectedResult = new MergedSnapshot(
                Now,
                CreatorChannelSnapshot.Default(Now, CreatorId1),
                CreatorGuestListSnapshot.Default(Now, CreatorId1),
                subscriberChannelSnapshot,
                SubscriberSnapshot.Default(Now, SubscriberId1));

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, new List<ISnapshot> { subscriberChannelSnapshot });

            CollectionAssert.AreEqual(new List<MergedSnapshot> { expectedResult }, result.ToList());
        }

        [TestMethod]
        public void WhenOnlySubscriberSnapshot_ShouldReturnMergedSnapshot()
        {
            var subscriberSnapshot = new SubscriberSnapshot(Now, SubscriberId1, "email");

            var expectedResult = new MergedSnapshot(
                Now,
                CreatorChannelSnapshot.Default(Now, CreatorId1),
                CreatorGuestListSnapshot.Default(Now, CreatorId1),
                SubscriberChannelSnapshot.Default(Now, SubscriberId1),
                subscriberSnapshot);

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, new List<ISnapshot> { subscriberSnapshot });

            CollectionAssert.AreEqual(new List<MergedSnapshot> { expectedResult }, result.ToList());
        }

        [TestMethod]
        public void WhenFirstSnapshotAfterStartTime_ShouldReturnSnapshotAtStartTimeAndSnapshotAtSnapshotTime()
        {
            var creatorSnapshot = new CreatorChannelSnapshot(Now.AddDays(1), CreatorId1, new List<CreatorChannelSnapshotItem> { new CreatorChannelSnapshotItem(Guid.NewGuid(), 100) });
            var expectedResult = new List<MergedSnapshot> 
            {
                new MergedSnapshot(
                    Now,
                    CreatorChannelSnapshot.Default(Now, CreatorId1),
                    CreatorGuestListSnapshot.Default(Now, CreatorId1),
                    SubscriberChannelSnapshot.Default(Now, SubscriberId1),
                    SubscriberSnapshot.Default(Now, SubscriberId1)),
                new MergedSnapshot(
                    Now.AddDays(1),
                    creatorSnapshot,
                    CreatorGuestListSnapshot.Default(Now, CreatorId1),
                    SubscriberChannelSnapshot.Default(Now, SubscriberId1),
                    SubscriberSnapshot.Default(Now, SubscriberId1)),
            };

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, new List<ISnapshot> { creatorSnapshot });

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenFirstSnapshotBeforeStartTime_ShouldReturnSnapshotAtSnapshotTime()
        {
            var creatorSnapshot = new CreatorChannelSnapshot(Now.AddDays(-1), CreatorId1, new List<CreatorChannelSnapshotItem> { new CreatorChannelSnapshotItem(Guid.NewGuid(), 100) });
            var expectedResult = new List<MergedSnapshot> 
            {
                new MergedSnapshot(
                    Now.AddDays(-1),
                    creatorSnapshot,
                    CreatorGuestListSnapshot.Default(Now.AddDays(-1), CreatorId1),
                    SubscriberChannelSnapshot.Default(Now.AddDays(-1), SubscriberId1),
                    SubscriberSnapshot.Default(Now.AddDays(-1), SubscriberId1)),
            };

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, new List<ISnapshot> { creatorSnapshot });

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenSimultaniousSnapshots_ShouldMergeCorrectly()
        {
            var creatorChannelSnapshot = new CreatorChannelSnapshot(Now, CreatorId1, new List<CreatorChannelSnapshotItem> { new CreatorChannelSnapshotItem(Guid.NewGuid(), 100) });
            var creatorGuestListSnapshot = new CreatorGuestListSnapshot(Now, CreatorId1, new List<string> { "a", "b" });
            var subscriberChannelSnapshot = new SubscriberChannelSnapshot(Now, SubscriberId1, new List<SubscriberChannelSnapshotItem> { new SubscriberChannelSnapshotItem(Guid.NewGuid(), 100, Now) });
            var subscriberSnapshot = new SubscriberSnapshot(Now, SubscriberId1, "email");
            var expectedResult = new List<MergedSnapshot> 
            {
                new MergedSnapshot(
                    Now,
                    creatorChannelSnapshot,
                    creatorGuestListSnapshot,
                    subscriberChannelSnapshot,
                    subscriberSnapshot),
            };

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, new List<ISnapshot> { creatorChannelSnapshot, subscriberChannelSnapshot, creatorGuestListSnapshot, subscriberSnapshot });

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenSequenceOfSnapshots_ShouldReturnSnapshotAtSnapshotTime()
        {
            var input = new List<ISnapshot> 
            {
                new CreatorChannelSnapshot(Now.AddHours(1), CreatorId1, new List<CreatorChannelSnapshotItem> { new CreatorChannelSnapshotItem(Guid.NewGuid(), 100) }),
                new CreatorGuestListSnapshot(Now.AddHours(2), CreatorId1, new List<string> { "a", "b" }),
                new SubscriberChannelSnapshot(Now.AddHours(3), SubscriberId1, new List<SubscriberChannelSnapshotItem> { new SubscriberChannelSnapshotItem(Guid.NewGuid(), 100, Now) }),
                new CreatorGuestListSnapshot(Now.AddHours(4), CreatorId1, new List<string> { "a", "b" }),
                new CreatorChannelSnapshot(Now.AddHours(5), CreatorId1, new List<CreatorChannelSnapshotItem> { new CreatorChannelSnapshotItem(Guid.NewGuid(), 100) }),
                new SubscriberChannelSnapshot(Now.AddHours(5), SubscriberId1, new List<SubscriberChannelSnapshotItem> { new SubscriberChannelSnapshotItem(Guid.NewGuid(), 100, Now) }),
                new SubscriberChannelSnapshot(Now.AddHours(6), SubscriberId1, new List<SubscriberChannelSnapshotItem> { new SubscriberChannelSnapshotItem(Guid.NewGuid(), 100, Now) }),
                new SubscriberSnapshot(Now.AddHours(7), SubscriberId1, "email"),
            };

            var defaultCreatorChannelSnapshot = CreatorChannelSnapshot.Default(Now, CreatorId1);
            var defaultCreatorGuestListSnapshot = CreatorGuestListSnapshot.Default(Now, CreatorId1);
            var defaultSubscriberChannelSnapshot = SubscriberChannelSnapshot.Default(Now, SubscriberId1);
            var defaultSubscriberSnapshot = SubscriberSnapshot.Default(Now, SubscriberId1);

            var expectedResult = new List<MergedSnapshot> 
            {
                new MergedSnapshot(
                    Now,
                    defaultCreatorChannelSnapshot,
                    defaultCreatorGuestListSnapshot,
                    defaultSubscriberChannelSnapshot,
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(1),
                    (CreatorChannelSnapshot)input[0],
                    defaultCreatorGuestListSnapshot,
                    defaultSubscriberChannelSnapshot,
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(2),
                    (CreatorChannelSnapshot)input[0],
                    (CreatorGuestListSnapshot)input[1],
                    defaultSubscriberChannelSnapshot,
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(3),
                    (CreatorChannelSnapshot)input[0],
                    (CreatorGuestListSnapshot)input[1],
                    (SubscriberChannelSnapshot)input[2],
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(4),
                    (CreatorChannelSnapshot)input[0],
                    (CreatorGuestListSnapshot)input[3],
                    (SubscriberChannelSnapshot)input[2],
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(5),
                    (CreatorChannelSnapshot)input[4],
                    (CreatorGuestListSnapshot)input[3],
                    (SubscriberChannelSnapshot)input[5],
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(6),
                    (CreatorChannelSnapshot)input[4],
                    (CreatorGuestListSnapshot)input[3],
                    (SubscriberChannelSnapshot)input[6],
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(7),
                    (CreatorChannelSnapshot)input[4],
                    (CreatorGuestListSnapshot)input[3],
                    (SubscriberChannelSnapshot)input[6],
                    (SubscriberSnapshot)input[7]),
            };

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, input);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }
    }
}