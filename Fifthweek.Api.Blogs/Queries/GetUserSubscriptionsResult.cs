namespace Fifthweek.Api.Blogs.Queries
{
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetUserSubscriptionsResult
    {
        public IReadOnlyList<BlogSubscriptionStatus> Blogs { get; private set; }
    }
}