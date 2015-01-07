using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Controllers
{
    [AutoEqualityMembers]
    public partial class RegistrationData
    {
        public string ExampleWork { get; set; }

        [Parsed(typeof(Email))]
        public string Email { get; set; }

        [Parsed(typeof(Username))]
        public string Username { get; set; }

        [Parsed(typeof(Password))]
        public string Password { get; set; }
    }
}