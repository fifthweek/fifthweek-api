namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public class CreatorGuestListSnapshotItem
    {
        [Required, Key]
        public Guid CreatorGuestListSnapshotId { get; set; }

        [Required, Optional, NonEquatable]
        public CreatorGuestListSnapshot CreatorGuestListSnapshot { get; set; }

        [Required, Key]
        public string Email { get; set; }
    }
}