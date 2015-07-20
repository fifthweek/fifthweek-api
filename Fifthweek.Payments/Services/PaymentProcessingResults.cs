namespace Fifthweek.Payments.Services
{
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class PaymentProcessingResults
    {
        public CommittedAccountBalance CommittedAccountBalance { get; private set; }

        public IReadOnlyList<PaymentProcessingResult> Items { get; private set; }
    }
}