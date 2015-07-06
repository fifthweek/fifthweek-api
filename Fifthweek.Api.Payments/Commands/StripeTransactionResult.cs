namespace Fifthweek.Api.Payments.Commands
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class StripeTransactionResult
    {
        public DateTime Timestamp { get; private set; }

        public Guid TransactionReference { get; private set; }

        public string StripeChargeId { get; private set; }
    }
}