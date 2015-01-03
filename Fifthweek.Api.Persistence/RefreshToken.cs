namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class RefreshToken
    {
        [Key]
        public string HashedId { get; set; }

        [Required]
        [MaxLength(50)]
        [Index("IX_UsernameAndClientId", 1)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        [Index("IX_UsernameAndClientId", 2)]
        public string ClientId { get; set; }

        public DateTime IssuedUtc { get; set; }

        public DateTime ExpiresUtc { get; set; }

        [Required]
        public string ProtectedTicket { get; set; }
    }
}