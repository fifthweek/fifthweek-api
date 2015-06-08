namespace Fifthweek.Payments.Tests.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Payments.Pipeline;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CalculateCostPeriodsExecutorTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;

        private static readonly Guid CreatorId1 = Guid.NewGuid();
        private static readonly Guid SubscriberId1 = Guid.NewGuid();

        private Mock<ICalculateSnapshotCostExecutor> costCalculator;
        private CalculateCostPeriodsExecutor target;

        [TestInitialize]
        public void Initialize()
        {
            this.costCalculator = new Mock<ICalculateSnapshotCostExecutor>();
            this.target = new CalculateCostPeriodsExecutor(this.costCalculator.Object);
        }

        [TestMethod]
        public void WhenNoSnapshots_ItShouldReturnEmptyList()
        {
            var result = this.target.Execute(Now, Now.AddDays(7), new List<MergedSnapshot>());
            CollectionAssert.AreEqual(new List<CostPeriod>(), result.ToList());
        }

        [TestMethod]
        public void WhenOneSnapshot_ItShouldReturnCostPeriod()
        {
            var defaultCreatorChannelsSnapshot = CreatorChannelSnapshot.Default(Now, CreatorId1);
            var defaultCreatorGuestListSnapshot = CreatorGuestListSnapshot.Default(Now, CreatorId1);
            var defaultSubscriberChannelsSnapshot = SubscriberChannelSnapshot.Default(Now, SubscriberId1);
            var defaultSubscriberSnapshot = SubscriberSnapshot.Default(Now, SubscriberId1);

            var mergedSnapshots = new List<MergedSnapshot> 
            {
                new MergedSnapshot(
                    Now.AddHours(5),
                    defaultCreatorChannelsSnapshot,
                    defaultCreatorGuestListSnapshot,
                    defaultSubscriberChannelsSnapshot,
                    defaultSubscriberSnapshot),
            };

            this.costCalculator.Setup(v => v.Execute(It.IsAny<MergedSnapshot>()))
                .Returns<MergedSnapshot>(s => (int)Math.Round((s.Timestamp - Now).TotalHours + 10));

            var expectedOutput = new List<CostPeriod> 
            {
                new CostPeriod(Now.AddHours(5), Now.AddHours(10), 15),
            };

            var result = this.target.Execute(Now, Now.AddHours(10), mergedSnapshots);

            CollectionAssert.AreEqual(expectedOutput, result.ToList());
        }

        [TestMethod]
        public void WhenSeriesOfSnapshots_ItShouldReturnCostPeriods()
        {
            var snapshots = new List<ISnapshot> 
            {
                new CreatorChannelSnapshot(Now.AddHours(1), CreatorId1, new List<CreatorChannelSnapshotItem> { new CreatorChannelSnapshotItem(Guid.NewGuid(), 100) }),
                new CreatorGuestListSnapshot(Now.AddHours(2), CreatorId1, new List<string> { "a", "b" }),
                new SubscriberChannelSnapshot(Now.AddHours(3), SubscriberId1, new List<SubscriberChannelSnapshotItem> { new SubscriberChannelSnapshotItem(Guid.NewGuid(), 100, Now) }),
                new CreatorGuestListSnapshot(Now.AddHours(4), CreatorId1, new List<string> { "a", "b" }),
                new CreatorChannelSnapshot(Now.AddHours(5), CreatorId1, new List<CreatorChannelSnapshotItem> { new CreatorChannelSnapshotItem(Guid.NewGuid(), 100) }),
                new SubscriberChannelSnapshot(Now.AddHours(5), SubscriberId1, new List<SubscriberChannelSnapshotItem> { new SubscriberChannelSnapshotItem(Guid.NewGuid(), 100, Now) }),
                new SubscriberSnapshot(Now.AddHours(6), SubscriberId1, "email"),
                new SubscriberChannelSnapshot(Now.AddHours(6), SubscriberId1, new List<SubscriberChannelSnapshotItem> { new SubscriberChannelSnapshotItem(Guid.NewGuid(), 100, Now) })
            };

            var defaultCreatorChannelSnapshot = CreatorChannelSnapshot.Default(Now, CreatorId1);
            var defaultCreatorGuestListSnapshot = CreatorGuestListSnapshot.Default(Now, CreatorId1);
            var defaultSubscriberChannelSnapshot = SubscriberChannelSnapshot.Default(Now, SubscriberId1);
            var defaultSubscriberSnapshot = SubscriberSnapshot.Default(Now, SubscriberId1);

            var mergedSnapshots = new List<MergedSnapshot> 
            {
                new MergedSnapshot(
                    Now,
                    defaultCreatorChannelSnapshot,
                    defaultCreatorGuestListSnapshot,
                    defaultSubscriberChannelSnapshot,
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(1),
                    (CreatorChannelSnapshot)snapshots[0],
                    defaultCreatorGuestListSnapshot,
                    defaultSubscriberChannelSnapshot,
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(2),
                    (CreatorChannelSnapshot)snapshots[0],
                    (CreatorGuestListSnapshot)snapshots[1],
                    defaultSubscriberChannelSnapshot,
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(3),
                    (CreatorChannelSnapshot)snapshots[0],
                    (CreatorGuestListSnapshot)snapshots[1],
                    (SubscriberChannelSnapshot)snapshots[2],
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(4),
                    (CreatorChannelSnapshot)snapshots[0],
                    (CreatorGuestListSnapshot)snapshots[3],
                    (SubscriberChannelSnapshot)snapshots[2],
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(5),
                    (CreatorChannelSnapshot)snapshots[4],
                    (CreatorGuestListSnapshot)snapshots[3],
                    (SubscriberChannelSnapshot)snapshots[5],
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(6),
                    (CreatorChannelSnapshot)snapshots[4],
                    (CreatorGuestListSnapshot)snapshots[3],
                    (SubscriberChannelSnapshot)snapshots[7],
                    (SubscriberSnapshot)snapshots[6]),
            };

            this.costCalculator.Setup(v => v.Execute(It.IsAny<MergedSnapshot>()))
                .Returns<MergedSnapshot>(s => (int)Math.Round((s.Timestamp - Now).TotalHours + 10));

            var expectedOutput = new List<CostPeriod> 
            {
                new CostPeriod(Now.AddHours(0), Now.AddHours(1), 10),
                new CostPeriod(Now.AddHours(1), Now.AddHours(2), 11),
                new CostPeriod(Now.AddHours(2), Now.AddHours(3), 12),
                new CostPeriod(Now.AddHours(3), Now.AddHours(4), 13),
                new CostPeriod(Now.AddHours(4), Now.AddHours(5), 14),
                new CostPeriod(Now.AddHours(5), Now.AddHours(6), 15),
                new CostPeriod(Now.AddHours(6), Now.AddHours(10), 16),
            };

            var result = this.target.Execute(Now, Now.AddHours(10), mergedSnapshots);

            CollectionAssert.AreEqual(expectedOutput, result.ToList());
        }
    }
}