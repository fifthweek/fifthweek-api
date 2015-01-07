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

        public UserId UserIdObject { get; private set; }

        public Password NewPasswordObject { get; private set; }

        public void Parse()
        {
            var modelState = new ModelStateDictionary();

            this.UserIdObject = Membership.UserId.Parse(this.UserId);
            this.NewPasswordObject = this.NewPassword.AsPassword("NewPassword", modelState);

            if (!modelState.IsValid)
            {
                throw new ModelValidationException(modelState);
            }
        }
    }
}