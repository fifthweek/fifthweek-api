namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public class CreatorChannelSnapshotItem
    {
        [Required, Key]
        public Guid CreatorChannelSnapshotId { get; set; }

        [Required, Optional, NonEquatable]
        public CreatorChannelSnapshot CreatorChannelSnapshot { get; set; }

        [Required, Key]
        public Guid ChannelId { get; set; }

        [Required]
        public int PriceInUsCentsPerWeek { get; set; }
    }
}