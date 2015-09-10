namespace Fifthweek.Api.Identity.Membership.Commands
{
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class UpdateCreatorAccountSettingsCommand
    {
        public Requester Requester { get; private set; }

        public UserId RequestedUserId { get; private set; }
    }
}