namespace Fifthweek.Payments.Services
{
    using System;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class CalculatedAccountBalanceResult
    {
        public DateTime Timestamp { get; private set; }

        public UserId UserId { get; private set; }

        public LedgerAccountType AccountType { get; private set; }

        public decimal Amount { get; private set; }
    }
}