namespace Dexter.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RefreshToken
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50)]
        public string ClientId { get; set; }

        public DateTime IssuedUtc { get; set; }

        public DateTime ExpiresUtc { get; set; }

        [Required]
        public string ProtectedTicket { get; set; }
    }
}