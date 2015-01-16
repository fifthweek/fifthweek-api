namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
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

        [Optional]
        public Guid? CollectionId { get; set; }

        [Optional, NonEquatable]
        public Collection Collection { get; set; }

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

        /// <remarks>
        /// Warning: not guaranteed to be unique per collection. We have biased availability over consistency since the likelihood and 
        /// impact of duplicates are both low. In this case, the cost of a deadlock is higher than the impact of a duplicate.
        /// </remarks>
        [Optional] 
        public int? QueuePosition { get; set; }

        [Optional, Index]
        public DateTime? LiveDate { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}