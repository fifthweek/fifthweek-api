namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Posts.Shared;

    public interface ISetBacklogPostLiveDateDbStatement
    {
        Task ExecuteAsync(PostId postId, DateTime newTime, DateTime now);
    }
}