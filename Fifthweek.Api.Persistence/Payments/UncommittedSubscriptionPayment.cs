namespace Fifthweek.Api.Persistence.Payments
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.CodeGeneration;

    /// <summary>
    /// This table contains subscription payments that have not yet
    /// been committed to the AppendOnlyLedger because they are still
    /// estimates.  For example, we estimate a subscriber's payments on the
    /// assumption that the creator will post during the billing week.
    /// This is still an estimate because until the billing week ends we
    /// do not know if this happened.
    /// </summary>
    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class UncommittedSubscriptionPayment
    {
        public UncommittedSubscriptionPayment()
        {
        }

        [Required, Key, Column(Order = 0)]
        public Guid SubscriberId { get; set; }

        [Required, Key, Column(Order = 1)]
        public Guid CreatorId { get; set; }

        [Required]
        public DateTime StartTimestampInclusive { get; set; }

        [Required]
        public DateTime EndTimestampExclusive { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public Guid InputDataReference { get; set; }
    }
}