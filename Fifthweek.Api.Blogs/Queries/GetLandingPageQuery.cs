namespace Fifthweek.Api.Blogs.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetLandingPageQuery : IQuery<GetLandingPageResult>
    {
        public Username CreatorUsername { get; private set; }
    }
}