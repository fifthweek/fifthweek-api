namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IUpdateBlogSubscriptionsDbStatement
    {
        Task ExecuteAsync(UserId userId, BlogId blogId, IReadOnlyList<AcceptedChannelSubscription> channels, DateTime now);
    }
}