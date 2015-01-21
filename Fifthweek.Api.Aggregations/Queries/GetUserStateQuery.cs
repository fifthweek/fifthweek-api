namespace Fifthweek.Api.Aggregations.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetUserStateQuery : IQuery<UserState>
    {
        public UserId RequestedUserId { get; private set; }

        public Requester Requester { get; private set; }

        public bool IsCreator { get; private set; }
    }
}