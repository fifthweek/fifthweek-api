namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Snapshots;

    public interface ISubscriberPaymentPipeline
    {
        AggregateCostSummary CalculatePayment(
            IReadOnlyList<ISnapshot> snapshots,
            UserId subscriberId,
            UserId creatorId,
            DateTime startTimeInclusive,
            DateTime endTimeExclusive);
    }
}