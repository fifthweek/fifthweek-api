using System;
using System.ComponentModel.DataAnnotations;

namespace Fifthweek.Api.Persistence
{
    public class Channel
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Subscription Subscription { get; set; }

        public Guid SubscriptionId { get; set; } 

        [Required]
        public int PriceInUsCentsPerWeek { get; set; }

        [Required]
        public DateTime CreationDate { get; set; } 
    }
}