namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public enum AssetUploadState
    {
        WaitingForClient,
        UploadCompleted,
        Processing,
        ProcessingCompleted
    }

    public class File
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("UserId")]
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public AssetUploadState State { get; set; }

        [Required]
        public string BlobReference { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public DateTime CompletionDate { get; set; }

        public DateTime ProcessedDate { get; set; }

        [Required]
        public string FileName { get; set; }

        public string MimeType { get; set; }
    }
}