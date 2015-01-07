using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Controllers
{
    [AutoEqualityMembers]
    public partial class PasswordResetRequestData
    {
        [Optional]
        [Parsed(typeof(Email))]
        public string Email { get; set; }

        [Optional]
        [Parsed(typeof(Username))]
        public string Username { get; set; }
    }
}