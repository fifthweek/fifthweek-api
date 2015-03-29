namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Shared;

    public interface ITryGetUnqueuedPostCollectionDbStatement
    {
        Task<CollectionId> ExecuteAsync(PostId postId);
    }
}