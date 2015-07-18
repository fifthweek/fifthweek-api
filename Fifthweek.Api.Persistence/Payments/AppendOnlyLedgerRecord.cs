namespace Fifthweek.Api.Persistence.Payments
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.CodeGeneration;

    /// <summary>
    /// This table is an append-only leger of transactions in a double-entry
    /// style of book keeping. No records should ever be modified, inserted
    /// or deleted, only appended.
    /// Note that due to clock differences inserts may occur towards the very
    /// end of the ledger, and this should be accounted for when calculating
    /// the current balance by working from a known safe point, which can simply
    /// be a time further back than the maximum possible clock drift, or even zero.
    /// 
    /// The UniqueKey is a safety measure to ensure we don't insert duplicate records by 
    /// processing the same data twice. However I felt it was too cumbersome to be the primary key,
    /// and kept that as a simple unique ID.
    /// 
    /// The column order of UniqueKey is important for GetLatestCommittedLedgerDateDbStatement.
    /// </summary>
    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class AppendOnlyLedgerRecord
    {
        public AppendOnlyLedgerRecord()
        {
        }

        [Required, Key]
        public Guid Id { get; set; }

        [Required, Index("UniqueKey", Order = 0, IsUnique = true), Index("TimestampAndAccountOwner", Order = 1)]
        public Guid AccountOwnerId { get; set; }

        [Optional, Index("UniqueKey", Order = 1, IsUnique = true)]
        public Guid? CounterpartyId { get; set; }

        [Required, Index("UniqueKey", Order = 3, IsUnique = true), Index("TimestampAndAccountOwner", Order = 0)]
        public DateTime Timestamp { get; set; }

        [Required, Index("UniqueKey", Order = 4, IsUnique = true)]
        public decimal Amount { get; set; }

        [Required, Index("UniqueKey", Order = 5, IsUnique = true)]
        public LedgerAccountType AccountType { get; set; }

        [Required, Index("UniqueKey", Order = 2, IsUnique = true)]
        public LedgerTransactionType TransactionType { get; set; }

        [Required, Index]
        public Guid TransactionReference { get; set; }

        [Optional]
        public Guid? InputDataReference { get; set; }

        [Optional]
        public string Comment { get; set; }

        [Optional]
        public string StripeChargeId { get; set; }

        [Optional]
        public string TaxamoTransactionKey { get; set; }
    }
}