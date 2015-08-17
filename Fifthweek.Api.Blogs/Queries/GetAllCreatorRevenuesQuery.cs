namespace Fifthweek.Api.Blogs.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetAllCreatorRevenuesQuery : IQuery<GetAllCreatorRevenuesResult>
    {
        public Requester Requester { get; private set; }
    }
}