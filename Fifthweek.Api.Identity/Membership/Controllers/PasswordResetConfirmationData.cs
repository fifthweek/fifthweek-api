using System;
using System.ComponentModel.DataAnnotations;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using Fifthweek.Shared;

    [AutoEqualityMembers]
    public partial class PasswordResetConfirmationData
    {
        [Constructed(typeof(UserId))]
        public Guid UserId { get; set; }

        [Parsed(typeof(ValidPassword))]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Token")]
        public string Token { get; set; }
    }
}