namespace Fifthweek.Payments.Services
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class PaymentProcessingResult
    {
        public DateTime StartTimeInclusive { get; private set; }

        public DateTime EndTimeExclusive { get; private set; }

        public AggregateCostSummary SubscriptionCost { get; private set; }

        [Optional]
        public CreatorPercentageOverrideData CreatorPercentageOverride { get; private set; }

        public bool IsCommitted { get; private set; }
    }
}