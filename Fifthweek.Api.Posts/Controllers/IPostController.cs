namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Posts.Shared;

    public interface IPostController
    {
        Task<IEnumerable<GetCreatorBacklogQueryResult>> GetCreatorBacklog(string creatorId);

        Task<IEnumerable<GetCreatorNewsfeedQueryResult>> GetCreatorNewsfeed(string creatorId, CreatorNewsfeedPaginationData newsfeedPaginationData);

        Task DeletePost(string postId);

        Task PostNewQueueOrder(string collectionId, IEnumerable<PostId> newQueueOrder);

        Task PostToQueue(string postId);

        Task PostToLive(string postId);

        Task PutLiveDate(string postId, DateTime newLiveDate);

        Task<GetNewsfeedQueryResult> GetNewsfeed(NewsfeedFilter filterData);
    }
}