namespace Fifthweek.Api.Accounts.Queries
{
    using Fifthweek.Api.Accounts.Controllers;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class GetAccountSettingsQuery : IQuery<GetAccountSettingsResult>
    {
        public UserId AuthenticatedUserId { get; private set; }

        public UserId RequestedUserId { get; private set; }
    }
}