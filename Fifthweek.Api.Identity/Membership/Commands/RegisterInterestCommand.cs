namespace Fifthweek.Api.Identity.Membership.Commands
{
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class RegisterInterestCommand
    {
        public string Name { get; private set; }

        public ValidEmail Email { get; private set; }
    }
}