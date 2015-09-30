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

        [Required, Key, Index("IX_DTA_GetNewsfeed", Order = 2)]
        public Guid Id { get; set; }

        [Required, Index, Index("IX_DTA_GetNewsfeed", Order = 0)]
        public Guid ChannelId { get; set; }

        [Required, Optional, NonEquatable]
        public Channel Channel { get; set; }

        [Optional, Index] 
        public Guid? QueueId { get; set; }

        [Optional, NonEquatable]
        public Queue Queue { get; set; }

        [Optional, Index, Index("IX_DTA_GetNewsfeed", Order = 3)]
        public Guid? FileId { get; set; }

        [Optional, NonEquatable]
        public File File { get; set; }

        [Optional, Index, Index("IX_DTA_GetNewsfeed", Order = 4)]
        public Guid? ImageId { get; set; }

        [Optional, NonEquatable]
        public File Image { get; set; }

        [Optional]
        [MaxLength(50000)] // ValidComment.MaxLength
        public string Comment { get; set; }

        [Required, Index("IX_DTA_GetNewsfeed", Order = 1)]
        public DateTime LiveDate { get; set; }

        [Required, Index("IX_DTA_GetNewsfeed", Order = 5)]
        public DateTime CreationDate { get; set; }
    }
}