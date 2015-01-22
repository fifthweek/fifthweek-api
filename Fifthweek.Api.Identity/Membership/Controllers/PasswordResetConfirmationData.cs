namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using System.ComponentModel.DataAnnotations;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class PasswordResetConfirmationData
    {
        public UserId UserId { get; set; }

        [Parsed(typeof(ValidPassword))]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Token")]
        public string Token { get; set; }
    }
}