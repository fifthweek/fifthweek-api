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
    public class MergeSnapshotsExecutorTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;

        private static readonly UserId CreatorId1 = new UserId(Guid.NewGuid());
        private static readonly UserId SubscriberId1 = new UserId(Guid.NewGuid());

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
            var creatorChannelSnapshot = new CreatorChannelsSnapshot(Now, CreatorId1, new List<CreatorChannelsSnapshotItem> { new CreatorChannelsSnapshotItem(new ChannelId(Guid.NewGuid()), 100) });
            var expectedResult = new MergedSnapshot(
                Now,
                creatorChannelSnapshot,
                CreatorFreeAccessUsersSnapshot.Default(Now, CreatorId1),
                SubscriberChannelsSnapshot.Default(Now, SubscriberId1),
                SubscriberSnapshot.Default(Now, SubscriberId1));

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, new List<ISnapshot> { creatorChannelSnapshot });

            CollectionAssert.AreEqual(new List<MergedSnapshot> { expectedResult }, result.ToList());
        }

        [TestMethod]
        public void WhenOnlyCreatorGuestListSnapshot_ShouldReturnMergedSnapshot()
        {
            var creatorGuestListSnapshot = new CreatorFreeAccessUsersSnapshot(Now, CreatorId1, new List<string> { "a", "b" });
            var expectedResult = new MergedSnapshot(
                Now,
                CreatorChannelsSnapshot.Default(Now, CreatorId1),
                creatorGuestListSnapshot,
                SubscriberChannelsSnapshot.Default(Now, SubscriberId1),
                SubscriberSnapshot.Default(Now, SubscriberId1));

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, new List<ISnapshot> { creatorGuestListSnapshot });

            CollectionAssert.AreEqual(new List<MergedSnapshot> { expectedResult }, result.ToList());
        }

        [TestMethod]
        public void WhenOnlySubscriberChannelSnapshot_ShouldReturnMergedSnapshot()
        {
            var subscriberChannelSnapshot = new SubscriberChannelsSnapshot(Now, SubscriberId1, new List<SubscriberChannelsSnapshotItem> { new SubscriberChannelsSnapshotItem(new ChannelId(Guid.NewGuid()), 100, Now) });

            var expectedResult = new MergedSnapshot(
                Now,
                CreatorChannelsSnapshot.Default(Now, CreatorId1),
                CreatorFreeAccessUsersSnapshot.Default(Now, CreatorId1),
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
                CreatorChannelsSnapshot.Default(Now, CreatorId1),
                CreatorFreeAccessUsersSnapshot.Default(Now, CreatorId1),
                SubscriberChannelsSnapshot.Default(Now, SubscriberId1),
                subscriberSnapshot);

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, new List<ISnapshot> { subscriberSnapshot });

            CollectionAssert.AreEqual(new List<MergedSnapshot> { expectedResult }, result.ToList());
        }

        [TestMethod]
        public void WhenFirstSnapshotAfterStartTime_ShouldReturnSnapshotAtStartTimeAndSnapshotAtSnapshotTime()
        {
            var creatorSnapshot = new CreatorChannelsSnapshot(Now.AddDays(1), CreatorId1, new List<CreatorChannelsSnapshotItem> { new CreatorChannelsSnapshotItem(new ChannelId(Guid.NewGuid()), 100) });
            var expectedResult = new List<MergedSnapshot> 
            {
                new MergedSnapshot(
                    Now,
                    CreatorChannelsSnapshot.Default(Now, CreatorId1),
                    CreatorFreeAccessUsersSnapshot.Default(Now, CreatorId1),
                    SubscriberChannelsSnapshot.Default(Now, SubscriberId1),
                    SubscriberSnapshot.Default(Now, SubscriberId1)),
                new MergedSnapshot(
                    Now.AddDays(1),
                    creatorSnapshot,
                    CreatorFreeAccessUsersSnapshot.Default(Now, CreatorId1),
                    SubscriberChannelsSnapshot.Default(Now, SubscriberId1),
                    SubscriberSnapshot.Default(Now, SubscriberId1)),
            };

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, new List<ISnapshot> { creatorSnapshot });

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenFirstSnapshotBeforeStartTime_ShouldReturnSnapshotAtSnapshotTime()
        {
            var creatorSnapshot = new CreatorChannelsSnapshot(Now.AddDays(-1), CreatorId1, new List<CreatorChannelsSnapshotItem> { new CreatorChannelsSnapshotItem(new ChannelId(Guid.NewGuid()), 100) });
            var expectedResult = new List<MergedSnapshot> 
            {
                new MergedSnapshot(
                    Now.AddDays(-1),
                    creatorSnapshot,
                    CreatorFreeAccessUsersSnapshot.Default(Now.AddDays(-1), CreatorId1),
                    SubscriberChannelsSnapshot.Default(Now.AddDays(-1), SubscriberId1),
                    SubscriberSnapshot.Default(Now.AddDays(-1), SubscriberId1)),
            };

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, new List<ISnapshot> { creatorSnapshot });

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        public void WhenSimultaniousSnapshots_ShouldMergeCorrectly()
        {
            var creatorChannelSnapshot = new CreatorChannelsSnapshot(Now, CreatorId1, new List<CreatorChannelsSnapshotItem> { new CreatorChannelsSnapshotItem(new ChannelId(Guid.NewGuid()), 100) });
            var creatorGuestListSnapshot = new CreatorFreeAccessUsersSnapshot(Now, CreatorId1, new List<string> { "a", "b" });
            var subscriberChannelSnapshot = new SubscriberChannelsSnapshot(Now, SubscriberId1, new List<SubscriberChannelsSnapshotItem> { new SubscriberChannelsSnapshotItem(new ChannelId(Guid.NewGuid()), 100, Now) });
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
                new CreatorChannelsSnapshot(Now.AddHours(1), CreatorId1, new List<CreatorChannelsSnapshotItem> { new CreatorChannelsSnapshotItem(new ChannelId(Guid.NewGuid()), 100) }),
                new CreatorFreeAccessUsersSnapshot(Now.AddHours(2), CreatorId1, new List<string> { "a", "b" }),
                new SubscriberChannelsSnapshot(Now.AddHours(3), SubscriberId1, new List<SubscriberChannelsSnapshotItem> { new SubscriberChannelsSnapshotItem(new ChannelId(Guid.NewGuid()), 100, Now) }),
                new CreatorFreeAccessUsersSnapshot(Now.AddHours(4), CreatorId1, new List<string> { "a", "b" }),
                new CreatorChannelsSnapshot(Now.AddHours(5), CreatorId1, new List<CreatorChannelsSnapshotItem> { new CreatorChannelsSnapshotItem(new ChannelId(Guid.NewGuid()), 100) }),
                new SubscriberChannelsSnapshot(Now.AddHours(5), SubscriberId1, new List<SubscriberChannelsSnapshotItem> { new SubscriberChannelsSnapshotItem(new ChannelId(Guid.NewGuid()), 100, Now) }),
                new SubscriberChannelsSnapshot(Now.AddHours(6), SubscriberId1, new List<SubscriberChannelsSnapshotItem> { new SubscriberChannelsSnapshotItem(new ChannelId(Guid.NewGuid()), 100, Now) }),
                new SubscriberSnapshot(Now.AddHours(7), SubscriberId1, "email"),
            };

            var defaultCreatorChannelSnapshot = CreatorChannelsSnapshot.Default(Now, CreatorId1);
            var defaultCreatorGuestListSnapshot = CreatorFreeAccessUsersSnapshot.Default(Now, CreatorId1);
            var defaultSubscriberChannelSnapshot = SubscriberChannelsSnapshot.Default(Now, SubscriberId1);
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
                    (CreatorChannelsSnapshot)input[0],
                    defaultCreatorGuestListSnapshot,
                    defaultSubscriberChannelSnapshot,
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(2),
                    (CreatorChannelsSnapshot)input[0],
                    (CreatorFreeAccessUsersSnapshot)input[1],
                    defaultSubscriberChannelSnapshot,
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(3),
                    (CreatorChannelsSnapshot)input[0],
                    (CreatorFreeAccessUsersSnapshot)input[1],
                    (SubscriberChannelsSnapshot)input[2],
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(4),
                    (CreatorChannelsSnapshot)input[0],
                    (CreatorFreeAccessUsersSnapshot)input[3],
                    (SubscriberChannelsSnapshot)input[2],
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(5),
                    (CreatorChannelsSnapshot)input[4],
                    (CreatorFreeAccessUsersSnapshot)input[3],
                    (SubscriberChannelsSnapshot)input[5],
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(6),
                    (CreatorChannelsSnapshot)input[4],
                    (CreatorFreeAccessUsersSnapshot)input[3],
                    (SubscriberChannelsSnapshot)input[6],
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(7),
                    (CreatorChannelsSnapshot)input[4],
                    (CreatorFreeAccessUsersSnapshot)input[3],
                    (SubscriberChannelsSnapshot)input[6],
                    (SubscriberSnapshot)input[7]),
            };

            var result = this.target.Execute(SubscriberId1, CreatorId1, Now, input);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }
    }
}