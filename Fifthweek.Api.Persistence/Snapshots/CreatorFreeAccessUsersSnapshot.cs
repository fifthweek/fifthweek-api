namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class CreatorFreeAccessUsersSnapshot
    {
        public CreatorFreeAccessUsersSnapshot()
        {
        }

        [Required, Key]
        public Guid Id { get; set; }

        [Required, Index("CreatorIdAndTimestamp", Order = 1)]
        public DateTime Timestamp { get; set; }

        [Required, Index("CreatorIdAndTimestamp", Order = 0)] // Not a foreign key.
        public Guid CreatorId { get; set; }
    }
}