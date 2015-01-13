using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Controllers
{
    [AutoEqualityMembers]
    public partial class PasswordResetRequestData
    {
        [Optional]
        [Parsed(typeof(ValidEmail))]
        public string Email { get; set; }

        [Optional]
        [Parsed(typeof(ValidUsername))]
        public string Username { get; set; }
    }
}