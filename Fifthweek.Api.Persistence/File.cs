namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.Api.Core;

    [AutoConstructor]
    public partial class File
    {
        public File()
        {
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [ForeignKey("UserId")]
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public FileState State { get; set; }

        [Required]
        public string BlobReference { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Optional]
        public DateTime? CompletionDate { get; set; }

        [Optional]
        public DateTime? ProcessedDate { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string Purpose { get; set; }
    }
}