namespace Fifthweek.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    public class InternalRegistrationData
    {
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(100, MinimumLength = 6)]
        [RegularExpression(@"[a-zA-Z0-9-]+", ErrorMessage = "Only alphanumeric characters and hyphens are allowed in the username.")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}