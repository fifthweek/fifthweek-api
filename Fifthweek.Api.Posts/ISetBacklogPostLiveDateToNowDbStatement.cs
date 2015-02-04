namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Posts.Shared;

    public interface ISetBacklogPostLiveDateToNowDbStatement
    {
        Task ExecuteAsync(PostId postId, DateTime now);
    }
}