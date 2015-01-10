using System;
using System.ComponentModel.DataAnnotations;
using Fifthweek.Api.Core;
using Fifthweek.Api.Persistence.Identity;

namespace Fifthweek.Api.Persistence
{
    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class Subscription
    {
        public Subscription()
        {
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required, Optional]
        public FifthweekUser Creator { get; set; }

        public Guid CreatorId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Tagline { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}