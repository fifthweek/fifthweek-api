namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class File
    {
        public File()
        {
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required, Optional, NonEquatable]
        [InverseProperty("Files")]
        public FifthweekUser User { get; set; }

        public Guid UserId { get; set; }

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

        [Required]
        public string FileNameWithoutExtension { get; set; }
        
        [Required]
        public string FileExtension { get; set; }

        [Required]
        public long BlobSizeBytes { get; set; }

        [Required]
        public string Purpose { get; set; }
    }
}