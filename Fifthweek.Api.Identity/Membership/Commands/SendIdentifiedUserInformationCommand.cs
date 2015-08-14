namespace Fifthweek.Api.Identity.Membership.Commands
{
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class SendIdentifiedUserInformationCommand
    {
        public Requester Requester { get; private set; }

        public bool IsUpdate { get; private set; }

        public Email Email { get; private set; }

        [Optional]
        public string Name { get; private set; }

        [Optional]
        public Username Username { get; private set; }
    }
}