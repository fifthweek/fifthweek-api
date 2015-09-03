namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class Like
    {
        public Like()
        {
        }

        [Required, Key, Column(Order = 0)]
        public Guid PostId { get; set; }

        [Required, Optional, NonEquatable]
        public Post Post { get; set; }

        [Required, Key, Column(Order = 1), Index]
        public Guid UserId { get; set; }

        [Required, Optional, NonEquatable]
        public FifthweekUser User { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}