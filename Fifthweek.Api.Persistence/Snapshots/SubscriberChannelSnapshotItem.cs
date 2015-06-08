namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class SubscriberChannelSnapshotItem
    {
        public SubscriberChannelSnapshotItem()
        {
        }

        [Required, Key, Column(Order = 0)]
        public Guid SubscriberChannelSnapshotId { get; set; }

        [Required, Optional, NonEquatable]
        public SubscriberChannelSnapshot SubscriberChannelSnapshot { get; set; }

        [Required, Key, Column(Order = 1)]
        public Guid ChannelId { get; set; }

        [Required]
        public int AcceptedPrice { get; set; }

        [Required]
        public DateTime SubscriptionStartDate { get; set; }
    }
}