namespace Fifthweek.Api.Identity.Membership.Commands
{
    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class RequestPasswordResetCommand
    {
        public NormalizedEmail Email { get; private set; }

        public NormalizedUsername Username { get; private set; }
    }
}