namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers, AutoSql]
    public partial class RefreshToken
    {
        public RefreshToken()
        {
        }

        [MaxLength(128)]
        [Key]
        public string HashedId { get; set; }

        [MaxLength(256)]
        [Required]
        [Index("IX_UsernameAndClientId", 1)]
        public string Username { get; set; }

        [MaxLength(64)]
        [Required]
        [Index("IX_UsernameAndClientId", 2)]
        public string ClientId { get; set; }

        [Required]
        public DateTime IssuedDate { get; set; }

        [Required]
        public DateTime ExpiresDate { get; set; }

        [Required]
        public string ProtectedTicket { get; set; }
    }
}