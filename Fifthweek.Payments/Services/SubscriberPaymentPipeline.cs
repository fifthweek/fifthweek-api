namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using Fifthweek.Payments.Snapshots;

    [AutoConstructor]
    public partial class SubscriberPaymentPipeline : ISubscriberPaymentPipeline
    {
        private readonly IVerifySnapshotsExecutor verifySnapshots;
        private readonly IMergeSnapshotsExecutor mergeSnapshots;
        private readonly IRollBackSubscriptionsExecutor rollBackSubscriptions;
        private readonly IRollForwardSubscriptionsExecutor rollForwardSubscriptions;
        private readonly ITrimSnapshotsExecutor trimSnapshots;
        private readonly IAddSnapshotsForBillingEndDatesExecutor addSnapshotsForBillingEndDates;
        private readonly ICalculateCostPeriodsExecutor calculateCostPeriods;
        private readonly IAggregateCostPeriodsExecutor aggregateCostPeriods;

        public AggregateCostSummary CalculatePayment(
            IReadOnlyList<ISnapshot> snapshots,
            IReadOnlyList<CreatorPost> creatorPosts,
            UserId subscriberId,
            UserId creatorId,
            DateTime startTimeInclusive,
            DateTime endTimeExclusive)
        {
            this.verifySnapshots.Execute(startTimeInclusive, endTimeExclusive, subscriberId, creatorId, snapshots);
            var mergedSnapshots = this.mergeSnapshots.Execute(subscriberId, creatorId, startTimeInclusive, snapshots);
            var rolledBackSnapshots = this.rollBackSubscriptions.Execute(mergedSnapshots);
            var rolledForwardSnapshots = this.rollForwardSubscriptions.Execute(endTimeExclusive, rolledBackSnapshots);
            var trimmedSnapshots = this.trimSnapshots.Execute(startTimeInclusive, rolledForwardSnapshots);
            var snapshotsWithBilling = this.addSnapshotsForBillingEndDates.Execute(trimmedSnapshots);
            var costPeriods = this.calculateCostPeriods.Execute(startTimeInclusive, endTimeExclusive, snapshotsWithBilling, creatorPosts);
            var aggregateCost = this.aggregateCostPeriods.Execute(costPeriods);
            return aggregateCost;
        }
    }
}
