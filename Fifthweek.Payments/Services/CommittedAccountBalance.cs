namespace Fifthweek.Payments.Services
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoJson]
    public partial class CommittedAccountBalance
    {
        public CommittedAccountBalance(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount must be non-negative.");
            }

            this.Amount = amount;
        }

        public decimal Amount { get; private set; }

        public CommittedAccountBalance Subtract(decimal value)
        {
            return new CommittedAccountBalance(this.Amount - value);
        }
    }
}