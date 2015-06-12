namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class SubscriberSnapshot
    {
        public SubscriberSnapshot()
        {
        }

        [Required, Key, Column(Order = 1)]
        public DateTime Timestamp { get; set; }

        [Required, Key, Column(Order = 0)] // Not a foreign key.
        public Guid SubscriberId { get; set; }

        [Optional]
        public string Email { get; set; }
    }
}