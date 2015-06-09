namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class SubscriberChannelsSnapshot
    {
        public SubscriberChannelsSnapshot()
        {
        }

        [Required, Key]
        public Guid Id { get; set; }

        [Required, Index("SubscriberIdAndTimestamp", Order = 1)]
        public DateTime Timestamp { get; set; }

        [Required, Index("SubscriberIdAndTimestamp", Order = 0)] // Not a foreign key.
        public Guid SubscriberId { get; set; } 
    }
}