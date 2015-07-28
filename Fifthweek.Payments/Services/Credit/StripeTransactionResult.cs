namespace Fifthweek.Payments.Services.Credit
{
    using System;

    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;

    [AutoConstructor, AutoEqualityMembers]
    public partial class StripeTransactionResult
    {
        public DateTime Timestamp { get; private set; }

        public TransactionReference TransactionReference { get; private set; }

        public string StripeChargeId { get; private set; }
    }
}