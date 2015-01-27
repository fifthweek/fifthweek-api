namespace Fifthweek.Api.Accounts.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class GetAccountSettingsQuery : IQuery<GetAccountSettingsResult>
    {
        public Requester Requester { get; private set; }

        public UserId RequestedUserId { get; private set; }
    }
}