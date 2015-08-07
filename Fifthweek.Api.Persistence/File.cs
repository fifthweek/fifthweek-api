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
        // This is the limit on some OSs. Again, somewhat arbitrary, but a safe limitation for us to enforce.
        public const int MaximumFileNameLength = 255;

        // Arbitrary but seems safe. Should really be enforcing this through the type system (ValidFileExension type).
        public const int MaximumFileExtensionLength = 25;

        public File()
        {
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        // No Foreign key because if the user is deleted we need to keep the file rows to enable garbage collection to find it.
        [Required, Index]
        public Guid UserId { get; set; }

        // No Foreign key because if the user deletes a channel we need to keep the file rows to enable garbage collection to find it.
        [Optional, Index]
        public Guid? ChannelId { get; set; }

        [Required]
        public FileState State { get; set; }

        [Required, Index]
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
        [MaxLength(MaximumFileNameLength)]
        public string FileNameWithoutExtension { get; set; }
        
        [Required]
        [MaxLength(MaximumFileExtensionLength)]
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