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
        public void WhenOnlyCreatorSnapshot_ShouldReturnMergedSnapshot()
        {
            var creatorSnapshot = new CreatorSnapshot(Now, CreatorId1, new List<CreatorChannelSnapshot> { new CreatorChannelSnapshot(Guid.NewGuid(), 100) });
            var expectedResult = new MergedSnapshot(
                Now,
                creatorSnapshot,
                CreatorGuestListSnapshot.Default(Now, CreatorId1),
                SubscriberSnapshot.Default(Now, SubscriberId1));

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, new List<ISnapshot> { creatorSnapshot });

            CollectionAssert.AreEqual(new List<MergedSnapshot> { expectedResult }, result.ToList());
        }

        [TestMethod]
        public void WhenOnlyCreatorGuestListSnapshot_ShouldReturnMergedSnapshot()
        {
            var creatorGuestListSnapshot = new CreatorGuestListSnapshot(Now, CreatorId1, new List<string> { "a", "b" });
            var expectedResult = new MergedSnapshot(
                Now,
                CreatorSnapshot.Default(Now, CreatorId1),
                creatorGuestListSnapshot,
                SubscriberSnapshot.Default(Now, SubscriberId1));

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, new List<ISnapshot> { creatorGuestListSnapshot });

            CollectionAssert.AreEqual(new List<MergedSnapshot> { expectedResult }, result.ToList());
        }

        [TestMethod]
        public void WhenOnlySubscriberSnapshot_ShouldReturnMergedSnapshot()
        {
            var subscriberSnapshot = new SubscriberSnapshot(Now, SubscriberId1, "email", new List<SubscriberChannelSnapshot> { new SubscriberChannelSnapshot(Guid.NewGuid(), 100, Now) });
            
            var expectedResult = new MergedSnapshot(
                Now,
                CreatorSnapshot.Default(Now, CreatorId1),
                CreatorGuestListSnapshot.Default(Now, CreatorId1),
                subscriberSnapshot);

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, new List<ISnapshot> { subscriberSnapshot });

            CollectionAssert.AreEqual(new List<MergedSnapshot> { expectedResult }, result.ToList());
        }

        [TestMethod]
        public void WhenFirstSnapshotAfterStartTime_ShouldReturnSnapshotAtStartTimeAndSnapshotAtSnapshotTime()
        {
            var creatorSnapshot = new CreatorSnapshot(Now.AddDays(1), CreatorId1, new List<CreatorChannelSnapshot> { new CreatorChannelSnapshot(Guid.NewGuid(), 100) });
            var expectedResult = new List<MergedSnapshot> 
            {
                new MergedSnapshot(
                    Now,
                    CreatorSnapshot.Default(Now, CreatorId1),
                    CreatorGuestListSnapshot.Default(Now, CreatorId1),
                    SubscriberSnapshot.Default(Now, SubscriberId1)),
                new MergedSnapshot(
                    Now.AddDays(1),
                    creatorSnapshot,
                    CreatorGuestListSnapshot.Default(Now, CreatorId1),
                    SubscriberSnapshot.Default(Now, SubscriberId1)),
            };

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, new List<ISnapshot> { creatorSnapshot });

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenFirstSnapshotBeforeStartTime_ShouldReturnSnapshotAtSnapshotTime()
        {
            var creatorSnapshot = new CreatorSnapshot(Now.AddDays(-1), CreatorId1, new List<CreatorChannelSnapshot> { new CreatorChannelSnapshot(Guid.NewGuid(), 100) });
            var expectedResult = new List<MergedSnapshot> 
            {
                new MergedSnapshot(
                    Now.AddDays(-1),
                    creatorSnapshot,
                    CreatorGuestListSnapshot.Default(Now.AddDays(-1), CreatorId1),
                    SubscriberSnapshot.Default(Now.AddDays(-1), SubscriberId1)),
            };

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, new List<ISnapshot> { creatorSnapshot });

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenSimultaniousSnapshots_ShouldMergeCorrectly()
        {
            var creatorSnapshot = new CreatorSnapshot(Now, CreatorId1, new List<CreatorChannelSnapshot> { new CreatorChannelSnapshot(Guid.NewGuid(), 100) });
            var creatorGuestListSnapshot = new CreatorGuestListSnapshot(Now, CreatorId1, new List<string> { "a", "b" });
            var subscriberSnapshot = new SubscriberSnapshot(Now, SubscriberId1, "email", new List<SubscriberChannelSnapshot> { new SubscriberChannelSnapshot(Guid.NewGuid(), 100, Now) });
            var expectedResult = new List<MergedSnapshot> 
            {
                new MergedSnapshot(
                    Now,
                    creatorSnapshot,
                    creatorGuestListSnapshot,
                    subscriberSnapshot),
            };

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, new List<ISnapshot> { creatorSnapshot, subscriberSnapshot, creatorGuestListSnapshot });

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenSequenceOfSnapshots_ShouldReturnSnapshotAtSnapshotTime()
        {
            var input = new List<ISnapshot> 
            {
                new CreatorSnapshot(Now.AddHours(1), CreatorId1, new List<CreatorChannelSnapshot> { new CreatorChannelSnapshot(Guid.NewGuid(), 100) }),
                new CreatorGuestListSnapshot(Now.AddHours(2), CreatorId1, new List<string> { "a", "b" }),
                new SubscriberSnapshot(Now.AddHours(3), SubscriberId1, "email", new List<SubscriberChannelSnapshot> { new SubscriberChannelSnapshot(Guid.NewGuid(), 100, Now) }),
                new CreatorGuestListSnapshot(Now.AddHours(4), CreatorId1, new List<string> { "a", "b" }),
                new CreatorSnapshot(Now.AddHours(5), CreatorId1, new List<CreatorChannelSnapshot> { new CreatorChannelSnapshot(Guid.NewGuid(), 100) }),
                new SubscriberSnapshot(Now.AddHours(5), SubscriberId1, "email", new List<SubscriberChannelSnapshot> { new SubscriberChannelSnapshot(Guid.NewGuid(), 100, Now) }),
                new SubscriberSnapshot(Now.AddHours(6), SubscriberId1, "email", new List<SubscriberChannelSnapshot> { new SubscriberChannelSnapshot(Guid.NewGuid(), 100, Now) })
            };

            var defaultCreatorSnapshot = CreatorSnapshot.Default(Now, CreatorId1);
            var defaultCreatorGuestListSnapshot = CreatorGuestListSnapshot.Default(Now, CreatorId1);
            var defaultSubscriberSnapshot = SubscriberSnapshot.Default(Now, SubscriberId1);

            var expectedResult = new List<MergedSnapshot> 
            {
                new MergedSnapshot(
                    Now,
                    defaultCreatorSnapshot,
                    defaultCreatorGuestListSnapshot,
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(1),
                    (CreatorSnapshot)input[0],
                    defaultCreatorGuestListSnapshot,
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(2),
                    (CreatorSnapshot)input[0],
                    (CreatorGuestListSnapshot)input[1],
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(3),
                    (CreatorSnapshot)input[0],
                    (CreatorGuestListSnapshot)input[1],
                    (SubscriberSnapshot)input[2]),
                new MergedSnapshot(
                    Now.AddHours(4),
                    (CreatorSnapshot)input[0],
                    (CreatorGuestListSnapshot)input[3],
                    (SubscriberSnapshot)input[2]),
                new MergedSnapshot(
                    Now.AddHours(5),
                    (CreatorSnapshot)input[4],
                    (CreatorGuestListSnapshot)input[3],
                    (SubscriberSnapshot)input[5]),
                new MergedSnapshot(
                    Now.AddHours(6),
                    (CreatorSnapshot)input[4],
                    (CreatorGuestListSnapshot)input[3],
                    (SubscriberSnapshot)input[6]),
            };

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, input);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }
    }
}