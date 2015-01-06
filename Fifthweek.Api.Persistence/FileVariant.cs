namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class FileVariant
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("FileId")]
        [Required]
        public Guid FileId { get; set; }

        [Required]
        public string BlobReference { get; set; }

        [Required]
        public string AssetType { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}