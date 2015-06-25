namespace Fifthweek.Api.Persistence.Payments
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    /// <summary>
    /// This table is the sum of all the ledger entries plus uncommitted entries for a user.
    /// </summary>
    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class CalculatedAccountBalance
    {
        public CalculatedAccountBalance()
        {
        }

        [Required, Key, Column(Order = 0)]
        public Guid UserId { get; set; }

        [Required, Key, Column(Order = 1)]
        public LedgerAccountType AccountType { get; set; }

        [Required, Key, Column(Order = 2)]
        public DateTime Timestamp { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
