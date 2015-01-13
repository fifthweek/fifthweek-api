using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Controllers
{
    [AutoEqualityMembers]
    public partial class PasswordResetRequestData
    {
        [Optional]
        [Parsed(typeof(ValidatedEmail))]
        public string Email { get; set; }

        [Optional]
        [Parsed(typeof(ValidatedUsername))]
        public string Username { get; set; }
    }
}