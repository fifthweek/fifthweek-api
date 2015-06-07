namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public class SubscriberChannelSnapshotItem
    {
        [Required, Key]
        public Guid SubscriberChannelSnapshotId { get; set; }

        [Required, Optional, NonEquatable]
        public SubscriberChannelSnapshot SubscriberChannelSnapshot { get; set; }

        [Required, Key]
        public Guid ChannelId { get; set; }

        [Required]
        public int AcceptedPrice { get; set; }

        [Required]
        public DateTime SubscriptionStartDate { get; set; }
    }
}