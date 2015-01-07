namespace Fifthweek.Api.Identity.Membership.Commands
{
    [AutoEqualityMembers]
    public partial class RequestPasswordResetCommand
    {
        public RequestPasswordResetCommand(NormalizedEmail email, NormalizedUsername username)
        {
            this.Email = email;
            this.Username = username;
        }

        public NormalizedEmail Email { get; private set; }

        public NormalizedUsername Username { get; private set; }
    }
}