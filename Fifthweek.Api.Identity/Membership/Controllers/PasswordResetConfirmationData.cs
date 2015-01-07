using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.ModelBinding;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Controllers
{
    [AutoEqualityMembers]
    public partial class PasswordResetConfirmationData
    {
        public Guid UserId { get; set; }

        [Required]
        [Display(Name = "Token")]
        public string Token { get; set; }

        public string NewPassword { get; set; }

        public UserId UserIdObj { get; private set; }

        public Password NewPasswordObj { get; private set; }

        public void Parse()
        {
            var modelState = new ModelStateDictionary();

            this.UserIdObj = Membership.UserId.Parse(this.UserId);
            this.NewPasswordObj = this.NewPassword.AsPassword("NewPassword", modelState);

            if (!modelState.IsValid)
            {
                throw new ModelValidationException(modelState);
            }
        }
    }
}