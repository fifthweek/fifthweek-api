namespace Fifthweek.Payments.Snapshots
{
    using System;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class CalculatedAccountBalanceSnapshot : ISnapshot
    {
        public DateTime Timestamp { get; private set; }

        public UserId UserId { get; private set; }

        public LedgerAccountType AccountType { get; private set; }

        public decimal Amount { get; private set; }

        public static CalculatedAccountBalanceSnapshot DefaultFifthweekCreditAccount(DateTime timestamp, UserId userId)
        {
            return new CalculatedAccountBalanceSnapshot(timestamp, userId, LedgerAccountType.FifthweekCredit, 0);
        }

        public static CalculatedAccountBalanceSnapshot Default(DateTime timestamp, UserId userId, LedgerAccountType accountType)
        {
            return new CalculatedAccountBalanceSnapshot(timestamp, userId, accountType, 0);
        }
    }
}