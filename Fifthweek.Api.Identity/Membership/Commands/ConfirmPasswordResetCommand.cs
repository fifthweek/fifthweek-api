using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Commands
{
    [AutoEqualityMembers, AutoConstructor]
    public partial class ConfirmPasswordResetCommand
    {
        public UserId UserId { get; private set; }

        public string Token { get; private set; }

        public Password NewPassword { get; private set; }
    }
}