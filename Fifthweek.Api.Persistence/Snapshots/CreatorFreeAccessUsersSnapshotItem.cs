namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class CreatorFreeAccessUsersSnapshotItem
    {
        public CreatorFreeAccessUsersSnapshotItem()
        {
        }

        [Required, Key, Column(Order = 0)]
        public Guid CreatorFreeAccessUsersSnapshotId { get; set; }

        [Required, Optional, NonEquatable]
        public CreatorFreeAccessUsersSnapshot CreatorFreeAccessUsersSnapshot { get; set; }

        [Required, Key, Column(Order = 1)]
        [MaxLength(256)] // See: ValidEmail.MaxLength
        public string Email { get; set; }
    }
}