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

        [MaxLength(256)]
        [Required]
        [Key, Column(Order = 0)]
        public string Username { get; set; }

        [MaxLength(64)]
        [Required]
        [Key, Column(Order = 1)]
        public string ClientId { get; set; }

        [MaxLength(48), Index]
        public string EncryptedId { get; set; }

        [Required]
        public DateTime IssuedDate { get; set; }

        [Required]
        public DateTime ExpiresDate { get; set; }

        [Required]
        public string ProtectedTicket { get; set; }
    }
}