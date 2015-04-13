namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class ChannelSubscription
    {
        public ChannelSubscription()
        {
        }

        [Required, Key, Column(Order = 0)]
        public Guid ChannelId { get; set; }

        [Required, Optional, NonEquatable]
        public Channel Channel { get; set; }

        [Required, Key, Column(Order = 1), Index]
        public Guid UserId { get; set; }

        [Required, Optional, NonEquatable]
        public FifthweekUser User { get; set; }

        [Required]
        public int AcceptedPriceInUsCentsPerWeek { get; set; }

        [Required]
        public DateTime PriceLastAcceptedDate { get; set; }

        [Required]
        public DateTime SubscriptionStartDate { get; set; }
    }
}