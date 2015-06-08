namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class CreatorChannelsSnapshotItem
    {
        public CreatorChannelsSnapshotItem()
        {
        }

        [Required, Key, Column(Order = 0)]
        public Guid CreatorChannelsSnapshotId { get; set; }

        [Required, Optional, NonEquatable]
        public CreatorChannelsSnapshot CreatorChannelsSnapshot { get; set; }

        [Required, Key, Column(Order = 1)]
        public Guid ChannelId { get; set; }

        [Required]
        public int PriceInUsCentsPerWeek { get; set; }
    }
}