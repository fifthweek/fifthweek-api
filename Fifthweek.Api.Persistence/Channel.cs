using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fifthweek.Api.Persistence
{
    public class Channel
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [ForeignKey("SubscriptionId")]
        [Required]
        public Guid SubscriptionId { get; set; }

        [Required]
        public int PriceInUsCentsPerWeek { get; set; }

        [Required]
        public DateTime CreationDate { get; set; } 
    }
}