using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Commands
{
    [AutoEqualityMembers, AutoConstructor]
    public partial class RequestPasswordResetCommand
    {
        [Optional]
        public Email Email { get; private set; }

        [Optional]
        public Username Username { get; private set; }
    }
}