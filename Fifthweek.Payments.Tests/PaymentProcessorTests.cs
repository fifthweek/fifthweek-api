namespace Fifthweek.Payments.Tests
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Payments.Pipeline;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PaymentProcessorTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly DateTime StartTimeInclusive = Now;
        private static readonly DateTime EndTimeExclusive = Now.AddDays(10);
        private static readonly Guid CreatorId1 = Guid.NewGuid();
        private static readonly Guid SubscriberId1 = Guid.NewGuid();

        private Mock<ILoadSnapshotsExecutor> loadSnapshots;
        private Mock<IVerifySnapshotsExecutor> verifySnapshots;
        private Mock<IMergeSnapshotsExecutor> mergeSnapshots;
        private Mock<IRollBackSubscriptionsExecutor> rollBackSubscriptions;
        private Mock<IRollForwardSubscriptionsExecutor> rollForwardSubscriptions;
        private Mock<ITrimSnapshotsExecutor> trimSnapshots;
        private Mock<ICalculateCostPeriodsExecutor> calculateCostPeriods;
        private Mock<IAggregateCostPeriodsExecutor> aggregateCostPeriods;
        private PaymentProcessor target;

        [TestInitialize]
        public void Initialize()
        {
            this.loadSnapshots = new Mock<ILoadSnapshotsExecutor>(MockBehavior.Strict);
            this.verifySnapshots = new Mock<IVerifySnapshotsExecutor>(MockBehavior.Strict);
            this.mergeSnapshots = new Mock<IMergeSnapshotsExecutor>(MockBehavior.Strict);
            this.rollBackSubscriptions = new Mock<IRollBackSubscriptionsExecutor>(MockBehavior.Strict);
            this.rollForwardSubscriptions = new Mock<IRollForwardSubscriptionsExecutor>(MockBehavior.Strict);
            this.trimSnapshots = new Mock<ITrimSnapshotsExecutor>(MockBehavior.Strict);
            this.calculateCostPeriods = new Mock<ICalculateCostPeriodsExecutor>(MockBehavior.Strict);
            this.aggregateCostPeriods = new Mock<IAggregateCostPeriodsExecutor>(MockBehavior.Strict);
            this.target = new PaymentProcessor(
                this.loadSnapshots.Object,
                this.verifySnapshots.Object,
                this.mergeSnapshots.Object,
                this.rollBackSubscriptions.Object,
                this.rollForwardSubscriptions.Object,
                this.trimSnapshots.Object,
                this.calculateCostPeriods.Object,
                this.aggregateCostPeriods.Object);
        }

        [TestMethod]
        public void ItShouldCallServicesInOrder()
        {
            var snapshots = new List<ISnapshot> { CreatorChannelsSnapshot.Default(Now, Guid.NewGuid()) };
            this.loadSnapshots.Setup(v => v.Execute(SubscriberId1, CreatorId1, StartTimeInclusive, EndTimeExclusive))
                .Returns(snapshots);

            this.verifySnapshots.Setup(v => v.Execute(StartTimeInclusive, EndTimeExclusive, SubscriberId1, CreatorId1, snapshots));

            var mergedSnapshots = CreateMergedSnapshotResult();
            this.mergeSnapshots.Setup(v => v.Execute(SubscriberId1, CreatorId1, StartTimeInclusive, snapshots))
                .Returns(mergedSnapshots);

            var rolledBackSnapshots = CreateMergedSnapshotResult();
            this.rollBackSubscriptions.Setup(v => v.Execute(mergedSnapshots)).Returns(rolledBackSnapshots);

            var rolledForwardSnapshots = CreateMergedSnapshotResult();
            this.rollForwardSubscriptions.Setup(v => v.Execute(EndTimeExclusive, rolledBackSnapshots)).Returns(rolledForwardSnapshots);

            var trimmedSnapshots = CreateMergedSnapshotResult();
            this.trimSnapshots.Setup(v => v.Execute(StartTimeInclusive, rolledForwardSnapshots)).Returns(trimmedSnapshots);

            var costPeriods = new List<CostPeriod> { new CostPeriod(StartTimeInclusive, EndTimeExclusive, 101) };
            this.calculateCostPeriods.Setup(v => v.Execute(StartTimeInclusive, EndTimeExclusive, trimmedSnapshots))
                .Returns(costPeriods);

            var aggregateCost = new AggregateCostSummary(201);
            this.aggregateCostPeriods.Setup(v => v.Execute(costPeriods)).Returns(aggregateCost);

            var result = this.target.CalculatePayment(SubscriberId1, CreatorId1, StartTimeInclusive, EndTimeExclusive);

            Assert.AreEqual(aggregateCost, result);
        }

        private static List<MergedSnapshot> CreateMergedSnapshotResult()
        {
            return new List<MergedSnapshot>
            {
                new MergedSnapshot(
                    CreatorChannelsSnapshot.Default(Now, Guid.NewGuid()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, Guid.NewGuid()),
                    SubscriberChannelsSnapshot.Default(Now, Guid.NewGuid()),
                    SubscriberSnapshot.Default(Now, Guid.NewGuid()))
            };
        }
    }
}