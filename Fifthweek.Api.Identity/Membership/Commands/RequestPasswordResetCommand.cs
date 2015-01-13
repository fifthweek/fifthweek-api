using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Commands
{
    [AutoEqualityMembers, AutoConstructor]
    public partial class RequestPasswordResetCommand
    {
        [Optional]
        public ValidatedEmail Email { get; private set; }

        [Optional]
        public ValidatedUsername Username { get; private set; }
    }
}