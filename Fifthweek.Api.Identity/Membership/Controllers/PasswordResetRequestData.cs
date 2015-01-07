using System.Web.Http.ModelBinding;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Controllers
{
    [AutoEqualityMembers]
    public partial class PasswordResetRequestData
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public Email EmailObj { get; private set; }

        public Username UsernameObj { get; private set; }

        public void Parse()
        {
            var modelState = new ModelStateDictionary();

            this.UsernameObj = this.Username.AsUsername("Username", modelState, isRequired: false);
            this.EmailObj = this.Email.AsEmail("Email", modelState, isRequired: false);

            if (!modelState.IsValid)
            {
                throw new ModelValidationException(modelState);
            }
        }
    }
}