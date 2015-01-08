using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fifthweek.Api.Persistence
{
    public class Subscription
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [ForeignKey("CreatorId")]
        [Required]
        public Guid CreatorId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Tagline { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}