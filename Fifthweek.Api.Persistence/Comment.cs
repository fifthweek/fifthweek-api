namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class Comment
    {
        public Comment()
        {
        }

        [Required, Key]
        public Guid Id { get; set; }

        [Required, Index("PostIdAndCreationDate", Order = 1)]
        public Guid PostId { get; set; }

        [Required, Optional, NonEquatable]
        public Post Post { get; set; }

        [Required, Index]
        public Guid UserId { get; set; }

        [Required, Optional, NonEquatable]
        public FifthweekUser User { get; set; }

        [MaxLength(50000)]
        public string Content { get; set; }

        [Required, Index("PostIdAndCreationDate", Order = 2)]
        public DateTime CreationDate { get; set; }
    }
}