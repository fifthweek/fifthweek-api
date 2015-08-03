namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class File
    {
        public File()
        {
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required, Optional, NonEquatable]
        [InverseProperty("Files")]
        public FifthweekUser User { get; set; }

        // No Foreign key because if the user deletes a channel we need to keep the file row to enable garbage collection to find it.
        [Optional]
        public Guid? ChannelId { get; set; }

        [Required]
        public FileState State { get; set; }

        [Required]
        public DateTime UploadStartedDate { get; set; }

        [Optional]
        public DateTime? UploadCompletedDate { get; set; }

        [Optional]
        public DateTime? ProcessingStartedDate { get; set; }

        [Optional]
        public DateTime? ProcessingCompletedDate { get; set; }

        [Optional]
        public int? ProcessingAttempts { get; set; }

        [Required]
        [MaxLength(255)] // This is the limit on some OSs. Again, somewhat arbitrary, but a safe limitation for us to enforce.
        public string FileNameWithoutExtension { get; set; }
        
        [Required]
        [MaxLength(25)] // Arbitrary but seems safe. Should really be enforcing this through the type system (ValidFileExension type).
        public string FileExtension { get; set; }

        [Required]
        public long BlobSizeBytes { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Optional]
        public int? RenderWidth { get; set; }

        [Optional]
        public int? RenderHeight { get; set; }
    }
}