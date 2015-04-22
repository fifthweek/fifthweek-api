namespace Fifthweek.Api.Blogs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetUserSubscriptionsDbStatement
    {
        Task<IReadOnlyList<BlogSubscriptionDbResult>> ExecuteAsync(UserId userId);
    }
}