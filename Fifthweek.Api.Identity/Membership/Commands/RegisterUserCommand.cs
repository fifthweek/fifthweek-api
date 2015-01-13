using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Commands
{
    [AutoEqualityMembers, AutoConstructor]
    public partial class RegisterUserCommand
    {
        public UserId UserId { get; private set; }

        [Optional]
        public string ExampleWork { get; private set; }

        public ValidatedEmail Email { get; private set; }

        public ValidatedUsername Username { get; private set; }

        public ValidatedPassword Password { get; private set; }
    }
}