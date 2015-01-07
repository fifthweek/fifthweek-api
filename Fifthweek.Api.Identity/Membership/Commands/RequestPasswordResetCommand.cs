using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Commands
{
    [AutoEqualityMembers, AutoConstructor]
    public partial class RequestPasswordResetCommand
    {
        [Optional]
        public NormalizedEmail Email { get; private set; }

        [Optional]
        public NormalizedUsername Username { get; private set; }
    }
}