namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Shared;

    public interface IMovePostToQueueDbStatement
    {
        Task ExecuteAsync(PostId postId, QueueId queueId);
    }
}