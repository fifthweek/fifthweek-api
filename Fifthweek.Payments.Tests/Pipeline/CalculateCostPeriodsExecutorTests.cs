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
            var defaultCreatorSnapshot = CreatorSnapshot.Default(Now, CreatorId1);
            var defaultCreatorGuestListSnapshot = CreatorGuestListSnapshot.Default(Now, CreatorId1);
            var defaultSubscriberSnapshot = SubscriberSnapshot.Default(Now, SubscriberId1);

            var mergedSnapshots = new List<MergedSnapshot> 
            {
                new MergedSnapshot(
                    Now.AddHours(5),
                    defaultCreatorSnapshot,
                    defaultCreatorGuestListSnapshot,
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

            var mergedSnapshots = new List<MergedSnapshot> 
            {
                new MergedSnapshot(
                    Now,
                    defaultCreatorSnapshot,
                    defaultCreatorGuestListSnapshot,
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(1),
                    (CreatorSnapshot)snapshots[0],
                    defaultCreatorGuestListSnapshot,
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(2),
                    (CreatorSnapshot)snapshots[0],
                    (CreatorGuestListSnapshot)snapshots[1],
                    defaultSubscriberSnapshot),
                new MergedSnapshot(
                    Now.AddHours(3),
                    (CreatorSnapshot)snapshots[0],
                    (CreatorGuestListSnapshot)snapshots[1],
                    (SubscriberSnapshot)snapshots[2]),
                new MergedSnapshot(
                    Now.AddHours(4),
                    (CreatorSnapshot)snapshots[0],
                    (CreatorGuestListSnapshot)snapshots[3],
                    (SubscriberSnapshot)snapshots[2]),
                new MergedSnapshot(
                    Now.AddHours(5),
                    (CreatorSnapshot)snapshots[4],
                    (CreatorGuestListSnapshot)snapshots[3],
                    (SubscriberSnapshot)snapshots[5]),
                new MergedSnapshot(
                    Now.AddHours(6),
                    (CreatorSnapshot)snapshots[4],
                    (CreatorGuestListSnapshot)snapshots[3],
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