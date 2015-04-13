namespace Fifthweek.Api.Blogs.Queries
{
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetBlogSubscriptionsQuery : IQuery<GetBlogSubscriptionsResult>
    {
        public Requester Requester { get; private set; }
    }
}