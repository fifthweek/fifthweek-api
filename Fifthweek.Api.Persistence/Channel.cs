namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.Api.Core;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class Channel
    {
        public Channel()
        {
        }

        [Required, Key]
        public Guid Id { get; set; }

        [Required, Index]
        public Guid SubscriptionId { get; set; }

        [Required, Optional, NonEquatable]
        public Subscription Subscription { get; set; }

        [Required]
        public int PriceInUsCentsPerWeek { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}