namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class ProcessPaymentProcessingData : IProcessPaymentProcessingData
    {
        private readonly ISubscriberPaymentPipeline subscriberPaymentPipeline;

        public Task<PaymentProcessingResults> ExecuteAsync(PaymentProcessingData data)
        {
            using (PaymentsPerformanceLogger.Instance.Log(typeof(ProcessPaymentProcessingData)))
            {
                data.AssertNotNull("data");

                var subscriberId = data.SubscriberId;
                var creatorId = data.CreatorId;
                DateTime startTimeInclusive = data.StartTimeInclusive;
                DateTime endTimeExclusive = data.EndTimeExclusive;
                IReadOnlyList<ISnapshot> orderedSnapshots = data.GetOrderedSnapshots();
                IReadOnlyList<CreatorPost> posts = data.CreatorPosts;

                var committedAccountBalance = data.CommittedAccountBalance;

                var currentStartTimeInclusive = startTimeInclusive;
                var currentEndTimeExclusive = startTimeInclusive.AddDays(7);

                // This is the final time at which we can be totally sure if the creator
                // has posted in the billing week, as the subscriber billing week most likely
                // doesn't line up with the payment billing week.
                var committedRecordsEndTimeExclusive = endTimeExclusive.AddDays(-7);

                var result = new List<PaymentProcessingResult>();
                while (currentEndTimeExclusive <= committedRecordsEndTimeExclusive)
                {
                    // Calculate complete week.
                    var cost = this.subscriberPaymentPipeline.CalculatePayment(
                        orderedSnapshots,
                        posts,
                        subscriberId,
                        creatorId,
                        currentStartTimeInclusive,
                        currentEndTimeExclusive);

                    if (cost.Cost > committedAccountBalance.Amount)
                    {
                        cost = new AggregateCostSummary(committedAccountBalance.Amount);
                    }

                    committedAccountBalance = committedAccountBalance.Subtract(cost.Cost);

                    var creatorPercentageOverride = this.GetCreatorPercentageOverride(
                        data.CreatorPercentageOverride,
                        currentEndTimeExclusive);

                    result.Add(new PaymentProcessingResult(currentStartTimeInclusive, currentEndTimeExclusive, cost, creatorPercentageOverride, true));

                    currentStartTimeInclusive = currentEndTimeExclusive;
                    currentEndTimeExclusive = currentStartTimeInclusive.AddDays(7);
                }

                if (currentStartTimeInclusive < endTimeExclusive)
                {
                    // Calculate uncommitted period.
                    // We calculate without taking into account CreatorPosts, 
                    // as we assume they will post until we can be totally sure 
                    // know otherwise.
                    var cost = this.subscriberPaymentPipeline.CalculatePayment(
                        orderedSnapshots,
                        null, 
                        subscriberId,
                        creatorId,
                        currentStartTimeInclusive,
                        endTimeExclusive);

                    var creatorPercentageOverride = this.GetCreatorPercentageOverride(
                        data.CreatorPercentageOverride,
                        endTimeExclusive);

                    result.Add(new PaymentProcessingResult(currentStartTimeInclusive, endTimeExclusive, cost, creatorPercentageOverride, false));
                }

                return Task.FromResult(new PaymentProcessingResults(committedAccountBalance, result));
            }
        }

        private CreatorPercentageOverrideData GetCreatorPercentageOverride(
            CreatorPercentageOverrideData data,
            DateTime endTimeExclusive)
        {
            if (data == null || (data.ExpiryDate != null && data.ExpiryDate < endTimeExclusive))
            {
                return null;
            }

            return data;
        }
    }
}