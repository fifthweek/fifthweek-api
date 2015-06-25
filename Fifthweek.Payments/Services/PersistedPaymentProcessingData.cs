namespace Fifthweek.Payments.Services
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class PersistedPaymentProcessingData
    {
        public Guid Id { get; private set; }

        public PaymentProcessingData Input { get; private set; }

        public PaymentProcessingResults Output { get; private set; }
    }
}