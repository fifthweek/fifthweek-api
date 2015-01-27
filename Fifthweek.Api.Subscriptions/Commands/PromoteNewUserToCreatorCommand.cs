namespace Fifthweek.Api.Subscriptions.Commands
{
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    [AutoConstructor, AutoEqualityMembers]
    public partial class PromoteNewUserToCreatorCommand
    {
        public UserId NewUserId { get; private set; }
    }
}