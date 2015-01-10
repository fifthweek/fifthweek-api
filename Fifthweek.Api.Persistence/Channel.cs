using System;
using System.ComponentModel.DataAnnotations;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Persistence
{
    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class Channel
    {
        public Channel()
        {
        }

        [Required, Key]
        public Guid Id { get; set; }

        [Required]
        public Guid SubscriptionId { get; set; }

        public Subscription Subscription { get; set; }

        [Required]
        public int PriceInUsCentsPerWeek { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}