namespace Fifthweek.Api.Subscriptions.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetCreatorStatusQuery : IQuery<CreatorStatus>
    {
        public Requester Requester { get; private set; }

        public UserId RequestedUserId { get; private set; }
    }
}