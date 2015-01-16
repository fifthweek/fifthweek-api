using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Commands
{
    using Fifthweek.Shared;

    [AutoEqualityMembers, AutoConstructor]
    public partial class RegisterUserCommand
    {
        public UserId UserId { get; private set; }

        [Optional]
        public string ExampleWork { get; private set; }

        public ValidEmail Email { get; private set; }

        public ValidUsername Username { get; private set; }

        public ValidPassword Password { get; private set; }
    }
}