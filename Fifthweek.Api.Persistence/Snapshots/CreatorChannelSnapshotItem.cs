namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class CreatorChannelSnapshotItem
    {
        public CreatorChannelSnapshotItem()
        {
        }

        [Required, Key, Column(Order = 0)]
        public Guid CreatorChannelSnapshotId { get; set; }

        [Required, Optional, NonEquatable]
        public CreatorChannelSnapshot CreatorChannelSnapshot { get; set; }

        [Required, Key, Column(Order = 1)]
        public Guid ChannelId { get; set; }

        [Required]
        public int PriceInUsCentsPerWeek { get; set; }
    }
}