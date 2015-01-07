using System.Web.Http.ModelBinding;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Controllers
{
    [AutoEqualityMembers]
    public partial class RegistrationData
    {
        public string ExampleWork { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public Email EmailObj { get; private set; }

        public Username UsernameObj { get; private set; }

        public Password PasswordObj { get; private set; }

        public void Parse()
        {
            var modelState = new ModelStateDictionary();

            this.UsernameObj = this.Username.AsUsername("Username", modelState);
            this.EmailObj = this.Email.AsEmail("Email", modelState);
            this.PasswordObj = this.Password.AsPassword("Password", modelState);

            if (!modelState.IsValid)
            {
                throw new ModelValidationException(modelState);
            }
        }
    }
}