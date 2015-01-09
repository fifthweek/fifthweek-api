using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Commands
{
    [AutoEqualityMembers, AutoConstructor]
    public partial class RegisterUserCommand
    {
        public UserId UserId { get; private set; }

        [Optional]
        public string ExampleWork { get; private set; }

        public Email Email { get; private set; }

        public Username Username { get; private set; }

        public Password Password { get; private set; }
    }
}