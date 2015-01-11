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

        [Required, Key]
        public Guid Id { get; set; }

        [Required, Optional, NonEquatable]
        public FifthweekUser Creator { get; set; }

        public Guid CreatorId { get; set; }

        [Optional]
        public string Name { get; set; }

        [Optional]
        public string Tagline { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}