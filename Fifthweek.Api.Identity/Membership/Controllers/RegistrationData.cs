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

        public Email EmailObject { get; private set; }

        public Username UsernameObject { get; private set; }

        public Password PasswordObject { get; private set; }

        public void Parse()
        {
            var modelState = new ModelStateDictionary();

            this.UsernameObject = this.Username.AsUsername("Username", modelState);
            this.EmailObject = this.Email.AsEmail("Email", modelState);
            this.PasswordObject = this.Password.AsPassword("Password", modelState);

            if (!modelState.IsValid)
            {
                throw new ModelValidationException(modelState);
            }
        }
    }
}