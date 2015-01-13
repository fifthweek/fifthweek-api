using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Controllers
{
    [AutoEqualityMembers]
    public partial class RegistrationData
    {
        public string ExampleWork { get; set; }

        [Parsed(typeof(ValidatedEmail))]
        public string Email { get; set; }

        [Parsed(typeof(ValidatedUsername))]
        public string Username { get; set; }

        [Parsed(typeof(ValidatedPassword))]
        public string Password { get; set; }
    }
}