namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class FreePost
    {
        public FreePost()
        {
        }

        // No Foreign key because if the user is deleted we want to keep the record of the post having been read for free.
        [Required, Key, Column(Order = 0), Index("IX_DTA_CountFreePostsOnDate", Order = 0)]
        public Guid UserId { get; set; }

        [Required, Key, Column(Order = 1), Index]
        public Guid PostId { get; set; }

        [Required, Optional, NonEquatable]
        public Post Post { get; set; }

        [Required, Index("IX_DTA_CountFreePostsOnDate", Order = 1)]
        public DateTime Timestamp { get; set; }
    }
}