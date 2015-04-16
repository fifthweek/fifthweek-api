namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class FreeAccessUser
    {
        public FreeAccessUser()
        {
        }

        [Required, Key, Column(Order = 0)]
        public Guid BlogId { get; set; }

        [Required, Optional, NonEquatable]
        public Blog Blog { get; set; }

        [Required, Key, Column(Order = 1), Index]
        [MaxLength(256)] // See: ValidEmail.MaxLength
        public string Email { get; set; }
    }
}