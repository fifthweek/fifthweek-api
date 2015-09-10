namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql, AutoCopy(RequiresBuilder = false)]
    public partial class Queue
    {
        public Queue()
        {
        }

        [Required, Key]
        public Guid Id { get; set; }

        [Required]
        public Guid BlogId { get; set; }

        [Required, Optional, NonEquatable]
        public Blog Blog { get; set; }

        [Required]
        [MaxLength(50)] // See: ValidCollectionName.MaxLength
        public string Name { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}