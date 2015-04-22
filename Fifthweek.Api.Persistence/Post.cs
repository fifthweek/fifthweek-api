namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.CodeGeneration;

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
        /// We moved away from a QueueIndex due to the added complexity and poor UX it introduced. Specifically, if the background dequeue 
        /// process became delayed, users would see posts that should have gone live remain at the front of their queue. Then, since live 
        /// dates would have been deferred from the current date, they would have seen all their posts shift back a release index. By 
        /// calculating queued post live dates upfront and storing them against the entity, we ensure users always see the same date 
        /// against their post (unless they change it), and the post will automatically appear live without depending on and external 
        /// process to flip it live.
        /// </remarks>
        [Required]
        public bool ScheduledByQueue { get; set; }

        [Required]
        [Index("IX_LiveDateAndCreationDate", 1)]
        public DateTime LiveDate { get; set; }

        [Required]
        [Index("IX_LiveDateAndCreationDate", 2)]
        public DateTime CreationDate { get; set; }
    }
}