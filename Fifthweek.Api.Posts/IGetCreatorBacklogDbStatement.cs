namespace Fifthweek.Api.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Queries;

    public interface IGetCreatorBacklogDbStatement
    {
        Task<IReadOnlyList<BacklogPost>> ExecuteAsync(UserId creatorId, DateTime now);
    }
}