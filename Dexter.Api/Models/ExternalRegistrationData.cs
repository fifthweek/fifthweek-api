namespace Dexter.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ExternalRegistrationData
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Provider { get; set; }

        [Required]
        public string ExternalAccessToken { get; set; }

    }
}