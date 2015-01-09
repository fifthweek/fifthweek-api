namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.Api.Core;

    public class FileVariant
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required, Optional]
        public File File { get; set; }

        public Guid FileId { get; set; }

        [Required]
        public long BlobSizeBytes { get; set; }

        [Required]
        public string MimeType { get; set; }

        [Required]
        public string VariantType { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}