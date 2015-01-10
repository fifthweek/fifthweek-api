using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Persistence
{
    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class Channel
    {
        public Channel()
        {
        }

        [Key, Column(Order = 0)]
        public Guid? Id { get; set; }

        [Key, Column(Order = 1), Required]
        public Guid SubscriptionId { get; set; }

        public Subscription Subscription { get; set; }

        [Required]
        public int PriceInUsCentsPerWeek { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}