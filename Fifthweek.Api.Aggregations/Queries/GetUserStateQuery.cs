namespace Fifthweek.Api.Aggregations.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetUserStateQuery : IQuery<UserState>
    {
        public Requester Requester { get; private set; }
        
        [Optional]
        public UserId RequestedUserId { get; private set; }
    }
}