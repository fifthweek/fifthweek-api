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
            data.AssertNotNull("data");

            var subscriberId = data.SubscriberId;
            var creatorId = data.CreatorId;
            DateTime startTimeInclusive = data.StartTimeInclusive;
            DateTime endTimeExclusive = data.EndTimeExclusive;
            IReadOnlyList<ISnapshot> orderedSnapshots = data.GetOrderedSnapshots();
            IReadOnlyList<CreatorPost> posts = data.CreatorPosts;

            var currentStartTimeInclusive = startTimeInclusive;
            var currentEndTimeExclusive = startTimeInclusive.AddDays(7);

            var result = new List<PaymentProcessingResult>();
            while (currentEndTimeExclusive <= endTimeExclusive)
            {
                // Calculate complete week.
                var cost = this.subscriberPaymentPipeline.CalculatePayment(
                    orderedSnapshots,
                    posts,
                    subscriberId,
                    creatorId,
                    currentStartTimeInclusive,
                    currentEndTimeExclusive);

                var creatorPercentageOverride = this.GetCreatorPercentageOverride(
                    data.CreatorPercentageOverride,
                    currentEndTimeExclusive);

                result.Add(new PaymentProcessingResult(currentStartTimeInclusive, currentEndTimeExclusive, cost, creatorPercentageOverride, true));

                currentStartTimeInclusive = currentEndTimeExclusive;
                currentEndTimeExclusive = currentStartTimeInclusive.AddDays(7);
            }

            if (currentStartTimeInclusive < endTimeExclusive)
            {
                // Calculate partial week.
                // We don't calculate taking into account CreatorPosts, 
                // as until the week is over we can't be sure if the creator will post
                // and we assume they will until we know otherwise.
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

            return Task.FromResult(new PaymentProcessingResults(result));
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