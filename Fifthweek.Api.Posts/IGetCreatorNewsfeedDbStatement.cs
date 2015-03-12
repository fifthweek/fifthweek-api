namespace Fifthweek.Api.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Shared;

    public interface IGetCreatorNewsfeedDbStatement
    {
        Task<IReadOnlyList<NewsfeedPost>> ExecuteAsync(UserId creatorId, DateTime now, NonNegativeInt startIndex, PositiveInt count);
    }
}