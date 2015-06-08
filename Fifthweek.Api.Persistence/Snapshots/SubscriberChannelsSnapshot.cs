namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class SubscriberChannelsSnapshot
    {
        public SubscriberChannelsSnapshot()
        {
        }

        [Required, Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required] // Not a foreign key.
        public Guid SubscriberId { get; set; } 
    }
}