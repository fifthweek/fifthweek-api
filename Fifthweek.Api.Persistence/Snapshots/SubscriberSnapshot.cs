namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public class SubscriberSnapshot
    {
        [Required, Key]
        public DateTime Timestamp { get; set; }

        [Required, Key] // Not a foreign key.
        public Guid SubscriberId { get; set; }

        [Required]
        public string Email { get; set; }
    }
}