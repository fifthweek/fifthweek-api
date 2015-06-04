using System;
using System.Text;
using System.Threading.Tasks;

namespace Fifthweek.Payments
{
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;

    [AutoConstructor]
    public partial class PaymentProcessor
    {
        private readonly ILoadSnapshotsExecutor loadSnapshots;
        private readonly IVerifySnapshotsExecutor verifySnapshots;
        private readonly IMergeSnapshotsExecutor mergeSnapshots;
        private readonly IRollBackSubscriptionsExecutor rollBackSubscriptions;
        private readonly IRollForwardSubscriptionsExecutor rollForwardSubscriptions;
        private readonly ITrimSnapshotsExecutor trimSnapshots;
        private readonly ICalculateCostPeriodsExecutor calculateCostPeriods;
        private readonly IAggregateCostPeriodsExecutor aggregateCostPeriods;

        public AggregateCostSummary CalculatePayment(
            Guid subscriberId,
            Guid creatorId,
            DateTime startTimeInclusive,
            DateTime endTimeExclusive)
        {
            var snapshots = this.loadSnapshots.Execute(subscriberId, creatorId, startTimeInclusive, endTimeExclusive);
            this.verifySnapshots.Execute(startTimeInclusive, endTimeExclusive, subscriberId, creatorId, snapshots);
            var mergedSnapshots = this.mergeSnapshots.Execute(subscriberId, creatorId, startTimeInclusive, snapshots);
            var rolledBackSnapshots = this.rollBackSubscriptions.Execute(mergedSnapshots);
            var rolledForwardSnapshots = this.rollForwardSubscriptions.Execute(endTimeExclusive, rolledBackSnapshots);
            var trimmedSnapshots = this.trimSnapshots.Execute(startTimeInclusive, rolledForwardSnapshots);
            var costPeriods = this.calculateCostPeriods.Execute(startTimeInclusive, endTimeExclusive, trimmedSnapshots);
            var aggregateCost = this.aggregateCostPeriods.Execute(costPeriods);
            return aggregateCost;
        }
    }
}
