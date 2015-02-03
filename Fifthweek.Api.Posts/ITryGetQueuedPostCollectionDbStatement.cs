namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Shared;

    public interface ITryGetQueuedPostCollectionDbStatement
    {
        Task<CollectionId> ExecuteAsync(PostId postId, DateTime now);
    }
}