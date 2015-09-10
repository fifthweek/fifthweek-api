namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.CodeGeneration;

    /// <summary>
    /// NOTE: The Posts table has a manually defined index with included columns.
    /// See migration 201505151520343_RemoveUnusedIndexOnPostsAndReplaceWithDtaRecommendedIndex.
    /// </summary>
    [AutoConstructor, AutoEqualityMembers, AutoSql, AutoCopy(RequiresBuilder = false)]
    public partial class Post
    {
        public Post()
        {
        }

        [Required, Key]
        public Guid Id { get; set; }

        [Required, Index]
        public Guid ChannelId { get; set; }

        [Required, Optional, NonEquatable]
        public Channel Channel { get; set; }

        [Optional, Index] 
        public Guid? QueueId { get; set; }

        [Optional, NonEquatable]
        public Queue Queue { get; set; }

        [Optional]
        public Guid? FileId { get; set; }

        [Optional, NonEquatable]
        public File File { get; set; }

        [Optional]
        public Guid? ImageId { get; set; }

        [Optional, NonEquatable]
        public File Image { get; set; }

        [Optional]
        [MaxLength(2000)] // Maximum of the two: ValidComment.MaxLength & ValidNote.MaxLength
        public string Comment { get; set; }

        [Required]
        public DateTime LiveDate { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}